using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCloud
{
    public partial class ContrastEditor : Form
    {
        FindingClouds fc = new FindingClouds();
        int val;
        Thread contrastThread;
        public delegate void MyDelegate(int percent);

        int pixelValue = 0;
        
        public ContrastEditor()
        {
            InitializeComponent();
            originalImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            contrastImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            contrastThread = new Thread(new ThreadStart(GoThread));
            contrastThread.IsBackground = true;
            contrastThread.Start();
        }

        private void OriginalImagePictureBox_Click(object sender, EventArgs e)
        {

        }

        private void PathToImageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pathToImageTextBox.Text = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                    originalImagePictureBox.Image = null;
                    originalImagePictureBox.Image = Image.FromFile(pathToImageTextBox.Text);
                    val = (int)(contrastTrackBar.Value * 255/100);
                    contrastImagePictureBox.Image =  fc.Contrast(originalImagePictureBox.Image, val);
                    cloudPercentLabel.Text = $"Процент облачности: {fc.CloudPercent}%";
                }
            }
        }

        private void GoThread()
        {
            while (true)
            {
                if (pixelValue != 0)
                {
                    int contrast = pixelValue;
                    pixelValue = 0;
                    GC.Collect();
                    
                    //pixelValueLabel.BeginInvoke(new MyDelegate(SetPixelValue), b[0]);

                    if (originalImagePictureBox.Image != null)
                    {
                        Image image = fc.Contrast(originalImagePictureBox.Image, contrast);
                        contrastImagePictureBox.Image = null;
                        contrastImagePictureBox.Image = image;
                        cloudPercentLabel.BeginInvoke(new MyDelegate(SetCloudPercent), fc.CloudPercent);
                    }

                }
                
            }
            
        }

        private void ContrastTrackBar_Scroll(object sender, EventArgs e)
        {
            pixelValueLabel.Text = $"{(int)(contrastTrackBar.Value*2.55)}";
            //if (b[0] == 0)
            //{
                pixelValue = (int)(contrastTrackBar.Value * 2.55);
            //}
            //else
            //{
            //    b[1] = (int)(contrastTrackBar.Value * 2.55);
            //}
            //if (contrastThread != null)
            //{
            //    GC.Collect();
            //    fc = null;
            //}
            //fc = new FindingClouds();
            
            //val = (int)(contrastTrackBar.Value * 2.55);
            //percentLabel.Text = $"{val}";
            
            //if (originalImagePictureBox.Image != null)
            //{
            //    contrastThread = new Thread(new ThreadStart(CreateContrastImage));
            //    contrastThread.Start();
            //}
            //contrastThread.Join();
            
        }
        

        private void SetCloudPercent(int percent)
        {
            cloudPercentLabel.Text = $"Процент облачности: {fc.CloudPercent}%";
        }

    }
}
