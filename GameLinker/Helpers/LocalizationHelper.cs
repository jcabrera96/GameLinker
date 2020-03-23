using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    public class LocalizationHelper
    {
        private static LocalizationHelper _instance;

        public JObject libraryLocalization, newGameFormLocalization, onedriveHelperLocalization, libraryHelperLocalization;

        public static LocalizationHelper Instance
        {
            get
            {
                if (_instance == null) _instance = new LocalizationHelper();
                return _instance;
            }
        }

        private LocalizationHelper()
        {
            var assembly = Assembly.GetExecutingAssembly();
            String[] resourcesName = { "Library.json", "OnedriveHelper.json", "LibraryHelper.json" };

            foreach (var resource in resourcesName)
            {
                using (Stream stream = assembly.GetManifestResourceStream("GameLinker.DynamicLang." + resource))
                using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                {
                    SetLocalization(reader.ReadToEnd(), resource);
                }
            }
        }

        private void SetLocalization(string json, string fileName)
        {
            switch (fileName)
            {
                case "Library.json":
                    libraryLocalization = JObject.Parse(json);
                    break;
                case "OnedriveHelper.json":
                    onedriveHelperLocalization = JObject.Parse(json);
                    break;
                case "LibraryHelper.json":
                    libraryHelperLocalization = JObject.Parse(json);
                    break;
            }
        }
    }
}
