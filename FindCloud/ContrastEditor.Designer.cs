namespace FindCloud
{
    partial class ContrastEditor
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
            this.originalImagePictureBox = new System.Windows.Forms.PictureBox();
            this.contrastImagePictureBox = new System.Windows.Forms.PictureBox();
            this.pathToImageTextBox = new System.Windows.Forms.TextBox();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.pixelValueLabel = new System.Windows.Forms.Label();
            this.cloudPercentLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // originalImagePictureBox
            // 
            this.originalImagePictureBox.Location = new System.Drawing.Point(12, 12);
            this.originalImagePictureBox.Name = "originalImagePictureBox";
            this.originalImagePictureBox.Size = new System.Drawing.Size(364, 379);
            this.originalImagePictureBox.TabIndex = 0;
            this.originalImagePictureBox.TabStop = false;
            this.originalImagePictureBox.Click += new System.EventHandler(this.OriginalImagePictureBox_Click);
            // 
            // contrastImagePictureBox
            // 
            this.contrastImagePictureBox.Location = new System.Drawing.Point(424, 12);
            this.contrastImagePictureBox.Name = "contrastImagePictureBox";
            this.contrastImagePictureBox.Size = new System.Drawing.Size(364, 379);
            this.contrastImagePictureBox.TabIndex = 1;
            this.contrastImagePictureBox.TabStop = false;
            // 
            // pathToImageTextBox
            // 
            this.pathToImageTextBox.Location = new System.Drawing.Point(12, 418);
            this.pathToImageTextBox.Name = "pathToImageTextBox";
            this.pathToImageTextBox.Size = new System.Drawing.Size(326, 20);
            this.pathToImageTextBox.TabIndex = 2;
            this.pathToImageTextBox.TextChanged += new System.EventHandler(this.PathToImageTextBox_TextChanged);
            // 
            // selectImageButton
            // 
            this.selectImageButton.Location = new System.Drawing.Point(344, 418);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(32, 23);
            this.selectImageButton.TabIndex = 3;
            this.selectImageButton.Text = ". . .";
            this.selectImageButton.UseVisualStyleBackColor = true;
            this.selectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.Location = new System.Drawing.Point(424, 418);
            this.contrastTrackBar.Maximum = 100;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.Size = new System.Drawing.Size(323, 45);
            this.contrastTrackBar.TabIndex = 4;
            this.contrastTrackBar.Value = 50;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.ContrastTrackBar_Scroll);
            // 
            // pixelValueLabel
            // 
            this.pixelValueLabel.AutoSize = true;
            this.pixelValueLabel.Location = new System.Drawing.Point(753, 418);
            this.pixelValueLabel.Name = "pixelValueLabel";
            this.pixelValueLabel.Size = new System.Drawing.Size(25, 13);
            this.pixelValueLabel.TabIndex = 5;
            this.pixelValueLabel.Text = "127";
            // 
            // cloudPercentLabel
            // 
            this.cloudPercentLabel.AutoSize = true;
            this.cloudPercentLabel.Location = new System.Drawing.Point(292, 463);
            this.cloudPercentLabel.Name = "cloudPercentLabel";
            this.cloudPercentLabel.Size = new System.Drawing.Size(117, 13);
            this.cloudPercentLabel.TabIndex = 6;
            this.cloudPercentLabel.Text = "Процент облачности: ";
            // 
            // ContrastEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.cloudPercentLabel);
            this.Controls.Add(this.pixelValueLabel);
            this.Controls.Add(this.contrastTrackBar);
            this.Controls.Add(this.selectImageButton);
            this.Controls.Add(this.pathToImageTextBox);
            this.Controls.Add(this.contrastImagePictureBox);
            this.Controls.Add(this.originalImagePictureBox);
            this.Name = "ContrastEditor";
            this.Text = "ContrastEditor";
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox originalImagePictureBox;
        private System.Windows.Forms.PictureBox contrastImagePictureBox;
        private System.Windows.Forms.TextBox pathToImageTextBox;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.Label pixelValueLabel;
        private System.Windows.Forms.Label cloudPercentLabel;
    }
}