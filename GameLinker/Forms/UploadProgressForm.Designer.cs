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
            this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.acceptButton = new System.Windows.Forms.Button();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.uploadValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uploadProgressBar
            // 
            this.uploadProgressBar.Location = new System.Drawing.Point(12, 149);
            this.uploadProgressBar.Name = "uploadProgressBar";
            this.uploadProgressBar.Size = new System.Drawing.Size(713, 23);
            this.uploadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.uploadProgressBar.TabIndex = 0;
            // 
            // acceptButton
            // 
            this.acceptButton.Enabled = false;
            this.acceptButton.Location = new System.Drawing.Point(332, 188);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 1;
            this.acceptButton.Text = "Aceptar";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // uploadLabel
            // 
            this.uploadLabel.AutoSize = true;
            this.uploadLabel.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadLabel.Location = new System.Drawing.Point(6, 41);
            this.uploadLabel.Name = "uploadLabel";
            this.uploadLabel.Size = new System.Drawing.Size(261, 34);
            this.uploadLabel.TabIndex = 2;
            this.uploadLabel.Text = "Upload progress: ";
            // 
            // uploadValueLabel
            // 
            this.uploadValueLabel.AutoSize = true;
            this.uploadValueLabel.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadValueLabel.Location = new System.Drawing.Point(273, 41);
            this.uploadValueLabel.Name = "uploadValueLabel";
            this.uploadValueLabel.Size = new System.Drawing.Size(49, 34);
            this.uploadValueLabel.TabIndex = 3;
            this.uploadValueLabel.Text = "0%";
            // 
            // UploadProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 223);
            this.Controls.Add(this.uploadValueLabel);
            this.Controls.Add(this.uploadLabel);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.uploadProgressBar);
            this.Name = "UploadProgressForm";
            this.Text = "Upload progress";
            this.Load += new System.EventHandler(this.UploadProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar uploadProgressBar;
        public System.Windows.Forms.Button acceptButton;
        public System.Windows.Forms.Label uploadValueLabel;
        public System.Windows.Forms.Label uploadLabel;
    }
}