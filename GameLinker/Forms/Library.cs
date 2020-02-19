using GameLinker.Helpers;
using GameLinker.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            SessionToogleLabel.Text = await OnedriveHelper.Instance.IsAuthenticated() ? "Logout" : "Login";
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
                Text = "Add game",
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
            libraryPanel.Columns.Add("Games", -2, HorizontalAlignment.Center);
            libraryPanel.Items.AddRange(gamesList.ToArray());
            libraryPanel.LargeImageList = gamesIconsList;
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
                DialogResult answer = MessageBox.Show("Are you sure you want to logout from OneDrive?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            SessionToogleLabel.Text = await OnedriveHelper.Instance.IsAuthenticated() ? "Logout" : "Login";
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
                        MessageBox.Show("You can't add a game to the library while not logged in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    NewGameForm addGameForm = new NewGameForm();
                    addGameForm.ShowDialog(this);
                    break;
            }
        }

        private async void ListItemHover(object sender, MouseEventArgs e)
        {
            ListViewItem listviewTest = libraryPanel.HitTest(e.X, e.Y).Item as ListViewItem;
            if (listviewTest != null)
            {
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
            }
            else if(listviewTest == null && lastItemHasDecreased == false)
            {
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
            }
        }
    }
}
