namespace FindCloud
{
    partial class OverwriteFile
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
            this.overwriteFileLabel = new System.Windows.Forms.Label();
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.overwrightAllCheckBox = new System.Windows.Forms.CheckBox();
            this.keepAllCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // overwriteFileLabel
            // 
            this.overwriteFileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overwriteFileLabel.AutoEllipsis = true;
            this.overwriteFileLabel.AutoSize = true;
            this.overwriteFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.overwriteFileLabel.Location = new System.Drawing.Point(14, 9);
            this.overwriteFileLabel.MaximumSize = new System.Drawing.Size(270, 0);
            this.overwriteFileLabel.Name = "overwriteFileLabel";
            this.overwriteFileLabel.Size = new System.Drawing.Size(253, 20);
            this.overwriteFileLabel.TabIndex = 0;
            this.overwriteFileLabel.Text = "Вы хотите перезаписать файл?";
            this.overwriteFileLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(18, 132);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "Да";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // noButton
            // 
            this.noButton.Location = new System.Drawing.Point(166, 132);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 3;
            this.noButton.Text = "Нет";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // overwrightAllCheckBox
            // 
            this.overwrightAllCheckBox.AutoSize = true;
            this.overwrightAllCheckBox.Location = new System.Drawing.Point(18, 109);
            this.overwrightAllCheckBox.Name = "overwrightAllCheckBox";
            this.overwrightAllCheckBox.Size = new System.Drawing.Size(120, 17);
            this.overwrightAllCheckBox.TabIndex = 4;
            this.overwrightAllCheckBox.Text = "Перезаписать все";
            this.overwrightAllCheckBox.UseVisualStyleBackColor = true;
            this.overwrightAllCheckBox.CheckedChanged += new System.EventHandler(this.OverwrightAllCheckBox_CheckedChanged);
            // 
            // keepAllCheckBox
            // 
            this.keepAllCheckBox.AutoSize = true;
            this.keepAllCheckBox.Location = new System.Drawing.Point(166, 109);
            this.keepAllCheckBox.Name = "keepAllCheckBox";
            this.keepAllCheckBox.Size = new System.Drawing.Size(95, 17);
            this.keepAllCheckBox.TabIndex = 5;
            this.keepAllCheckBox.Text = "Оставить все";
            this.keepAllCheckBox.UseVisualStyleBackColor = true;
            this.keepAllCheckBox.CheckedChanged += new System.EventHandler(this.KeepAllCheckBox_CheckedChanged);
            // 
            // OverwriteFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 167);
            this.Controls.Add(this.keepAllCheckBox);
            this.Controls.Add(this.overwrightAllCheckBox);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.overwriteFileLabel);
            this.Name = "OverwriteFile";
            this.Text = "Уверены? мм аа?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label overwriteFileLabel;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.CheckBox overwrightAllCheckBox;
        private System.Windows.Forms.CheckBox keepAllCheckBox;
    }
}