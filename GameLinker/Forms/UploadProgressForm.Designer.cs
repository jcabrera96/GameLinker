namespace GameLinker.Forms
{
    partial class UploadProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadProgressForm));
            this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.acceptButton = new System.Windows.Forms.Button();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.uploadValueLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadProgressBar
            // 
            resources.ApplyResources(this.uploadProgressBar, "uploadProgressBar");
            this.uploadProgressBar.Name = "uploadProgressBar";
            this.uploadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // acceptButton
            // 
            resources.ApplyResources(this.acceptButton, "acceptButton");
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // uploadLabel
            // 
            resources.ApplyResources(this.uploadLabel, "uploadLabel");
            this.uploadLabel.Name = "uploadLabel";
            // 
            // uploadValueLabel
            // 
            resources.ApplyResources(this.uploadValueLabel, "uploadValueLabel");
            this.uploadValueLabel.Name = "uploadValueLabel";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.uploadLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uploadValueLabel, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // UploadProgressForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.uploadProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UploadProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UploadProgressForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ProgressBar uploadProgressBar;
        public System.Windows.Forms.Button acceptButton;
        public System.Windows.Forms.Label uploadValueLabel;
        public System.Windows.Forms.Label uploadLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}