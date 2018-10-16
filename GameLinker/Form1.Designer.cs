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
            this.dataFolderSelectionDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.savesFolderSelectionDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dataPathLabel = new System.Windows.Forms.Label();
            this.savesPathLabel = new System.Windows.Forms.Label();
            this.dataPathTextBox = new System.Windows.Forms.TextBox();
            this.savesPathTextBox = new System.Windows.Forms.TextBox();
            this.dataPathSelectionButton = new System.Windows.Forms.Button();
            this.savesPathSelectionButton = new System.Windows.Forms.Button();
            this.addGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataFolderSelectionDialog
            // 
            this.dataFolderSelectionDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // savesFolderSelectionDialog
            // 
            this.savesFolderSelectionDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // dataPathLabel
            // 
            this.dataPathLabel.AutoSize = true;
            this.dataPathLabel.Location = new System.Drawing.Point(12, 14);
            this.dataPathLabel.Name = "dataPathLabel";
            this.dataPathLabel.Size = new System.Drawing.Size(86, 13);
            this.dataPathLabel.TabIndex = 0;
            this.dataPathLabel.Text = "Game data path:";
            // 
            // savesPathLabel
            // 
            this.savesPathLabel.AutoSize = true;
            this.savesPathLabel.Location = new System.Drawing.Point(12, 46);
            this.savesPathLabel.Name = "savesPathLabel";
            this.savesPathLabel.Size = new System.Drawing.Size(88, 13);
            this.savesPathLabel.TabIndex = 1;
            this.savesPathLabel.Text = "Saves data path:";
            // 
            // dataPathTextBox
            // 
            this.dataPathTextBox.Location = new System.Drawing.Point(104, 11);
            this.dataPathTextBox.Name = "dataPathTextBox";
            this.dataPathTextBox.ReadOnly = true;
            this.dataPathTextBox.Size = new System.Drawing.Size(195, 20);
            this.dataPathTextBox.TabIndex = 2;
            // 
            // savesPathTextBox
            // 
            this.savesPathTextBox.Location = new System.Drawing.Point(104, 43);
            this.savesPathTextBox.Name = "savesPathTextBox";
            this.savesPathTextBox.ReadOnly = true;
            this.savesPathTextBox.Size = new System.Drawing.Size(195, 20);
            this.savesPathTextBox.TabIndex = 3;
            // 
            // dataPathSelectionButton
            // 
            this.dataPathSelectionButton.Location = new System.Drawing.Point(305, 9);
            this.dataPathSelectionButton.Name = "dataPathSelectionButton";
            this.dataPathSelectionButton.Size = new System.Drawing.Size(30, 23);
            this.dataPathSelectionButton.TabIndex = 4;
            this.dataPathSelectionButton.Text = "...";
            this.dataPathSelectionButton.UseVisualStyleBackColor = true;
            this.dataPathSelectionButton.Click += new System.EventHandler(this.dataPathSelectionButton_Click);
            // 
            // savesPathSelectionButton
            // 
            this.savesPathSelectionButton.Location = new System.Drawing.Point(305, 41);
            this.savesPathSelectionButton.Name = "savesPathSelectionButton";
            this.savesPathSelectionButton.Size = new System.Drawing.Size(30, 23);
            this.savesPathSelectionButton.TabIndex = 5;
            this.savesPathSelectionButton.Text = "...";
            this.savesPathSelectionButton.UseVisualStyleBackColor = true;
            this.savesPathSelectionButton.Click += new System.EventHandler(this.savesPathSelectionButton_Click);
            // 
            // addGameButton
            // 
            this.addGameButton.Location = new System.Drawing.Point(224, 85);
            this.addGameButton.Name = "addGameButton";
            this.addGameButton.Size = new System.Drawing.Size(75, 23);
            this.addGameButton.TabIndex = 6;
            this.addGameButton.Text = "Add game";
            this.addGameButton.UseVisualStyleBackColor = true;
            this.addGameButton.Click += new System.EventHandler(this.addGameButton_Click);
            // 
            // NewGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 120);
            this.Controls.Add(this.addGameButton);
            this.Controls.Add(this.savesPathSelectionButton);
            this.Controls.Add(this.dataPathSelectionButton);
            this.Controls.Add(this.savesPathTextBox);
            this.Controls.Add(this.dataPathTextBox);
            this.Controls.Add(this.savesPathLabel);
            this.Controls.Add(this.dataPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGameForm";
            this.Text = "Add new game";
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
    }
}

