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
    /// <summary>
    /// Главная форма. Здесь указывается путь к снимку, к каталогу квиклуков (по умолчанию папка QuickLook создаётся в папке со снимками);
    /// Можно выбрать выходное расширение файла (jpg или tif);
    /// Уменьшить изображение : 1) Выбрать размер наибольшей стороны (вторая считается пропорционально), 2) Указать в процентах размеры сторон,
    /// 3) указать площадь выходного изображения;
    /// Выбор одного из чекбоксов  определяет, при нажатии кнопки выбирается один файл или вся папка;
    /// Также есть чекбокс подкаталогов. Если он true, будут обрабатываться все снимки подкаталогов и сохраняться в указанную папку. 
    /// </summary>
    public partial class MainForm : Form, IMainForm
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

        /// <summary>
        /// Проверка правильности заполнения MainForm
        /// </summary>
        /// <returns></returns>
        private bool IsFormCorrect()
        {
            if (pathToImage.Text.Equals(""))
            {
                MessageBox.Show("Выберите путь к изображениям");
                return false;
            }
            if (widthSizeTextBox.Text.Equals("") && percentTextBox.Text.Equals("") && squareTextBox.Text.Equals(""))
            {
                MessageBox.Show($"Неверные значения полей {widthSizeLabel.Text} или {percentLabel.Text} или {squareLabel.Text}");
                return false;
            }
            int width = 1;
            int square = 1;
            double prct = 1.0;
            if (!widthSizeTextBox.Text.Equals("") && !int.TryParse(widthSizeTextBox.Text, out width))
            {
                MessageBox.Show("Неверное значение стороны");
                return false;
            }
            else
            {
                if (width < 0 || width>10000)
                {
                    MessageBox.Show("Неверное значение стороны");
                    return false;
                }
            }
            if(!squareTextBox.Text.Equals("") && !int.TryParse(squareTextBox.Text, out square))
            {
                MessageBox.Show("Неверное значение площади");
                return false;
            }
            else
            {
                if (square < 0 || square > 900000000)
                {
                    MessageBox.Show("Неверное значение площади");
                    return false;
                }
            }
            if(!percentTextBox.Text.Equals("") && !double.TryParse(percentTextBox.Text, out prct))
            {
                MessageBox.Show("Неверное значение процента");
                return false;
            }
            else
            {
                if (prct < 0 || prct > 100)
                {
                    MessageBox.Show("Неверное значение процента");
                    return false;
                }
            }

            if (pathToImage.Text.EndsWith(".tif"))
            {
                if (!Directory.Exists(Path.GetDirectoryName(pathToImage.Text)))
                {
                    MessageBox.Show("Неверный путь к снимку");
                    return false;
                }
                
            }
            else
            {
                if (!Directory.Exists(pathToImage.Text))
                {
                    MessageBox.Show("Неверный путь к снимку");
                    return false;
                }
            }

            if (!Directory.GetParent(pathToQuicklookCatalog.Text).Exists)
            {
                MessageBox.Show("Неверный путь к каталогу квиклуков");
                return false;
            }

            return true;
            
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать квиклук"
        /// Кнопка блокируется
        /// Проверяет, какое поле (процент, сторона, площадь снимка) непустое, и вызывает метод создания квиклука класса Quicklook;
        /// Затем подписывается на объект Quicklook (Это сделано, чтобы отследить, когда создание квиклука завершится,
        /// и сделать кнопку создания квиклука доступной;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void  CreateQuicklookButton_Click(object sender, EventArgs e)
        {
            if (!IsFormCorrect())
            {
                return;
            }

            createQuicklookButton.Enabled = false;

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
                    percent, format);
                ql.SubscribeMainform(this);

            }
            else if (!widthSizeTextBox.Text.Equals(""))
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
                    size, format);
                ql.SubscribeMainform(this);
                
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
                ql.SubscribeMainform(this);
            }
            




            //(new ProgressDialog()).ShowDialog(this);
        }

        /// <summary>
        /// В зависимости от чекбокса: если выбираем один файл, то используется OpenfileDialog;
        /// Если выбираем папку, то Cataloguer.FolderBrowser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            else if (pathToImage.Text.Equals(""))
                            {

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

        /// <summary>
        /// Кнопка выбора пути к каталогу квиклуков, если нужно сохранить квиклук не в папке QuickLook, которая находится в папке со снимком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// При нажатии на одно из полей размеров изображений, ему присваивается стандартное значение, а другие поля становятся пустыми
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private void SquareTextBox_Click(object sender, EventArgs e)
        {
            squareTextBox.Text = "4000000";
            widthSizeTextBox.Text = "";
            percentTextBox.Text = "";
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

        public void EnableCreateQuicklookButton()
        {
            createQuicklookButton.Enabled = true;
        }
    }
}
