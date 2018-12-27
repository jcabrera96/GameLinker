using GameLinker.Models;
using Microsoft.OneDrive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    class LibraryHelper : Serializer
    {
        static GamesLibrary library;

        public static void SaveLibrary()
        {

        }

        static void LoadLibrary()
        {

        }

        public static void CheckForLibrary()
        {
            if (library == null)
            {

            }
        }
    }
}
