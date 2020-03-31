using GameLinker.Forms;
using GameLinker.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker
{
    public partial class NewGameForm : Form
    {
        OnedriveHelper onedriveManager;
        Library.CallBack callback;
        JObject lang = (JObject)LocalizationHelper.Instance.newGameFormLocalization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];

        public NewGameForm(Library.CallBack callback)
        {
            this.callback = callback;
            InitializeComponent();
            onedriveManager = OnedriveHelper.Instance;
        }

        private void NewGameForm_Load(object sender, EventArgs e)
        {

        }

        private void dataPathSelectionButton_Click(object sender, EventArgs e)
        {
            dataFolderSelectionDialog.ShowDialog();
            dataPathTextBox.Text = dataFolderSelectionDialog.SelectedPath;
        }

        private void savesPathSelectionButton_Click(object sender, EventArgs e)
        {
            savesFolderSelectionDialog.ShowDialog();
            savesPathTextBox.Text = savesFolderSelectionDialog.SelectedPath;
        }

        private async void addGameButton_Click(object sender, EventArgs e)
        {
            string savesPath = savesPathTextBox.Text, dataPath = dataPathTextBox.Text, gameName = gameNameTextbox.Text;
            try
            {
                Game item = new Game(savesPath, dataPath, gameName);
                UploadProgressForm uploadForm = new UploadProgressForm(callback);
                uploadForm.Show(Owner);
                Hide();
                if (dataPath != "") {
                    int[] filesData = await onedriveManager.UploadFolder(dataPath, "GameLinker/" + gameName + "/", uploadForm, gameName);
                    item.DataParts = filesData[1];
                    item.DataSize = filesData[0];
                }
                if (savesPath != "")
                {
                    int[] savesData = await onedriveManager.UploadFolder(dataPath, "GameLinker/" + gameName + "/", uploadForm, gameName);
                    item.SavesParts = savesData[1];
                    item.SaveSize = savesData[0];
                }
                uploadForm.uploadLabel.Text = (string)lang["updating_library"];
                uploadForm.uploadValueLabel.Text = "";
                LibraryHelper.Library.AddGame(item);
                LibraryHelper.SaveLibrary();
                uploadForm.uploadLabel.Text = (string)lang["uploading_updated_library"];
                uploadForm.uploadValueLabel.Text = "0%";
                onedriveManager.compressedFilesCount = 1;
                onedriveManager.uploadedCompressedFiles = 0;
                await onedriveManager.UploadItem(AppDomain.CurrentDomain.BaseDirectory + "Library.bin", "GameLinker/" + "Library.bin", uploadForm);
                uploadForm.uploadLabel.Text = (string)lang["upload_successful"];
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.acceptButton.Enabled = true;
                Close();
            }
            catch(ArgumentNullException err)
            {
                MessageBox.Show(this,err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);}
            }
    }
}
