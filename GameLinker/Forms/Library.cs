using GameLinker.Helpers;
using GameLinker.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Transitions;

namespace GameLinker.Forms
{
    public partial class Library : Form
    {
        private bool ShowingSidebar = false;
        private ImageList gamesIconsList;
        private List<ListViewItem> gamesList;
        private ListViewItem lastItemHovered;
        private bool lastItemHasDecreased = true;
        private JObject lang = (JObject)LocalizationHelper.Instance.libraryLocalization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];

        public delegate void CallBack();

        public Library()
        {
            InitializeComponent();
            InitializeLibrary();
        }

        #region Initialization

        private async void InitializeLibrary()
        {
            await LibraryHelper.LoadLibrary();
            GenerateGamesList();
            libraryPanel.Click += ListItemClicked;
            libraryPanel.MouseMove += ListItemHover;
            libraryPanel.ItemSelectionChanged += ListViewItemSelectionChanged;
            sessionToogleButton.Image = await OnedriveHelper.Instance.IsAuthenticated() ? Resources.logout : Resources.login;
            SessionToogleLabel.Text = (string)lang[await OnedriveHelper.Instance.IsAuthenticated() ? "log_out" : "log_in"];
            sessionToogleButton.MouseEnter += SessionToogleButtonEnter;
            sessionToogleButton.MouseLeave += SessionToogleButtonExit;
            sessionToogleButton.Click += SessionToogle;
        }

        private async void GenerateGamesList()
        {
            gamesIconsList = new ImageList();
            gamesIconsList.ImageSize = new Size(128, 128);
            gamesList = new List<ListViewItem>();
            gamesList.Add(new ListViewItem
            {
                Text = (string)lang["add_game"],
                Tag = -1,
                ImageKey = "0"
            });
            gamesIconsList.Images.Add("0", await OnedriveHelper.Instance.IsAuthenticated() ? Resources.add_game : Resources.add_game_disabled);
            foreach (var game in LibraryHelper.Library.GetGames())
            {
                ListViewItem gamesListItem = new ListViewItem
                {
                    Text = game.GameName,
                    Tag = LibraryHelper.Library.GetGames().IndexOf(game),
                    ImageKey = gamesIconsList.Images.Count.ToString()
                };
                gamesList.Add(gamesListItem);
                gamesIconsList.Images.Add(gamesIconsList.Images.Count.ToString(), Resources.generic_game);
            }
            libraryPanel.Items.Clear();
            libraryPanel.Columns.Clear();
            libraryPanel.Columns.Add("Games", -2, HorizontalAlignment.Center);
            libraryPanel.Items.AddRange(gamesList.ToArray());
            libraryPanel.LargeImageList = gamesIconsList;
            libraryPanel.Refresh();
        }

        #endregion

        #region Sidebar

        private void MenuButton_Click(object sender, EventArgs e)
        {
            ToogleSidebar();
        }

        private void ToogleSidebar()
        {
            Transition clickAnimation = new Transition(new TransitionType_Bounce(200));
            clickAnimation.add(menuButton, "Top", (int)Math.Round(menuButton.Top * 1.5));
            if (ShowingSidebar)
            {
                ShowingSidebar = false;
                Transition mainAnimation = new Transition(new TransitionType_CriticalDamping(500));
                mainAnimation.add(sidebar, "Left", -sidebar.Width);
                mainAnimation.add(libraryPanel, "Width", libraryPanel.Width + sidebar.Width);
                mainAnimation.add(libraryPanel, "Left", libraryPanel.Left - sidebar.Width);
                mainAnimation.add(menuButton, "Left", menuButton.Left - sidebar.Width);
                Transition.runChain(clickAnimation, mainAnimation);
                menuButton.Image = Resources.sidebar_inactive;
            }
            else
            {
                ShowingSidebar = true;
                Transition mainAnimation = new Transition(new TransitionType_CriticalDamping(500));
                mainAnimation.add(sidebar, "Left", 0);
                mainAnimation.add(libraryPanel, "Width", libraryPanel.Width - sidebar.Width);
                mainAnimation.add(libraryPanel, "Left", libraryPanel.Left + sidebar.Width);
                mainAnimation.add(menuButton, "Left", menuButton.Left + sidebar.Width);
                Transition.runChain(clickAnimation, mainAnimation);
                menuButton.Image = Resources.sidebar_active;
            }
        }

        private async void SessionToogleButtonEnter(object sender, EventArgs e)
        {
            sessionToogleButton.Image = await OnedriveHelper.Instance.IsAuthenticated() ? Resources.logout_active : Resources.login_active;
        }

        private async void SessionToogleButtonExit(object sender, EventArgs e)
        {
            sessionToogleButton.Image = await OnedriveHelper.Instance.IsAuthenticated() ? Resources.logout : Resources.login;
        }

        private async void SessionToogle(object sender, EventArgs e)
        {
            if (await OnedriveHelper.Instance.IsAuthenticated())
            {
                DialogResult answer = MessageBox.Show((string)lang["log_out_confirm"], (string)lang["warning"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes) await OnedriveHelper.Instance.EndSession();
            }
            else
            {
                await OnedriveHelper.Instance.Authenticate();
            }
            gamesIconsList.Images.RemoveByKey("0");
            gamesIconsList.Images.Add("0", await OnedriveHelper.Instance.IsAuthenticated() ? Resources.add_game : Resources.add_game_disabled);
            libraryPanel.Refresh();
            sessionToogleButton.Image = await OnedriveHelper.Instance.IsAuthenticated() ? Resources.logout : Resources.login;
            SessionToogleLabel.Text = (string)lang[await OnedriveHelper.Instance.IsAuthenticated() ? "log_out" : "log_in"];
        }

        #endregion

        private void ListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                e.Item.Selected = false;
            }
        }

        private async void ListItemClicked(object sender, EventArgs e)
        {
            switch ((int)lastItemHovered.Tag)
            {
                case -1:
                    if (!await OnedriveHelper.Instance.IsAuthenticated())
                    {
                        MessageBox.Show(
                            (string)lang["add_game_disabled"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    NewGameForm addGameForm = new NewGameForm(GenerateGamesList);
                    addGameForm.ShowDialog(this);
                    break;
                default:
                    if (!await OnedriveHelper.Instance.IsAuthenticated())
                    {
                        MessageBox.Show(
                            (string)lang["restore_disabled"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Game selectedGame = LibraryHelper.Library.GetGames()[(int)lastItemHovered.Tag];
                    string message = (string)lang["restore_game"] + Environment.NewLine + Environment.NewLine + (string)lang["data_path"] +
                        Environment.NewLine + (selectedGame.DataPath == "" ? (string)lang["none"] : selectedGame.DataPath) + Environment.NewLine +
                        Environment.NewLine + (string)lang["saves_path"] + Environment.NewLine + (selectedGame.SavePath == "" ? (string)lang["none"] : selectedGame.SavePath);
                    DialogResult answer = MessageBox.Show(message, (string)lang["warning"], MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (answer == DialogResult.Yes)
                    {
                        UploadProgressForm uploadForm = new UploadProgressForm();
                        uploadForm.Owner = this;
                        await CompressionHelper.JoinAndDecompress(selectedGame, uploadForm, selectedGame.DataPath, selectedGame.SavePath);
                    }
                    else if(answer == DialogResult.No)
                    {
                        UploadProgressForm uploadForm = new UploadProgressForm();
                        uploadForm.Owner = this;
                        string dataPath = "", savesPath = "";
                        if(selectedGame.DataPath != "")
                        {
                            DialogResult dataResult = dataFolderBrowserDialog.ShowDialog();
                            if (dataResult == DialogResult.OK) dataPath = dataFolderBrowserDialog.SelectedPath;
                        }
                        if (selectedGame.SavePath != "")
                        {
                            DialogResult savesResult = savesFolderBrowserDialog.ShowDialog();
                            if (savesResult == DialogResult.OK) savesPath = savesFolderBrowserDialog.SelectedPath;
                        }
                        await CompressionHelper.JoinAndDecompress(selectedGame, uploadForm, dataPath, savesPath);
                    }
                    break;
            }
        }

        private async void ListItemHover(object sender, MouseEventArgs e)
        {
            ListViewItem listviewTest = libraryPanel.HitTest(e.X, e.Y).Item as ListViewItem;
            if (listviewTest != null)
            {
                libraryPanel.BeginUpdate();
                Cursor.Current = Cursors.Hand;
                if(lastItemHovered == null || lastItemHovered != listviewTest)
                {
                    lastItemHovered = listviewTest;
                    lastItemHasDecreased = false;
                    gamesIconsList.Images.RemoveByKey(lastItemHovered.ImageKey);
                    if(lastItemHovered.ImageKey == "0")
                    {
                        gamesIconsList.Images.Add(lastItemHovered.ImageKey, await OnedriveHelper.Instance.IsAuthenticated() ? Resources.add_game_selected : Resources.add_game_disabled);
                    }
                    else
                    {
                        gamesIconsList.Images.Add(lastItemHovered.ImageKey, Resources.generic_game_selected);
                    }
                }
                libraryPanel.EndUpdate();
            }
            else if(listviewTest == null && lastItemHasDecreased == false)
            {
                libraryPanel.BeginUpdate();
                Cursor.Current = Cursors.Default;
                lastItemHasDecreased = true;
                gamesIconsList.Images.RemoveByKey(lastItemHovered.ImageKey);
                if (lastItemHovered.ImageKey == "0")
                {
                    gamesIconsList.Images.Add(lastItemHovered.ImageKey, await OnedriveHelper.Instance.IsAuthenticated() ? Resources.add_game : Resources.add_game_disabled);
                }
                else
                {
                    gamesIconsList.Images.Add(lastItemHovered.ImageKey, Resources.generic_game);
                }
                lastItemHovered = null;
                libraryPanel.EndUpdate();
            }
        }
    }
}
