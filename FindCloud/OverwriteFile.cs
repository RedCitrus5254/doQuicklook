using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCloud
{
    /// <summary>
    /// Форма открывается, если программа видит, что квиклук этого снимка уже создан;
    /// Форма предлагает на выбор: перезаписать квиклук или пропустить, перезаписать все квиклуки или пропустить все, что были уже созданы 
    /// </summary>
    public partial class OverwriteFile : Form
    {
        public bool IsAccepted { get; set; }
        public bool IsForAll { get; set; }
        public OverwriteFile()
        {
            InitializeComponent();
        }

        public void ChangeText(string text)
        {
            overwriteFileLabel.Text = $"Вы хотите перезаписать файл {text}?";
        }
        private void KeepAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(keepAllCheckBox.Checked == true)
            {
                overwrightAllCheckBox.Checked = false;
                yesButton.Enabled = false;
            }
            else if(keepAllCheckBox.Checked == false)
            {
                yesButton.Enabled = true;
            }
        }

        private void OverwrightAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(overwrightAllCheckBox.Checked == true)
            {
                keepAllCheckBox.Checked = false;
                noButton.Enabled = false;
            }
            else if(overwrightAllCheckBox.Checked == false)
            {
                noButton.Enabled = true;
            }
            

        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            if(overwrightAllCheckBox.Checked == true)
            {
                IsForAll = true;
            }
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            IsAccepted = false;
            if (keepAllCheckBox.Checked == true)
            {
                IsForAll = true;
            }
            this.Close();
        }
    }
}
