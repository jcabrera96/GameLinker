using GameLinker.Forms;
using GameLinker.Properties;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker.Helpers
{
    public class CompressionHelper
    {
        static JObject lang = (JObject)LocalizationHelper.Instance.compressionHelperLocalization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];

        public async static Task<List<string>> CompressAndSplit(string tarFilePath, string sourceDirectory)
        {
            FileStream stream = await Compress(tarFilePath, sourceDirectory);
            double myMaxChunkSize = 16 * 320 * 1024; // 5MiB
            int numBytesToRead = (int)stream.Length;
            int numBytesRead = 0;
            int fileIndex = 0;
           
            List<string> files = new List<string>();
            while (numBytesToRead > 0)
            {
                try
                {
                    byte[] data = new byte[((int)myMaxChunkSize < numBytesToRead ? (int)myMaxChunkSize : numBytesToRead)];
                    // Read may return anything from 0 to numBytesToRead.
                    int n = stream.Read(data, 0, (int)myMaxChunkSize < numBytesToRead ? (int)myMaxChunkSize : numBytesToRead);
                    files.Add(tarFilePath + ".part_" + fileIndex);
                    await SaveData(data, tarFilePath + ".part_" + fileIndex);
                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;
                    fileIndex++;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                catch (Exception)
                {

                }
            }
            return files;
        }

        static Task<FileStream> Compress(string tarFilePath, string sourceDirectory)
        {
            Stream outStream = File.Create(tarFilePath);
            Stream gzoStream = new GZipOutputStream(outStream);
            TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzoStream);

            tarArchive.RootPath = sourceDirectory.Replace('\\', '/');
            if (tarArchive.RootPath.EndsWith("/"))
                tarArchive.RootPath = tarArchive.RootPath.Remove(tarArchive.RootPath.Length - 1);

            AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);

            tarArchive.Close();
            return Task.FromResult(File.OpenRead(tarFilePath));
        }

        static async Task CompressFilePart(string tarFilePath, MemoryStream stream)
        {
            try
            {
                Stream outStream = File.Create(tarFilePath);
                Stream gzoStream = new GZipOutputStream(outStream);
                TarOutputStream tarOutputStream = new TarOutputStream(gzoStream);

                Guid g = Guid.NewGuid();
                string randomName = Convert.ToBase64String(g.ToByteArray());
                randomName = randomName.Replace("=", "");
                randomName = randomName.Replace("+", "");

                TarEntry tarEntry = TarEntry.CreateTarEntry(randomName + ".bin");
                tarEntry.Size = stream.Length;
                tarOutputStream.PutNextEntry(tarEntry);
                await tarOutputStream.WriteAsync(stream.ToArray(), 0, (int)stream.Length);
                tarOutputStream.CloseEntry();
            }
            catch (Exception)
            {

            }
        }

        public async static Task JoinAndDecompress(Game gameData, UploadProgressForm uploadForm, string dataPath = null, string savesPath = null)
        {
            uploadForm.uploadLabel.Text = (string)lang["updating_library"];
            uploadForm.uploadValueLabel.Text = "";
            uploadForm.Show();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Temp/")) Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Temp/");
            if(dataPath != "")
            {
                uploadForm.uploadLabel.Text = (string)lang["downloading_data_fragments"];
                uploadForm.uploadValueLabel.Text = "0%";
                uploadForm.uploadProgressBar.Value = 0;
                uploadForm.uploadProgressBar.Style = ProgressBarStyle.Continuous;
                byte[] dataBuffer = await ReadAllParts(gameData, true, uploadForm);
                uploadForm.uploadLabel.Text = (string)lang["decompressing_data"];
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.uploadProgressBar.Value = 100;
                uploadForm.uploadProgressBar.Style = ProgressBarStyle.Marquee;
                bool dataFileCreated = await SaveData(dataBuffer, AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameData.GameName + "_Data.tar.gz");
                Decompress(AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameData.GameName + "_Data.tar.gz", dataPath, true, uploadForm);
            }
            if(savesPath != "")
            {
                uploadForm.uploadLabel.Text = (string)lang["downloading_saves_fragments"];
                uploadForm.uploadValueLabel.Text = "0%";
                uploadForm.uploadProgressBar.Value = 0;
                uploadForm.uploadProgressBar.Style = ProgressBarStyle.Continuous;
                byte[] savesDataBuffer = await ReadAllParts(gameData, false, uploadForm);
                uploadForm.uploadLabel.Text = (string)lang["decompressing_saves"];
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.uploadProgressBar.Value = 100;
                uploadForm.uploadProgressBar.Style = ProgressBarStyle.Marquee;
                bool savesFileCreated = await SaveData(savesDataBuffer, AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameData.GameName + "_Saves.tar.gz");
                Decompress(AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + gameData.GameName + "_Saves.tar.gz", savesPath, false, uploadForm);
            }
            uploadForm.uploadLabel.Text = (string)lang["restore_successful"];
            uploadForm.uploadValueLabel.Text = "";
            uploadForm.uploadProgressBar.Value = 100;
            uploadForm.uploadProgressBar.Style = ProgressBarStyle.Continuous;
            uploadForm.acceptButton.Enabled = true;
        }

        static async Task<byte[]> ReadAllParts(Game gameData, bool isGameData, UploadProgressForm uploadForm)
        {
            OnedriveHelper onedriveHelper = OnedriveHelper.Instance;
            int bytesRead = 0;
            byte[] buffer = new byte[isGameData ? gameData.DataSize : gameData.SaveSize];
            int partsNumber = isGameData ? gameData.DataParts : gameData.SavesParts;
            try
            {
                for (int i = 0; i < partsNumber; i++)
                {
                    byte[] data = await onedriveHelper.ReadItem("GameLinker/" + gameData.GameName + (isGameData ? "/data/" : "/saves/") + gameData.GameName + (isGameData ? "_Data" : "_Saves") + ".tar.gz.part_" + i);

                    data.CopyTo(buffer, bytesRead);
                    bytesRead += data.Length;
                    uploadForm.Invoke((MethodInvoker)delegate
                    {
                        int percentage = (int)(((float)bytesRead / (float)buffer.Length) * 100);
                        uploadForm.uploadProgressBar.Value = percentage;
                        uploadForm.uploadValueLabel.Text = percentage + "%";
                    });
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {

            }
            return buffer;
        }

        static async void Decompress(string gzArchiveName, string destFolder, bool isGameData, UploadProgressForm uploadForm)
        {

            Stream inStream = File.OpenRead(gzArchiveName);
            Stream gzipStream = new GZipInputStream(inStream);

            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            await Task.Run(() => {
                tarArchive.ExtractContents(destFolder);
            });
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
            File.Delete(gzArchiveName);
        }

        private static void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
        {
            TarEntry tarEntry = TarEntry.CreateEntryFromFile(sourceDirectory);
            tarArchive.WriteEntry(tarEntry, false);

            string[] filenames = Directory.GetFiles(sourceDirectory);
            foreach (string filename in filenames)
            {
                tarEntry = TarEntry.CreateEntryFromFile(filename);
                tarArchive.WriteEntry(tarEntry, true);
            }

            if (recurse)
            {
                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                    AddDirectoryFilesToTar(tarArchive, directory, recurse);
            }
        }

        public static async Task<bool> SaveData(byte[] data, string destination)
        {
            if (data.Length == 0) return false;
            try
            {
                await Task.Run(() => {
                    File.WriteAllBytes(destination, data);
                });
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
