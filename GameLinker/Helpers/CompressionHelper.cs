using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    public class CompressionHelper
    {

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
                    await CompressFilePart(tarFilePath + ".part_" + fileIndex, new MemoryStream(data));
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
                TarOutputStream tarOutputStream = new TarOutputStream(outStream);

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

        static void Decompress(string gzArchiveName, string destFolder)
        {
            Stream inStream = File.OpenRead(gzArchiveName);
            Stream gzipStream = new GZipInputStream(inStream);

            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(destFolder);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
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

        public static Task<bool> SaveData(byte[] data, string destination)
        {
            try
            {
                File.WriteAllBytes(destination, data);
                return Task.FromResult(true);
            }
            catch (IOException)
            {
                return Task.FromResult(false);
            }
        }
    }
}
