using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    class FileToStreamHelper
    {
        public static Stream GetFileStream(string filePath)
        {
            Stream fs = File.OpenRead(filePath);
            return fs;
        }
    }
}
