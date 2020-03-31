namespace GameLinker
{
    partial class NewGameForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameForm));
            this.dataFolderSelectionDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.savesFolderSelectionDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dataPathLabel = new System.Windows.Forms.Label();
            this.savesPathLabel = new System.Windows.Forms.Label();
            this.dataPathTextBox = new System.Windows.Forms.TextBox();
            this.savesPathTextBox = new System.Windows.Forms.TextBox();
            this.dataPathSelectionButton = new System.Windows.Forms.Button();
            this.savesPathSelectionButton = new System.Windows.Forms.Button();
            this.addGameButton = new System.Windows.Forms.Button();
            this.gameNameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OnedriveFolderOpen = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // dataFolderSelectionDialog
            // 
            resources.ApplyResources(this.dataFolderSelectionDialog, "dataFolderSelectionDialog");
            this.dataFolderSelectionDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // savesFolderSelectionDialog
            // 
            resources.ApplyResources(this.savesFolderSelectionDialog, "savesFolderSelectionDialog");
            this.savesFolderSelectionDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // dataPathLabel
            // 
            resources.ApplyResources(this.dataPathLabel, "dataPathLabel");
            this.dataPathLabel.Name = "dataPathLabel";
            // 
            // savesPathLabel
            // 
            resources.ApplyResources(this.savesPathLabel, "savesPathLabel");
            this.savesPathLabel.Name = "savesPathLabel";
            // 
            // dataPathTextBox
            // 
            resources.ApplyResources(this.dataPathTextBox, "dataPathTextBox");
            this.dataPathTextBox.Name = "dataPathTextBox";
            this.dataPathTextBox.ReadOnly = true;
            // 
            // savesPathTextBox
            // 
            resources.ApplyResources(this.savesPathTextBox, "savesPathTextBox");
            this.savesPathTextBox.Name = "savesPathTextBox";
            this.savesPathTextBox.ReadOnly = true;
            // 
            // dataPathSelectionButton
            // 
            resources.ApplyResources(this.dataPathSelectionButton, "dataPathSelectionButton");
            this.dataPathSelectionButton.Name = "dataPathSelectionButton";
            this.dataPathSelectionButton.UseVisualStyleBackColor = true;
            this.dataPathSelectionButton.Click += new System.EventHandler(this.dataPathSelectionButton_Click);
            // 
            // savesPathSelectionButton
            // 
            resources.ApplyResources(this.savesPathSelectionButton, "savesPathSelectionButton");
            this.savesPathSelectionButton.Name = "savesPathSelectionButton";
            this.savesPathSelectionButton.UseVisualStyleBackColor = true;
            this.savesPathSelectionButton.Click += new System.EventHandler(this.savesPathSelectionButton_Click);
            // 
            // addGameButton
            // 
            resources.ApplyResources(this.addGameButton, "addGameButton");
            this.addGameButton.Name = "addGameButton";
            this.addGameButton.UseVisualStyleBackColor = true;
            this.addGameButton.Click += new System.EventHandler(this.addGameButton_Click);
            // 
            // gameNameTextbox
            // 
            resources.ApplyResources(this.gameNameTextbox, "gameNameTextbox");
            this.gameNameTextbox.Name = "gameNameTextbox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // NewGameForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gameNameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addGameButton);
            this.Controls.Add(this.savesPathSelectionButton);
            this.Controls.Add(this.dataPathSelectionButton);
            this.Controls.Add(this.savesPathTextBox);
            this.Controls.Add(this.dataPathTextBox);
            this.Controls.Add(this.savesPathLabel);
            this.Controls.Add(this.dataPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGameForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.NewGameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog dataFolderSelectionDialog;
        private System.Windows.Forms.FolderBrowserDialog savesFolderSelectionDialog;
        private System.Windows.Forms.Label dataPathLabel;
        private System.Windows.Forms.Label savesPathLabel;
        private System.Windows.Forms.TextBox dataPathTextBox;
        private System.Windows.Forms.TextBox savesPathTextBox;
        private System.Windows.Forms.Button dataPathSelectionButton;
        private System.Windows.Forms.Button savesPathSelectionButton;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.TextBox gameNameTextbox;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker OnedriveFolderOpen;
    }
}

