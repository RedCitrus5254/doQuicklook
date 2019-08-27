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
            pathToImageToolTip.SetToolTip(pathToImage, "Путь к папке с изображениями");
            pathToQuicklookCatalogToolTip.SetToolTip(pathToQuicklookCatalog, "Путь к папке, в которую будут сохраняться квиклуки");
            widthSizeToolTip.SetToolTip(widthSizeTextBox, "Задать размер наибольшей стороны квиклука");
            percentToolTip.SetToolTip(percentTextBox, "Задать размер квиклука в процентах от исходного изображения");
            setImageFormatToolTip.SetToolTip(radioButtonsPanel, "Выберите расширение квиклука");
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        
        private void CreateQuicklookCatalog() //проверяет, существует ли папка с квиклуками в папке с изображениями, если напрямую не указана папка квиклуков. Если нет, пишет в 
        {                                     //textbox путь к каталогу квиклуков, и при нажатии на кнопку создания квиклука создаёт каталог
            string quicklookPath;
            if (pathToQuicklookCatalog.Text.Equals(""))
            {
                FileAttributes attr = File.GetAttributes(pathToImage.Text);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    quicklookPath = pathToImage.Text;
                    Console.WriteLine(quicklookPath);
                }
                else
                {
                    quicklookPath = Path.GetDirectoryName(pathToImage.Text);
                    Console.WriteLine(quicklookPath);
                }
                quicklookPath += @"\QuickLook";
                
                
                pathToQuicklookCatalog.Text = quicklookPath;
                    
            
                //if (pathToImage.Text.Contains(".tif"))
                //{
                //    quicklookPath = pathToImage.Text.Replace(pathToImage.Text.Substring(pathToImage.Text.LastIndexOf(@"\")), "");
                //    Console.WriteLine(quicklookPath);
                //}
            }
            else
            {
                return;
            }
        }
        private void CreateQuicklookButton_Click(object sender, EventArgs e)
        {
            if (pathToImage.Text.Equals(""))
            {
                MessageBox.Show("Выберите путь к изображениям");
                return;
            }
            
            if (widthSizeTextBox.Text.Equals("") && percentTextBox.Text.Equals("")&&squareTextBox.Text.Equals(""))
            {
                MessageBox.Show($"Неверные значения полей {widthSizeLabel.Text} или {percentLabel.Text} или {squareLabel.Text}");
            }
            else
            {
                
                if (!percentTextBox.Text.Equals(""))
                {
                    double percent;
                    try
                    {
                        percent = Convert.ToDouble(percentTextBox.Text);
                    }
                    catch
                    {
                        return;
                    }
                    
                    if (percent < 0 || percent > 100)
                    {
                        MessageBox.Show("Значение не входит в промежуток [0,100]");
                    }
                    else
                    {
                        if (!Directory.Exists(pathToQuicklookCatalog.Text)) //создаём каталог квиклуков, если его нет
                        {
                            Directory.CreateDirectory(pathToQuicklookCatalog.Text);
                        }
                        Quicklook ql = new Quicklook();
                        if(subdirectoryCheckBox.Checked == true)
                        {
                            ql.GoDirectories = true;
                        }
                        ql.CreateQuicklook(
                            pathToImage.Text,
                            pathToQuicklookCatalog.Text,
                            percent, format);
                    }
                }
                else if(!widthSizeTextBox.Text.Equals(""))
                {
                    int size;
                    try
                    {
                        size = Convert.ToInt32(widthSizeTextBox.Text);
                    }
                    catch
                    {
                        return;
                    }
                    
                    if (size <= 0 || size > 10000)
                    {
                        MessageBox.Show("Значение не входит в промежуток (0,10000]");
                    }
                    else
                    {
                        if (!Directory.Exists(pathToQuicklookCatalog.Text)) //создаём каталог квиклуков, если его нет
                        {
                            Directory.CreateDirectory(pathToQuicklookCatalog.Text);
                        }
                        Quicklook ql = new Quicklook();
                        if (subdirectoryCheckBox.Checked == true)
                        {
                            ql.GoDirectories = true;
                        }
                        createQuicklookButton.Enabled = false;
                        ql.CreateQuicklook(
                            pathToImage.Text,
                            pathToQuicklookCatalog.Text,
                            size, format);
                    }
                }
                else if (!squareTextBox.Text.Equals(""))
                {
                    if (!Directory.Exists(pathToQuicklookCatalog.Text)) //создаём каталог квиклуков, если его нет
                    {
                        Directory.CreateDirectory(pathToQuicklookCatalog.Text);
                    }
                    Quicklook ql = new Quicklook();
                    if (subdirectoryCheckBox.Checked == true)
                    {
                        ql.GoDirectories = true;
                    }
                    ql.CreateQuicklook(
                        pathToImage.Text,
                        pathToQuicklookCatalog.Text,
                        squareTextBox.Text,
                        format);

                }
                createQuicklookButton.Enabled = true;
            }
            
            //(new ProgressDialog()).ShowDialog(this);
        }

        private void openFileLabel_Click(object sender, EventArgs e)
        {

        }

        private void SelectPathToImageButton_Click(object sender, EventArgs e)
        {
            if (setOneFileRadioButton.Checked) //если выбираем 1 файл
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            pathToImage.Text = openFileDialog.FileName;

                            if (Directory.Exists(Path.GetDirectoryName(pathToImage.Text)))
                            {
                                pathToQuicklookCatalog.Text = Path.GetDirectoryName(pathToImage.Text) + @"\QuickLook";
                            }
                            else
                            {
                                MessageBox.Show("Указанная директория не существует");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
                            $"Details:\n\n{ex.StackTrace}");
                        }
                    }
                }
            }
            else //если выбираем папку
            {
                pathToImage.Text = Cataloguer.FolderBrowser.ChooseFolder(this);

                if (Directory.Exists(pathToImage.Text))
                {
                    pathToQuicklookCatalog.Text = pathToImage.Text + @"\QuickLook";
                }
                else
                {
                    MessageBox.Show("указанная директория не существует");
                }
            }

            //if (!pathToImage.Text.Equals(""))
            //{
            //    CreateQuicklookCatalog();
            //}
            //if (Directory.Exists(pathToImage.Text))
            //{
            //    pathToQuicklookCatalog.Text = Path.GetDirectoryName(pathToImage.Text) + @"\QuickLook";
            //}
            //else
            //{
            //    MessageBox.Show("указанная директория не существует");
            //}
        }

        private void SelectPathToQuicklookCatalog_Click(object sender, EventArgs e)
        {
            pathToQuicklookCatalog.Text = Cataloguer.FolderBrowser.ChooseFolder(this);

            //using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            //{
            //    if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            pathToQuicklookCatalog.Text = folderBrowserDialog.SelectedPath;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
            //            $"Details:\n\n{ex.StackTrace}");
            //        }
            //    }
            //}
        }

        private void WidthSizeTextBox_Click(object sender, EventArgs e)
        {
            percentTextBox.Text = "";
            widthSizeTextBox.Text = "2000";
            squareTextBox.Text = "";
        }

        private void PercentTextBox_Click(object sender, EventArgs e)
        {
            percentTextBox.Text = "10";
            widthSizeTextBox.Text = "";
            squareTextBox.Text = "";
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
                format = "tif";
            }
        }

        private void subdirectoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void widthSizeLabel_Click(object sender, EventArgs e)
        {

        }

        private void SquareTextBox_Click(object sender, EventArgs e)
        {
            squareTextBox.Text = "4000000";
            widthSizeTextBox.Text = "";
            percentTextBox.Text = "";
        }
    }
}
