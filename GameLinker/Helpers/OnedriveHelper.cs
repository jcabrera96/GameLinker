using GameLinker.Forms;
using GameLinker.Properties;
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

        public int compressedFilesCount, uploadedCompressedFiles;

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
            var folderToCreate = new Item { Folder = new Folder() };
            try
            {
                if (authenticator == null) await InitClient();
                var folder = await client
                                 .Drive
                                 .Root
                                 .ItemWithPath(folderPath)
                                 .Request()
                                 .CreateAsync(folderToCreate);
                return folder;
            }
            catch (Microsoft.Graph.ServiceException)
            {
                return await client
                                 .Drive
                                 .Root
                                 .ItemWithPath(folderPath)
                                 .Request()
                                 .GetAsync();
            }
        }

        public async Task<byte[]> ReadItem(string itemPath)
        {
            try
            {
                if (authenticator == null) await InitClient();
                var dataStream = await client.Drive.Root.ItemWithPath(itemPath).Content.Request().GetAsync();
                byte[] data = new byte[(int)dataStream.Length];
                dataStream.Read(data, 0, (int)dataStream.Length);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Item> UploadItem(string itemPath, string destinationPath, UploadProgressForm uploadForm)
        {
            if (authenticator == null) await InitClient();
            Item itemResult = null;
            try
            {
                using (var fileStream = FileToStreamHelper.GetFileStream(itemPath))
                {
                    var myMaxChunkSize = 16 * 320 * 1024; // 5MiB
                    var session = await client.Drive.Root.ItemWithPath(destinationPath).CreateSession().Request().PostAsync();
                    var provider = new ChunkedUploadProvider(session, client, fileStream, (int)myMaxChunkSize);
                    if (uploadForm != null && !uploadForm.Visible) uploadForm.Show();
                    //upload the chunks

                    // Setup the chunk request necessities
                    var chunkRequests = provider.GetUploadChunkRequests();
                    var readBuffer = new byte[(int)myMaxChunkSize];
                    var trackedExceptions = new List<Exception>();
                    UploadChunkResult result;
                    int chunkCount = 0, totalChunks = chunkRequests.Count();
                    for (var chunk = 0; chunk < chunkRequests.Count(); chunk++)
                    {
                        try
                        {
                            result = await provider.GetChunkRequestResponseAsync(chunkRequests.ElementAt(chunk), readBuffer, trackedExceptions);
                            chunkCount++;
                            if (result.UploadSucceeded)
                            {
                                uploadedCompressedFiles++;
                                uploadForm.Invoke((MethodInvoker)delegate
                                {
                                    uploadForm.uploadProgressBar.Value = (int)(((float)uploadedCompressedFiles / compressedFilesCount) * 100);
                                    uploadForm.uploadValueLabel.Text = (int)(((float)uploadedCompressedFiles / compressedFilesCount) * 100) + "%";
                                });
                                itemResult = result.ItemResponse;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            // Check that upload succeeded
            if (itemResult == null)
            {
                itemResult = await UploadItem(itemPath, destinationPath, uploadForm);
            }
            System.GC.Collect();
            return itemResult;
        }

        public async Task<int> UploadFolder(string folderPath, string destinationPath, UploadProgressForm uploadForm, string gameName,bool isGameData = true)
        {
            Item folder = await CreateFolder(destinationPath);
            var folderName = Path.GetFileName(folderPath);
            uploadForm.uploadLabel.Text = isGameData ? "Compressing data files" : "Compressing save files";
            uploadForm.uploadValueLabel.Text = "";
            uploadForm.uploadProgressBar.Value = 100;
            uploadForm.uploadProgressBar.Style = ProgressBarStyle.Marquee;
            uploadForm.Show();
            Dictionary<string,List<string>> compressedFilesData = await CompressFiles(uploadForm, folderPath, gameName, isGameData);
            System.GC.Collect();
            uploadForm.uploadLabel.Text = isGameData ? "Uploading data files" : "Uploading save files";
            uploadForm.uploadValueLabel.Text = "0%";
            uploadForm.uploadProgressBar.Value = 0;
            uploadForm.uploadProgressBar.Style = ProgressBarStyle.Continuous;
            await PerformBulkUpload(uploadForm, destinationPath, compressedFilesData.ElementAt(0).Value, isGameData);
            int fileSize = (int)new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameName + (isGameData ? "_Data.tar.gz" : "_Saves.tar.gz")).Length;
            CleanTempFiles(compressedFilesData.ElementAt(0).Key, compressedFilesData.ElementAt(0).Value);
            return fileSize;
        }

        private async Task PerformBulkUpload(UploadProgressForm uploadForm, string destinationPath ,List<string> compressedFiles, bool isGameData)
        {
            System.Timers.Timer dotsTimer = new System.Timers.Timer(1000);
            dotsTimer = new System.Timers.Timer(1000);
            dotsTimer.AutoReset = true;
            dotsTimer.Elapsed += (s, e) =>
            {
                int dotsCount = uploadForm.uploadLabel.Text.Count(c => c == '.');
                uploadForm.Invoke((MethodInvoker)delegate {
                    uploadForm.uploadLabel.Text = (isGameData ? "Uploading data files" : "Uploading save files") + new string('.', (dotsCount + 1) % 4);
                });
            };
            dotsTimer.Enabled = true;
            int threadCount = 0;
            foreach (var file in compressedFiles)
            {
                Task.Run(async () =>
                {
                    await UploadItem(file, destinationPath + (isGameData ? "data/" : "saves/") + Path.GetFileName(file), uploadForm);
                    threadCount--;
                });
                threadCount++;
                while (threadCount >= Settings.Default.MaxUploadThreads) await Task.Delay(100);
            }
            while (uploadedCompressedFiles < compressedFilesCount) await Task.Delay(100);
            dotsTimer.Stop();
        }

        private async Task<Dictionary<string, List<string>>> CompressFiles(UploadProgressForm uploadForm, string folderPath, string gameName, bool isGameData)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Temp/"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Temp/");
            string compressedFilePath = AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameName + (isGameData ? "_Data.tar.gz" : "_Saves.tar.gz");
            System.Timers.Timer dotsTimer = new System.Timers.Timer(1000);
            dotsTimer.AutoReset = true;
            dotsTimer.Elapsed += (s, e) =>
            {
                int dotsCount = uploadForm.uploadLabel.Text.Count(c => c == '.');
                uploadForm.Invoke((MethodInvoker)delegate {
                    uploadForm.uploadLabel.Text = (isGameData ? "Compressing data files" : "Compressing save files") + new string('.', (dotsCount + 1) % 4);
                });
            };
            dotsTimer.Enabled = true;
            List<string> compressedFiles = await Task.Run(async () =>
            {
                return await CompressionHelper.CompressAndSplit(compressedFilePath, folderPath);
            });
            compressedFilesCount = compressedFiles.Count;
            uploadedCompressedFiles = 0;
            dotsTimer.Stop();
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            result.Add(compressedFilePath, compressedFiles);
            return result;
        }

        private void CleanTempFiles(string compressedFilePath, List<string> compressedFiles)
        {
            System.IO.File.Delete(compressedFilePath);
            foreach (var file in compressedFiles) System.IO.File.Delete(file);
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
