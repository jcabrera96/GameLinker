﻿using GameLinker.Forms;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using Microsoft.OneDrive.Sdk.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker.Helpers
{
    class OnedriveHelper
    {
        static string[] scopes = { "onedrive.readwrite", "wl.signin" };
        static JObject jsonObject;
        static readonly string appId = Init();
        static OneDriveClient client;

        static string Init()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameLinker.Configs.AppKey.json"))
            {
                JsonTextReader jsonReader = new JsonTextReader(new StreamReader(stream));
                jsonObject = JObject.Load(jsonReader);
                return (string)jsonObject.SelectToken("App.Key");
            };
        }

        async static Task InitClient()
        {
            MsaAuthenticationProvider authenticator = AuthentificationHelper.GetAuthenticator(appId, "https://login.live.com/oauth20_desktop.srf", scopes);
            await authenticator.AuthenticateUserAsync();
            client = new OneDriveClient("https://api.onedrive.com/v1.0", authenticator);
        }

        public async static Task<Item> GetFolder(string folderPath)
        {
            if (client == null) await InitClient();
            var folder = await client
                             .Drive
                             .Root
                             .ItemWithPath(folderPath)
                             .Request()
                             .GetAsync();
            return folder;
        }

        public async static Task<Item> CreateFolder(string folderPath)
        {
            if (client == null) await InitClient();
            var folderToCreate = new Item { Folder = new Folder() };
            var folder = await client
                             .Drive
                             .Root
                             .ItemWithPath(folderPath)
                             .Request()
                             .CreateAsync(folderToCreate);
            return folder;
        }

        public async static Task<Item> UploadItem(string itemPath, string destinationPath, bool showProgress = true)
        {
            if (client == null) await InitClient();
            var fileStream = FileToStreamHelper.GetFileStream(itemPath);
            var myMaxChunkSize = 5 * 1024 * 1024; // 5MB
            var session = await client.Drive.Root.ItemWithPath(destinationPath).CreateSession().Request().PostAsync();
            var provider = new ChunkedUploadProvider(session, client, fileStream, myMaxChunkSize);
            // Setup the chunk request necessities
            var chunkRequests = provider.GetUploadChunkRequests();
            var readBuffer = new byte[myMaxChunkSize];
            var trackedExceptions = new List<Exception>();
            Item itemResult = null;
            UploadProgressForm uploadForm = new UploadProgressForm();
            if (showProgress)uploadForm.Show();
            //upload the chunks
            for (var chunk = 0; chunk < chunkRequests.Count(); chunk++)
            {
                // Send chunk request
                var result = await provider.GetChunkRequestResponseAsync(chunkRequests.ElementAt(chunk), readBuffer, trackedExceptions);
                uploadForm.uploadProgressBar.Value = (int)(((double)(chunk + 1) / chunkRequests.Count()) * 100);
                uploadForm.uploadValueLabel.Text = (int)(((double)(chunk + 1) / chunkRequests.Count()) * 100) + "%";
                if (result.UploadSucceeded)
                {
                    itemResult = result.ItemResponse;
                    uploadForm.acceptButton.Enabled = true;
                }
            }

            // Check that upload succeeded
            if (itemResult == null)
            {
                // Retry the upload
                // ...
            }
            return itemResult;
        }

        public async static Task<Item> UploadFolder(string folderPath, string destinationPath)
        {
            Item folder = await CreateFolder(destinationPath);
            var folderName = Path.GetFileName(folderPath);
            List<string> files = Directory.GetFiles(folderPath).ToList();
            UploadProgressForm uploadForm = new UploadProgressForm();
            uploadForm.uploadLabel.Text = "Creating folders...";
            uploadForm.uploadValueLabel.Text = "";
            uploadForm.uploadProgressBar.Value = 100;
            uploadForm.Show();
            await CheckForFolders(folderPath, destinationPath, files);
            uploadForm.uploadLabel.Text = "Upload progress:";
            uploadForm.uploadValueLabel.Text = "0%";
            uploadForm.uploadProgressBar.Value = 0;
            // Copy the files and overwrite destination files if they already exist.
            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                Item result = await UploadItem(file, destinationPath + "/" + file.Substring(file.IndexOf(folderName)), false);
                if (result != null)
                {
                    uploadForm.uploadProgressBar.Value = (int)(((double)(files.ToList().IndexOf(file) + 1) / files.Count) * 100);
                    uploadForm.uploadValueLabel.Text = (int)(((double)(files.ToList().IndexOf(file) + 1) / files.Count) * 100) + "%";
                }
            }
            uploadForm.acceptButton.Enabled = true;
            return folder;
        }

        private async static Task CheckForFolders(string rootPath, string destinationPath, List<string> files)
        {
            foreach (var folder in Directory.GetDirectories(rootPath))
            {
                var folderName = folder.Substring(folder.Replace('\\', '/').LastIndexOf('/'));
                await CreateFolder(destinationPath + folderName);
                foreach (var file in Directory.GetFiles(folder))
                {
                    files.Add(file);
                }
                await CheckForFolders(folder , destinationPath + folderName, files);
            }    

        }

    }
}
