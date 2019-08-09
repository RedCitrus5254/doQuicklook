﻿namespace FindCloud
{
    partial class MainForm
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
            this.createQuicklookButton = new System.Windows.Forms.Button();
            this.openFileLabel = new System.Windows.Forms.Label();
            this.pathToImage = new System.Windows.Forms.TextBox();
            this.quicklookCatalogLabel = new System.Windows.Forms.Label();
            this.pathToQuicklookCatalog = new System.Windows.Forms.TextBox();
            this.selectPathToImageButton = new System.Windows.Forms.Button();
            this.selectPathToQuicklookCatalog = new System.Windows.Forms.Button();
            this.widthSizeTextBox = new System.Windows.Forms.TextBox();
            this.percentTextBox = new System.Windows.Forms.TextBox();
            this.widthSizeLabel = new System.Windows.Forms.Label();
            this.percentLabel = new System.Windows.Forms.Label();
            this.radioButtonsPanel = new System.Windows.Forms.Panel();
            this.tiffRadioButton = new System.Windows.Forms.RadioButton();
            this.jpgRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // createQuicklookButton
            // 
            this.createQuicklookButton.Location = new System.Drawing.Point(347, 180);
            this.createQuicklookButton.Name = "createQuicklookButton";
            this.createQuicklookButton.Size = new System.Drawing.Size(75, 23);
            this.createQuicklookButton.TabIndex = 0;
            this.createQuicklookButton.Text = "Создать";
            this.createQuicklookButton.UseVisualStyleBackColor = true;
            this.createQuicklookButton.Click += new System.EventHandler(this.CreateQuicklookButton_Click);
            // 
            // openFileLabel
            // 
            this.openFileLabel.AutoSize = true;
            this.openFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openFileLabel.Location = new System.Drawing.Point(32, 35);
            this.openFileLabel.Name = "openFileLabel";
            this.openFileLabel.Size = new System.Drawing.Size(108, 17);
            this.openFileLabel.TabIndex = 1;
            this.openFileLabel.Text = "Открыть файл:";
            this.openFileLabel.Click += new System.EventHandler(this.openFileLabel_Click);
            // 
            // pathToImage
            // 
            this.pathToImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathToImage.Location = new System.Drawing.Point(35, 55);
            this.pathToImage.Name = "pathToImage";
            this.pathToImage.Size = new System.Drawing.Size(324, 24);
            this.pathToImage.TabIndex = 2;
            // 
            // quicklookCatalogLabel
            // 
            this.quicklookCatalogLabel.AutoSize = true;
            this.quicklookCatalogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quicklookCatalogLabel.Location = new System.Drawing.Point(32, 102);
            this.quicklookCatalogLabel.Name = "quicklookCatalogLabel";
            this.quicklookCatalogLabel.Size = new System.Drawing.Size(125, 17);
            this.quicklookCatalogLabel.TabIndex = 3;
            this.quicklookCatalogLabel.Text = "Целевой каталог:";
            // 
            // pathToQuicklookCatalog
            // 
            this.pathToQuicklookCatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathToQuicklookCatalog.Location = new System.Drawing.Point(35, 122);
            this.pathToQuicklookCatalog.Name = "pathToQuicklookCatalog";
            this.pathToQuicklookCatalog.Size = new System.Drawing.Size(324, 24);
            this.pathToQuicklookCatalog.TabIndex = 4;
            // 
            // selectPathToImageButton
            // 
            this.selectPathToImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectPathToImageButton.Location = new System.Drawing.Point(365, 55);
            this.selectPathToImageButton.Name = "selectPathToImageButton";
            this.selectPathToImageButton.Size = new System.Drawing.Size(41, 23);
            this.selectPathToImageButton.TabIndex = 5;
            this.selectPathToImageButton.Text = ". . .";
            this.selectPathToImageButton.UseVisualStyleBackColor = true;
            this.selectPathToImageButton.Click += new System.EventHandler(this.SelectPathToImageButton_Click);
            // 
            // selectPathToQuicklookCatalog
            // 
            this.selectPathToQuicklookCatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectPathToQuicklookCatalog.Location = new System.Drawing.Point(365, 122);
            this.selectPathToQuicklookCatalog.Name = "selectPathToQuicklookCatalog";
            this.selectPathToQuicklookCatalog.Size = new System.Drawing.Size(41, 24);
            this.selectPathToQuicklookCatalog.TabIndex = 6;
            this.selectPathToQuicklookCatalog.Text = ". . .";
            this.selectPathToQuicklookCatalog.UseVisualStyleBackColor = true;
            this.selectPathToQuicklookCatalog.Click += new System.EventHandler(this.SelectPathToQuicklookCatalog_Click);
            // 
            // widthSizeTextBox
            // 
            this.widthSizeTextBox.Location = new System.Drawing.Point(35, 183);
            this.widthSizeTextBox.Name = "widthSizeTextBox";
            this.widthSizeTextBox.Size = new System.Drawing.Size(54, 20);
            this.widthSizeTextBox.TabIndex = 7;
            this.widthSizeTextBox.Text = "2000";
            this.widthSizeTextBox.Click += new System.EventHandler(this.WidthSizeTextBox_Click);
            // 
            // percentTextBox
            // 
            this.percentTextBox.Location = new System.Drawing.Point(132, 183);
            this.percentTextBox.Name = "percentTextBox";
            this.percentTextBox.Size = new System.Drawing.Size(52, 20);
            this.percentTextBox.TabIndex = 8;
            this.percentTextBox.Click += new System.EventHandler(this.PercentTextBox_Click);
            // 
            // widthSizeLabel
            // 
            this.widthSizeLabel.AutoSize = true;
            this.widthSizeLabel.Location = new System.Drawing.Point(32, 167);
            this.widthSizeLabel.Name = "widthSizeLabel";
            this.widthSizeLabel.Size = new System.Drawing.Size(46, 13);
            this.widthSizeLabel.TabIndex = 9;
            this.widthSizeLabel.Text = "Ширина";
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Location = new System.Drawing.Point(129, 167);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(50, 13);
            this.percentLabel.TabIndex = 10;
            this.percentLabel.Text = "Процент";
            // 
            // radioButtonsPanel
            // 
            this.radioButtonsPanel.Controls.Add(this.tiffRadioButton);
            this.radioButtonsPanel.Controls.Add(this.jpgRadioButton);
            this.radioButtonsPanel.Location = new System.Drawing.Point(232, 158);
            this.radioButtonsPanel.Name = "radioButtonsPanel";
            this.radioButtonsPanel.Size = new System.Drawing.Size(85, 51);
            this.radioButtonsPanel.TabIndex = 11;
            // 
            // tiffRadioButton
            // 
            this.tiffRadioButton.AutoSize = true;
            this.tiffRadioButton.Location = new System.Drawing.Point(0, 28);
            this.tiffRadioButton.Name = "tiffRadioButton";
            this.tiffRadioButton.Size = new System.Drawing.Size(47, 17);
            this.tiffRadioButton.TabIndex = 1;
            this.tiffRadioButton.TabStop = true;
            this.tiffRadioButton.Text = "TIFF";
            this.tiffRadioButton.UseVisualStyleBackColor = true;
            this.tiffRadioButton.CheckedChanged += new System.EventHandler(this.TiffRadioButton_CheckedChanged);
            // 
            // jpgRadioButton
            // 
            this.jpgRadioButton.AutoSize = true;
            this.jpgRadioButton.Checked = true;
            this.jpgRadioButton.Location = new System.Drawing.Point(0, 0);
            this.jpgRadioButton.Name = "jpgRadioButton";
            this.jpgRadioButton.Size = new System.Drawing.Size(52, 17);
            this.jpgRadioButton.TabIndex = 0;
            this.jpgRadioButton.TabStop = true;
            this.jpgRadioButton.Text = "JPEG";
            this.jpgRadioButton.UseVisualStyleBackColor = true;
            this.jpgRadioButton.CheckedChanged += new System.EventHandler(this.JpgRadioButton_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 221);
            this.Controls.Add(this.radioButtonsPanel);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.widthSizeLabel);
            this.Controls.Add(this.percentTextBox);
            this.Controls.Add(this.widthSizeTextBox);
            this.Controls.Add(this.selectPathToQuicklookCatalog);
            this.Controls.Add(this.selectPathToImageButton);
            this.Controls.Add(this.pathToQuicklookCatalog);
            this.Controls.Add(this.quicklookCatalogLabel);
            this.Controls.Add(this.pathToImage);
            this.Controls.Add(this.openFileLabel);
            this.Controls.Add(this.createQuicklookButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Создание квиклука";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.radioButtonsPanel.ResumeLayout(false);
            this.radioButtonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createQuicklookButton;
        private System.Windows.Forms.Label openFileLabel;
        private System.Windows.Forms.TextBox pathToImage;
        private System.Windows.Forms.Label quicklookCatalogLabel;
        private System.Windows.Forms.TextBox pathToQuicklookCatalog;
        private System.Windows.Forms.Button selectPathToImageButton;
        private System.Windows.Forms.Button selectPathToQuicklookCatalog;
        private System.Windows.Forms.TextBox widthSizeTextBox;
        private System.Windows.Forms.TextBox percentTextBox;
        private System.Windows.Forms.Label widthSizeLabel;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.Panel radioButtonsPanel;
        private System.Windows.Forms.RadioButton jpgRadioButton;
        private System.Windows.Forms.RadioButton tiffRadioButton;
    }
}