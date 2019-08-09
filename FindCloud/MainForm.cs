using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCloud
{
    public partial class MainForm : Form
    {
        private string format = "jpg";
        public MainForm()
        {
            InitializeComponent();

            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void CreateQuicklookButton_Click(object sender, EventArgs e)
        {
            
            if (widthSizeTextBox.Text.Equals("") && percentTextBox.Text.Equals(""))
            {
                MessageBox.Show($"Неверные значения полей {widthSizeLabel.Text} или {percentLabel.Text}");
            }
            else
            {
                
                if (widthSizeTextBox.Text.Equals(""))
                {
                    double percent = Convert.ToDouble(percentTextBox.Text);
                    if (percent < 0 || percent > 100)
                    {
                        MessageBox.Show("Значение не входит в промежуток [0,100]");
                    }
                    else
                    {
                        Quicklook ql = new Quicklook();
                        ql.CreateQuicklook(
                            pathToImage.Text,
                            pathToQuicklookCatalog.Text,
                            //@"\\Server2\gil\COSMOS_RAB\2019\SPOT_7\UTM",
                            //@"C:\Users\rez\Pictures\Saburov\forQuicklooks",
                            percent, format);
                    }
                }
                else
                {
                    int size = Convert.ToInt32(widthSizeTextBox.Text);
                    if (size <= 0 || size > 10000)
                    {
                        MessageBox.Show("Значение не входит в промежуток (0,10000]");
                    }
                    else
                    {
                        Quicklook ql = new Quicklook();
                        ql.CreateQuicklook(
                            pathToImage.Text,
                            pathToQuicklookCatalog.Text,
                            //@"\\Server2\gil\COSMOS_RAB\2019\SPOT_7\UTM",
                            //@"C:\Users\rez\Pictures\Saburov\forQuicklooks",
                            size, format);
                    }
                }
            }
            
            //(new ProgressDialog()).ShowDialog(this);
        }

        private void openFileLabel_Click(object sender, EventArgs e)
        {

        }

        private void SelectPathToImageButton_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pathToImage.Text = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                }
            }
            
        }

        private void SelectPathToQuicklookCatalog_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pathToQuicklookCatalog.Text = folderBrowserDialog.SelectedPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                }
            }
        }

        private void WidthSizeTextBox_Click(object sender, EventArgs e)
        {
            percentTextBox.Text = "";
            widthSizeTextBox.Text = "2000";

        }

        private void PercentTextBox_Click(object sender, EventArgs e)
        {
            widthSizeTextBox.Text = "";
            percentTextBox.Text = "10";
        }

        private void JpgRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(jpgRadioButton.Checked == true)
            {
                format = "jpg";
            }
        }

        private void TiffRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(tiffRadioButton.Checked == true)
            {
                format = "tiff";
            }
        }
    }
}
