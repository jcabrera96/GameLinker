using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker
{
    public partial class NewGameForm : Form
    {
        public NewGameForm()
        {
            InitializeComponent();
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

        private void addGameButton_Click(object sender, EventArgs e)
        {
            string savesPath = savesPathTextBox.Text, dataPath = dataPathTextBox.Text;
            try
            {
                Game item = savesPath != "" && dataPath != "" ? new Game(savesPath, dataPath) : savesPath != "" ? new Game(savesPath) : dataPath != "" ? new Game(dataPath) : new Game();
                Console.WriteLine(item.ToString());
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine("Can't add a game without paths");
            }
        }
    }
}
