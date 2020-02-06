using GameLinker.Forms;
using GameLinker.Models;
using Microsoft.OneDrive.Sdk;
using System;
using System.Collections.Generic;
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

        public static void SaveLibrary()
        {
            Serialize(Library, AppDomain.CurrentDomain.BaseDirectory + "Library.bin");
        }

        public static async Task LoadLibrary()
        {
            if (Library == null)
            {
                UploadProgressForm uploadForm = new UploadProgressForm();
                uploadForm.uploadLabel.Text = "Downloading library. Please wait.";
                uploadForm.Text = "Download progress";
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.uploadProgressBar.Value = 100;
                try
                {
                    Library = (GamesLibrary)Deserialize(AppDomain.CurrentDomain.BaseDirectory + "Library.bin");
                }
                catch (FileNotFoundException)
                {
                    DialogResult response = MessageBox.Show("No se ha encontrado la biblioteca de juegos.\n¿Desea tratar de descargar una copia en la nube?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if(response == DialogResult.Yes)
                    {
                        uploadForm.Show();
                        byte[] libraryData = await OnedriveHelper.Instance.ReadItem("GameLinker/Library.bin");
                        uploadForm.Hide();
                        if(libraryData == null)
                        {
                            Library = new GamesLibrary();
                            MessageBox.Show("No se ha encontrado la biblioteca de juegos.\nSe creará una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SaveLibrary();
                        }
                        else
                        {
                            Library = (GamesLibrary)DeserializeBytes(libraryData);
                            SaveLibrary();
                            MessageBox.Show("Biblioteca restaurada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Library = new GamesLibrary();
                        MessageBox.Show("No se ha encontrado la biblioteca de juegos.\nSe creará una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SaveLibrary();
                    }
                }
                catch (InvalidCastException)
                {
                    DialogResult response = MessageBox.Show("La biblioteca de juegos está corrupta o es inválida.\n¿Desea tratar de descargar una copia en la nube?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (response == DialogResult.Yes)
                    {
                        uploadForm.Show();
                        byte[] libraryData = await OnedriveHelper.Instance.ReadItem("GameLinker/Library.bin");
                        uploadForm.Hide();
                        if (libraryData == null)
                        {
                            Library = new GamesLibrary();
                            MessageBox.Show("No se ha encontrado la biblioteca de juegos.\nSe creará una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SaveLibrary();
                        }
                        else
                        {
                            Library = (GamesLibrary)DeserializeBytes(libraryData);
                            SaveLibrary();
                            MessageBox.Show("Biblioteca restaurada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Library = new GamesLibrary();
                        MessageBox.Show("No se ha encontrado la biblioteca de juegos.\nSe creará una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SaveLibrary();
                    }
                }
            }
        }
    }
}
