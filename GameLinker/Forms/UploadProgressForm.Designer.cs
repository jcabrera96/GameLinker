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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadProgressBar
            // 
            this.uploadProgressBar.Location = new System.Drawing.Point(16, 183);
            this.uploadProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.uploadProgressBar.Name = "uploadProgressBar";
            this.uploadProgressBar.Size = new System.Drawing.Size(951, 28);
            this.uploadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.uploadProgressBar.TabIndex = 0;
            // 
            // acceptButton
            // 
            this.acceptButton.Enabled = false;
            this.acceptButton.Location = new System.Drawing.Point(443, 231);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(4);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(100, 28);
            this.acceptButton.TabIndex = 1;
            this.acceptButton.Text = "Aceptar";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // uploadLabel
            // 
            this.uploadLabel.AutoSize = true;
            this.uploadLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadLabel.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadLabel.Location = new System.Drawing.Point(4, 0);
            this.uploadLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uploadLabel.Name = "uploadLabel";
            this.uploadLabel.Size = new System.Drawing.Size(943, 44);
            this.uploadLabel.TabIndex = 2;
            this.uploadLabel.Text = "Upload progress...";
            this.uploadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uploadValueLabel
            // 
            this.uploadValueLabel.AutoSize = true;
            this.uploadValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadValueLabel.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadValueLabel.Location = new System.Drawing.Point(4, 44);
            this.uploadValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uploadValueLabel.Name = "uploadValueLabel";
            this.uploadValueLabel.Size = new System.Drawing.Size(943, 44);
            this.uploadValueLabel.TabIndex = 3;
            this.uploadValueLabel.Text = "0%";
            this.uploadValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.38276F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.61724F));
            this.tableLayoutPanel1.Controls.Add(this.uploadLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uploadValueLabel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(951, 88);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // UploadProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 274);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.uploadProgressBar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UploadProgressForm";
            this.Text = "Upload progress";
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