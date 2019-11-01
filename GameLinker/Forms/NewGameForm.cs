using GameLinker.Forms;
using GameLinker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public NewGameForm()
        {
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
                UploadProgressForm uploadForm = new UploadProgressForm();
                if (dataPath != "") item.DataSize = await onedriveManager.UploadFolder(dataPath, "GameLinker/" + gameName + "/", uploadForm, gameName);
                if (savesPath != "") item.SaveSize = await onedriveManager.UploadFolder(savesPath, "GameLinker/" + gameName + "/", uploadForm, gameName, false);
                uploadForm.uploadLabel.Text = "Updating game library";
                uploadForm.uploadValueLabel.Text = "";
                LibraryHelper.Library.AddGame(item);
                LibraryHelper.SaveLibrary();
                uploadForm.uploadLabel.Text = "Uploading updated game library";
                uploadForm.uploadValueLabel.Text = "0%";
                onedriveManager.compressedFilesCount = 1;
                onedriveManager.uploadedCompressedFiles = 0;
                await onedriveManager.UploadItem(AppDomain.CurrentDomain.BaseDirectory + "Library.bin", "GameLinker/" + "Library.bin", uploadForm);
                uploadForm.uploadLabel.Text = "Game files uploaded successfully.";
                uploadForm.uploadValueLabel.Text = "";
                uploadForm.acceptButton.Enabled = true;
            }
            catch(ArgumentNullException err)
            {
                MessageBox.Show(this,err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);}
            }
    }
}
