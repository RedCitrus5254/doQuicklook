using System;

namespace FindCloud
{
    partial class ProgressDialog
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.quicklookCreatingProgressBar = new System.Windows.Forms.ProgressBar();
            this.numOfImagesComplited = new System.Windows.Forms.ProgressBar();
            this.numOfImagesLabel = new System.Windows.Forms.Label();
            this.CreatingQuicklookLabel = new System.Windows.Forms.Label();
            this.canselButton = new System.Windows.Forms.Button();
            this.quicklookCreatingPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // quicklookCreatingProgressBar
            // 
            this.quicklookCreatingProgressBar.Location = new System.Drawing.Point(12, 79);
            this.quicklookCreatingProgressBar.Name = "quicklookCreatingProgressBar";
            this.quicklookCreatingProgressBar.Size = new System.Drawing.Size(318, 23);
            this.quicklookCreatingProgressBar.TabIndex = 0;
            // 
            // numOfImagesComplited
            // 
            this.numOfImagesComplited.Location = new System.Drawing.Point(12, 25);
            this.numOfImagesComplited.Name = "numOfImagesComplited";
            this.numOfImagesComplited.Size = new System.Drawing.Size(318, 23);
            this.numOfImagesComplited.TabIndex = 3;
            // 
            // numOfImagesLabel
            // 
            this.numOfImagesLabel.AutoSize = true;
            this.numOfImagesLabel.Location = new System.Drawing.Point(12, 9);
            this.numOfImagesLabel.Name = "numOfImagesLabel";
            this.numOfImagesLabel.Size = new System.Drawing.Size(49, 13);
            this.numOfImagesLabel.TabIndex = 4;
            this.numOfImagesLabel.Text = "Снимок:";
            // 
            // CreatingQuicklookLabel
            // 
            this.CreatingQuicklookLabel.AutoSize = true;
            this.CreatingQuicklookLabel.Location = new System.Drawing.Point(12, 63);
            this.CreatingQuicklookLabel.Name = "CreatingQuicklookLabel";
            this.CreatingQuicklookLabel.Size = new System.Drawing.Size(109, 13);
            this.CreatingQuicklookLabel.TabIndex = 5;
            this.CreatingQuicklookLabel.Text = "Создание квиклука:";
            // 
            // canselButton
            // 
            this.canselButton.Location = new System.Drawing.Point(131, 108);
            this.canselButton.Name = "canselButton";
            this.canselButton.Size = new System.Drawing.Size(75, 23);
            this.canselButton.TabIndex = 6;
            this.canselButton.Text = "Отмена";
            this.canselButton.UseVisualStyleBackColor = true;
            this.canselButton.Click += new System.EventHandler(this.CanselButton_Click);
            // 
            // quicklookCreatingPercent
            // 
            this.quicklookCreatingPercent.AutoSize = true;
            this.quicklookCreatingPercent.Location = new System.Drawing.Point(295, 63);
            this.quicklookCreatingPercent.Name = "quicklookCreatingPercent";
            this.quicklookCreatingPercent.Size = new System.Drawing.Size(21, 13);
            this.quicklookCreatingPercent.TabIndex = 7;
            this.quicklookCreatingPercent.Text = "0%";
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 138);
            this.Controls.Add(this.quicklookCreatingPercent);
            this.Controls.Add(this.canselButton);
            this.Controls.Add(this.CreatingQuicklookLabel);
            this.Controls.Add(this.numOfImagesLabel);
            this.Controls.Add(this.numOfImagesComplited);
            this.Controls.Add(this.quicklookCreatingProgressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Прогресс...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion

        private System.Windows.Forms.ProgressBar quicklookCreatingProgressBar;
        private System.Windows.Forms.ProgressBar numOfImagesComplited;
        private System.Windows.Forms.Label numOfImagesLabel;
        private System.Windows.Forms.Label CreatingQuicklookLabel;
        private System.Windows.Forms.Button canselButton;
        private System.Windows.Forms.Label quicklookCreatingPercent;
    }
}

