using GameLinker.Forms;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker.Helpers
{
    public class OnedriveHelper
    {
        readonly string[] scopes = { "onedrive.readwrite", "wl.signin" };
        JObject jsonObject;
        string appId;
        MsaAuthenticationProvider authenticator;
        OneDriveClient client;

        private static OnedriveHelper _instance;

        private OnedriveHelper()
        {

        }

        public static OnedriveHelper Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OnedriveHelper();
                    _instance.appId = _instance.Init();
                }
                return _instance;
            }
        }

        string Init()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameLinker.Configs.AppKey.json"))
            {
                JsonTextReader jsonReader = new JsonTextReader(new StreamReader(stream));
                jsonObject = JObject.Load(jsonReader);
                return (string)jsonObject.SelectToken("App.Key");
            };
        }

        async Task InitClient()
        {
            authenticator = AuthentificationHelper.GetAuthenticator(appId, "https://login.live.com/oauth20_desktop.srf", scopes);
            await authenticator.AuthenticateUserAsync();
            client = new OneDriveClient("https://api.onedrive.com/v1.0", authenticator);
        }

        public async Task<Item> GetFolder(string folderPath)
        {
            if (authenticator == null) await InitClient();
            OneDriveClient client = new OneDriveClient("https://api.onedrive.com/v1.0", authenticator);
            var folder = await client
                             .Drive
                             .Root
                             .ItemWithPath(folderPath)
                             .Request()
                             .GetAsync();
            return folder;
        }

        public async Task<Item> CreateFolder(string folderPath)
        {
            if (authenticator == null) await InitClient();
            OneDriveClient client = new OneDriveClient("https://api.onedrive.com/v1.0", authenticator);
            var folderToCreate = new Item { Folder = new Folder() };
            var folder = await client
                             .Drive
                             .Root
                             .ItemWithPath(folderPath)
                             .Request()
                             .CreateAsync(folderToCreate);
            return folder;
        }

        public async Task<Item> UploadItem(string itemPath, string destinationPath, UploadProgressForm uploadForm = null)
        {
            if (authenticator == null) await InitClient();
            var fileStream = FileToStreamHelper.GetFileStream(itemPath);
            var myMaxChunkSize = 15 * 1024 * 1024; // 15MB
            var session = await client.Drive.Root.ItemWithPath(destinationPath).CreateSession().Request().PostAsync();
            var provider = new ChunkedUploadProvider(session, client, fileStream, myMaxChunkSize);
            Item itemResult = null;
            UploadProgressForm localUploadForm = null;
            if (uploadForm == null)
            {
                localUploadForm = new UploadProgressForm();
                localUploadForm.Show();
            }
            if (uploadForm != null && !uploadForm.Visible) uploadForm.Show();
            //upload the chunks
            
            if(uploadForm == null)
            {
                // Setup the chunk request necessities
                var chunkRequests = provider.GetUploadChunkRequests();
                var readBuffer = new byte[myMaxChunkSize];
                var trackedExceptions = new List<Exception>();
                for (var chunk = 0; chunk < chunkRequests.Count(); chunk++)
                {
                    var result = await provider.GetChunkRequestResponseAsync(chunkRequests.ElementAt(chunk), readBuffer, trackedExceptions);
                    localUploadForm.uploadProgressBar.Value = (int)(((double)(chunk + 1) / chunkRequests.Count()) * 100);
                    localUploadForm.uploadValueLabel.Text = (int)(((double)(chunk + 1) / chunkRequests.Count()) * 100) + "%";
                    if (result.UploadSucceeded && localUploadForm.uploadProgressBar.Value == 100)
                    {
                        itemResult = result.ItemResponse;
                        localUploadForm.acceptButton.Enabled = true;
                    }
                }

            }
            else
            {
                var result = await provider.UploadAsync();
                if (result != null && uploadForm.uploadProgressBar.Value == 100)
                {
                    itemResult = result;
                    uploadForm.Invoke((MethodInvoker)delegate
                    {
                        uploadForm.acceptButton.Enabled = true;
                    });
                }
                else if (result != null)
                {
                    itemResult = result;
                }
            }
            // Check that upload succeeded
            if (itemResult == null)
            {
                itemResult =  await UploadItem(itemPath, destinationPath, uploadForm);
            }
            return itemResult;
        }

        public async Task<Item> UploadFolder(string folderPath, string destinationPath)
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
            uploadForm.uploadProgressBar.Style = ProgressBarStyle.Continuous;
            var limiter = 0;
            var count = 0;
            // Copy the files and overwrite destination files if they already exist.
            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                limiter++;
                Task.Run(async () =>
                {
                    Item result = await UploadItem(file, destinationPath + file.Substring(file.IndexOf(folderName) + folderName.Length), uploadForm);
                    if (result != null)
                    {
                        count++;
                        uploadForm.Invoke((MethodInvoker)delegate {
                            uploadForm.uploadProgressBar.Value = (int)(((double) count/ files.Count) * 100);
                            uploadForm.uploadValueLabel.Text = (int)(((double) count / files.Count) * 100) + "%";
                        });
                    }
                    limiter--;
                });
                while (limiter >= 15) { await Task.Delay(1); };
            }
            return folder;
        }

        private async Task CheckForFolders(string rootPath, string destinationPath, List<string> files)
        {
            foreach (var folder in Directory.GetDirectories(rootPath))
            {
                var folderName = folder.Substring(folder.Replace('\\', '/').LastIndexOf('/'));
                foreach (var file in Directory.GetFiles(folder))
                {
                    files.Add(file);
                }
                await CheckForFolders(folder , destinationPath + folderName, files);
            }
        }

    }
}
