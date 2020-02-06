using GameLinker.Helpers;
using GameLinker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;

namespace GameLinker.Forms
{
    public partial class Library : Form
    {
        private bool ShowingSidebar = false;

        public Library()
        {
            InitializeComponent();
            ImageList gamesIconsList = new ImageList();
            gamesIconsList.ImageSize = new Size(128, 128);
            List<ListViewItem> gamesList = new List<ListViewItem>();
            gamesList.Add(new ListViewItem {
                Text = "Add game",
                Tag = -1,
                ImageIndex = 0
            });
            gamesIconsList.Images.Add(Resources.add_game);
            foreach (var game in LibraryHelper.Library.GetGames())
            {
                ListViewItem gamesListItem = new ListViewItem {
                    Text = game.GameName,
                    Tag = LibraryHelper.Library.GetGames().IndexOf(game),
                    ImageIndex = gamesIconsList.Images.Count
                };
                gamesList.Add(gamesListItem);
                gamesIconsList.Images.Add(Resources.generic_game);
            }
            libraryPanel.Columns.Add("Games", -2, HorizontalAlignment.Center);
            libraryPanel.Items.AddRange(gamesList.ToArray());
            libraryPanel.LargeImageList = gamesIconsList;
        }

        private void ToogleSidebar()
        {
            Transition clickAnimation = new Transition(new TransitionType_Bounce(200));
            clickAnimation.add(menuButton, "Top", (int)Math.Round(menuButton.Top * 1.5));
            if (ShowingSidebar)
            {
                ShowingSidebar = false;
                Transition mainAnimation = new Transition(new TransitionType_CriticalDamping(500));
                mainAnimation.add(sidebar, "Left", -200);
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

        private void MenuButton_Click(object sender, EventArgs e)
        {
            ToogleSidebar();
        }
    }
}
