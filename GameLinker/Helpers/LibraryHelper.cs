using GameLinker.Forms;
using GameLinker.Models;
using Microsoft.OneDrive.Sdk;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker.Helpers
{
    public class LibraryHelper : Serializer
    {
        public static GamesLibrary Library { get; private set; }
        private static JObject lang = (JObject)LocalizationHelper.Instance.libraryHelperLocalization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];

        public static void SaveLibrary()
        {
            Serialize(Library, AppDomain.CurrentDomain.BaseDirectory + "Library.bin");
        }

        public static async Task LoadLibrary()
        {
            if (Library == null)
            {
                UploadProgressForm uploadForm = new UploadProgressForm();
                uploadForm.uploadLabel.Text = (string)lang["downloading_library"];
                uploadForm.Text = (string)lang["download_progress"];
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.uploadProgressBar.Value = 100;
                try
                {
                    Library = (GamesLibrary)Deserialize(AppDomain.CurrentDomain.BaseDirectory + "Library.bin");
                }
                catch (FileNotFoundException)
                {
                    DialogResult response = MessageBox.Show((string)lang["library_not_found"], (string)lang["warning"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if(response == DialogResult.Yes)
                    {
                        uploadForm.Show();
                        byte[] libraryData = await OnedriveHelper.Instance.ReadItem("GameLinker/Library.bin");
                        uploadForm.Hide();
                        if(libraryData == null)
                        {
                            Library = new GamesLibrary();
                            MessageBox.Show((string)lang["library_being_created"], (string)lang["warning"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SaveLibrary();
                        }
                        else
                        {
                            Library = (GamesLibrary)DeserializeBytes(libraryData);
                            SaveLibrary();
                            MessageBox.Show((string)lang["library_restored"], (string)lang["success"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Library = new GamesLibrary();
                        MessageBox.Show((string)lang["library_being_created"], (string)lang["warning"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SaveLibrary();
                    }
                }
                catch (InvalidCastException)
                {
                    DialogResult response = MessageBox.Show((string)lang["library_corrupted"], (string)lang["warning"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (response == DialogResult.Yes)
                    {
                        uploadForm.Show();
                        byte[] libraryData = await OnedriveHelper.Instance.ReadItem("GameLinker/Library.bin");
                        uploadForm.Hide();
                        if (libraryData == null)
                        {
                            Library = new GamesLibrary();
                            MessageBox.Show((string)lang["library_being_created"], (string)lang["warning"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SaveLibrary();
                        }
                        else
                        {
                            Library = (GamesLibrary)DeserializeBytes(libraryData);
                            SaveLibrary();
                            MessageBox.Show((string)lang["library_restored"], (string)lang["success"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Library = new GamesLibrary();
                        MessageBox.Show((string)lang["library_being_created"], (string)lang["warning"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SaveLibrary();
                    }
                }
            }
        }
    }
}
