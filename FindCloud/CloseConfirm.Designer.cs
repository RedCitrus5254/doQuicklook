namespace FindCloud
{
    partial class CloseConfirm
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
            this.closeQuicklookCreatingLabel = new System.Windows.Forms.Label();
            this.yesButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeQuicklookCreatingLabel
            // 
            this.closeQuicklookCreatingLabel.AutoEllipsis = true;
            this.closeQuicklookCreatingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeQuicklookCreatingLabel.Location = new System.Drawing.Point(26, 9);
            this.closeQuicklookCreatingLabel.Name = "closeQuicklookCreatingLabel";
            this.closeQuicklookCreatingLabel.Size = new System.Drawing.Size(200, 59);
            this.closeQuicklookCreatingLabel.TabIndex = 0;
            this.closeQuicklookCreatingLabel.Text = "Вы уверены, что хотите остановить создание квиклука?";
            this.closeQuicklookCreatingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(29, 95);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "Да";
            this.yesButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(149, 95);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // CloseConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 130);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.closeQuicklookCreatingLabel);
            this.Name = "CloseConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Точно?? а? м?";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label closeQuicklookCreatingLabel;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button cancelButton;
    }
}