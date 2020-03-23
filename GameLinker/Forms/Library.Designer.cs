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
            this.SessionToogleLabel = new System.Windows.Forms.Label();
            this.libraryPanel = new System.Windows.Forms.ListView();
            this.menuButton = new System.Windows.Forms.PictureBox();
            this.sessionToogleButton = new System.Windows.Forms.PictureBox();
            this.sidebar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionToogleButton)).BeginInit();
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
            resources.ApplyResources(this.menuButton, "menuButton");
            this.menuButton.Image = global::GameLinker.Properties.Resources.sidebar_inactive;
            this.menuButton.Name = "menuButton";
            this.menuButton.TabStop = false;
            this.menuButton.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // sessionToogleButton
            // 
            resources.ApplyResources(this.sessionToogleButton, "sessionToogleButton");
            this.sessionToogleButton.Image = global::GameLinker.Properties.Resources.logout;
            this.sessionToogleButton.Name = "sessionToogleButton";
            this.sessionToogleButton.TabStop = false;
            // 
            // Library
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.libraryPanel);
            this.Controls.Add(this.menuButton);
            this.Controls.Add(this.sidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Library";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.sidebar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionToogleButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.PictureBox menuButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox sessionToogleButton;
        private System.Windows.Forms.Label SessionToogleLabel;
        private System.Windows.Forms.ListView libraryPanel;
    }
}