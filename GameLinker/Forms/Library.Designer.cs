namespace GameLinker.Forms
{
    partial class Library
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Library));
            this.sidebar = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sessionToogleButton = new System.Windows.Forms.PictureBox();
            this.SessionToogleLabel = new System.Windows.Forms.Label();
            this.libraryPanel = new System.Windows.Forms.ListView();
            this.menuButton = new System.Windows.Forms.PictureBox();
            this.savesFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dataFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.totalSpaceLabel = new System.Windows.Forms.Label();
            this.freeSpaceLabel = new System.Windows.Forms.Label();
            this.sidebar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionToogleButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuButton)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            resources.ApplyResources(this.sidebar, "sidebar");
            this.sidebar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sidebar.Controls.Add(this.tableLayoutPanel1);
            this.sidebar.Name = "sidebar";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.sessionToogleButton, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.SessionToogleLabel, 1, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // sessionToogleButton
            // 
            resources.ApplyResources(this.sessionToogleButton, "sessionToogleButton");
            this.sessionToogleButton.Image = global::GameLinker.Properties.Resources.logout;
            this.sessionToogleButton.Name = "sessionToogleButton";
            this.sessionToogleButton.TabStop = false;
            // 
            // SessionToogleLabel
            // 
            resources.ApplyResources(this.SessionToogleLabel, "SessionToogleLabel");
            this.SessionToogleLabel.Name = "SessionToogleLabel";
            // 
            // libraryPanel
            // 
            resources.ApplyResources(this.libraryPanel, "libraryPanel");
            this.libraryPanel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.libraryPanel.HideSelection = false;
            this.libraryPanel.MultiSelect = false;
            this.libraryPanel.Name = "libraryPanel";
            this.libraryPanel.UseCompatibleStateImageBehavior = false;
            // 
            // menuButton
            // 
            this.menuButton.Image = global::GameLinker.Properties.Resources.sidebar_active;
            resources.ApplyResources(this.menuButton, "menuButton");
            this.menuButton.Name = "menuButton";
            this.menuButton.TabStop = false;
            this.menuButton.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // savesFolderBrowserDialog
            // 
            resources.ApplyResources(this.savesFolderBrowserDialog, "savesFolderBrowserDialog");
            this.savesFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // dataFolderBrowserDialog
            // 
            resources.ApplyResources(this.dataFolderBrowserDialog, "dataFolderBrowserDialog");
            this.dataFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // totalSpaceLabel
            // 
            resources.ApplyResources(this.totalSpaceLabel, "totalSpaceLabel");
            this.totalSpaceLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.totalSpaceLabel.Name = "totalSpaceLabel";
            this.totalSpaceLabel.Click += new System.EventHandler(this.totalSpaceLabel_Click);
            // 
            // freeSpaceLabel
            // 
            resources.ApplyResources(this.freeSpaceLabel, "freeSpaceLabel");
            this.freeSpaceLabel.ForeColor = System.Drawing.Color.ForestGreen;
            this.freeSpaceLabel.Name = "freeSpaceLabel";
            // 
            // Library
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.freeSpaceLabel);
            this.Controls.Add(this.totalSpaceLabel);
            this.Controls.Add(this.libraryPanel);
            this.Controls.Add(this.menuButton);
            this.Controls.Add(this.sidebar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Library";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Library_Load);
            this.sidebar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionToogleButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.PictureBox menuButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox sessionToogleButton;
        private System.Windows.Forms.Label SessionToogleLabel;
        private System.Windows.Forms.ListView libraryPanel;
        private System.Windows.Forms.FolderBrowserDialog savesFolderBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog dataFolderBrowserDialog;
        private System.Windows.Forms.Label totalSpaceLabel;
        private System.Windows.Forms.Label freeSpaceLabel;
    }
}