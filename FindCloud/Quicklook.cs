using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace FindCloud
{
    /// <summary>
    /// Главный класс, который отвечает за создание квиклуков;
    /// главный метод -- CreateImage -- с помощью Gdal создаёт квиклук;
    /// Трижды перегруженный метод CreateQuicklook (перегружен из-за параметра размера изображения -- площадь, сторона, процент),
    /// который вызывается в MainForm. Из этих методов вызываются асинхронно методы CreateQuicklookOneImage и CreateQuicklookInDirectory,
    /// которые вызывают главный метод -- CreateImage
    /// </summary>
    public class Quicklook
    {
        private static int numOfPictures;

        private int quicklookSize;

        private int square;

        private string pathToQuicklookCatalog;

        private static string pathToPicture;

        private double percent;

        private string outputFile;

        private static bool isCanceled = false;

        private string format;

        private bool overwriteImage = true;

        private bool isForAll = false;

        public bool GoDirectories { get; set; } = false;
        
        private static ProgressDialog ProgressDialog { get; set; }

        private IMainForm mainform;

        public Quicklook()
        {

        }

        /// <summary>
        /// Сообщаем MainForm'е, когда создание квиклуков завершится
        /// </summary>
        /// <param name="mf">интерфейс с методом, который делает кнопку создания квиклуков активной</param>
        public void SubscribeMainform(IMainForm mf)
        {
            mainform = mf;
        }
        private void NotifyMainForm()
        {
            mainform.EnableCreateQuicklookButton();
        }
        
        //удаляет файлы, если отменить создание квиклука
        public void DeleteFiles()
        {

            string rootFolderPath = pathToQuicklookCatalog;
            string filesToDelete = outputFile.Substring(outputFile.LastIndexOf(@"\")+1);   
            filesToDelete = filesToDelete.Replace(".jpg", ""); //оставляем только имя файла без разрешения
            Console.WriteLine("удалён " + filesToDelete);
            string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete); //ищем такие файлы
            foreach (string file in fileList)
            {
                System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                System.IO.File.Delete(file);
            }
        }

        /// <summary>
        /// Сообщает прогресс диалогу о процессе создания квиклука;
        /// Обрабатывает отмену создания квиклука
        /// </summary>
        /// <param name="Complete"></param>
        /// <param name="Message"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int ProgressFunc(double Complete, IntPtr Message, IntPtr Data)
        {
            ProgressDialog.SetQuicklookCreatingProgress((int)(100 * Complete));

            if (ProgressDialog.IsWorkPaused)
            {
                CloseConfirm cc = new CloseConfirm();
                cc.ShowDialog();
                if (cc.IsAccepted)
                {
                    isCanceled = true;
                    return 0;
                }
                else
                {
                    ProgressDialog.IsWorkPaused = false;
                }
            }

            return 1;
        }

        
        /// <summary>
        /// считает процент, если была задана площадь квиклука
        /// </summary>
        /// <param name="s">площадь квиклука</param>
        /// <returns></returns>
        private double CalculateSquare(int s)
        {
            double inputFileWidth;
            double inputFileHeight;
            double coefficient;
            using (FileStream stream = new FileStream(pathToPicture, FileMode.Open, FileAccess.Read)) //открываем информаци о файле
            {
                using (Image tif = Image.FromStream(stream, false, false))
                {
                    inputFileWidth = tif.PhysicalDimension.Width;
                    inputFileHeight = tif.PhysicalDimension.Height;
                }
            }

            coefficient = Math.Sqrt(s / (inputFileHeight * inputFileWidth))*100;
            if (coefficient > 100)
            {
                return 100;
            }
            else
            {
                return coefficient;
            }

        }
        
        /// <summary>
        /// считает процент, если была задана длина стороны квиклука
        /// </summary>
        /// <param name="maxSize">Длина наибольшей стороны</param>
        /// <param name="InputFile"></param>
        /// <returns></returns>
        private double CalculatePercent(int maxSize, string InputFile)  //высчитываем, какой процент изображения останется от исходного 
        {
            double inputFileWidth;
            double inputFileHeight;
            double coefficient;
            double quicklookWidth;
            double quicklookHeight;

            using (FileStream stream = new FileStream(InputFile, FileMode.Open, FileAccess.Read)) //открываем информаци о файле
            {
                using (Image tif = Image.FromStream(stream, false, false))
                {
                    inputFileWidth = tif.PhysicalDimension.Width;
                    inputFileHeight = tif.PhysicalDimension.Height;
                    Console.WriteLine("ширина = " + inputFileWidth);
                    Console.WriteLine("высота = " + inputFileHeight);
                }
            }
            if (inputFileHeight > inputFileWidth)
            {
                quicklookHeight = maxSize;
                quicklookWidth = (quicklookHeight / inputFileHeight) * inputFileWidth;
            }
            else
            {
                quicklookWidth = maxSize;
                quicklookHeight = (quicklookWidth / inputFileWidth) * inputFileHeight;
            }
            

            coefficient = Math.Sqrt((quicklookHeight*quicklookWidth) / (inputFileWidth*inputFileHeight));
            Console.WriteLine(coefficient);

            coefficient = coefficient * 100; //получаем коэффициент в процентах
            Console.WriteLine(coefficient);

            return coefficient;
        }
        
        /// <summary>
        /// проверяет, есть ли файл в каталоге квиклуков и предлагает перезаписать его или оставить; перезаписать все или оставить все
        /// </summary>
        /// <param name="path">Путь к снимку</param>
        /// <returns></returns>
        private bool IsFileInCatalog(string path) 
        {

            if (File.Exists(path))
            {
                if (isForAll) //если уже была поставлена галочка выполнять для всех, пропустить эти инструкции
                {
                    return true;
                }
                OverwriteFile of = new OverwriteFile();
                of.ChangeText(path);
                of.ShowDialog();
                if (of.IsAccepted)
                {
                    overwriteImage = true;
                }
                else
                {
                    overwriteImage = false;
                }
                if (of.IsForAll)
                {
                    isForAll = true;
                }
                return true;

            }
            else
            {
                return false;

            }
        }

        //проверяет, существует ли снимок и директория квиклуков
        public bool IsPathValid(string pathToImage, string pathToQuicklookcatalog)
        {
            if (File.Exists(pathToImage) || Directory.Exists(pathToImage))
            {

            }
            else
            {
                MessageBox.Show("Неверный путь файла изображения");
                return false;
            }

            if (Directory.Exists(pathToQuicklookcatalog))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Неверный путь для каталога");
                return false;
            }
            
        }

        //создаёт квиклук
        public void CreateImage()
        {
            if (format.Equals("tif"))
            {


                Dataset ds = Gdal.Open(pathToPicture, Access.GA_ReadOnly);
                var options =
                        new GDALTranslateOptions(new[]
                        {
                    "-of", "GTiff",
                    "-outsize", $"{percent}%", $"{percent}%",
                    "-co", "WORLDFILE=YES",
                        });
                try
                {
                    Dataset newDs = Gdal.wrapper_GDALTranslate(outputFile, ds, options,
                    new Gdal.GDALProgressFuncDelegate(ProgressFunc), null);
                    newDs.Dispose();
                }
                catch
                {

                }
            }
            else if (format.Equals("jpg"))
            {
                Dataset ds = Gdal.Open(pathToPicture, Access.GA_ReadOnly);
                var options =
                    new GDALTranslateOptions(new[]
                    {
                    "-of", "JPEG",
                    "-outsize", $"{percent}%", $"{percent}%",
                    "-co", "WORLDFILE=YES",
                    });
                try
                {
                    Dataset newDs = Gdal.wrapper_GDALTranslate(outputFile, ds, options,
                    new Gdal.GDALProgressFuncDelegate(ProgressFunc), null);
                    newDs.Dispose();
                }
                catch
                {

                }
                
            }
        }

        //квиклук по площади изображения
        /// <summary>
        /// Запускает асинхронно задачу создания квиклука;
        /// Записывает информацию о формате, путях к снимку и к каталогу квиклуков;
        /// Инициализирует progressDialog и показывает его 
        /// </summary>
        /// <param name="path">Путь к снимку</param>
        /// <param name="pathToQuicklookCatalog"></param>
        /// <param name="square">В данном случае, площадь квиклука. В следующих 2 методах сторона и процент квиклука</param>
        /// <param name="format">Формат квиклука</param>
        public async void CreateQuicklook(string path, string pathToQuicklookCatalog, string square, string format)
        {
            this.format = format;
            if (!IsPathValid(path, pathToQuicklookCatalog))
            {
                return;
            }
            //ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука
            ProgressDialog = new ProgressDialog();
            try
            {
                this.square = Convert.ToInt32(square);
            }
            catch
            {
                MessageBox.Show("Неверное значение площади");
                return;
            }
            
            this.pathToQuicklookCatalog = pathToQuicklookCatalog;
            pathToPicture = path;

            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                await Task.Run(() => CreateQuicklookOneImageSquare());
            }
            else
            {
                await Task.Run(() => CreateQuicklookInDirectorySquare());
            }

            NotifyMainForm();
        }

        //квиклук по стороне
        public async void CreateQuicklook(string path, string pathToQuicklookCatalog, int quicklookSize, string format) //квиклук из файла или папки с заданной шириной картинки
        {
            this.format = format;
            if(!IsPathValid(path, pathToQuicklookCatalog))
            {
                return;
            }
            
            ProgressDialog = new ProgressDialog();

            this.quicklookSize = quicklookSize;
            this.pathToQuicklookCatalog = pathToQuicklookCatalog;
            pathToPicture = path;

            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                await Task.Run(() => CreateQuicklookOneImageWidthSize());
            }
            else
            {
                await Task.Run(() => CreateQuicklookInDirectoryWidthSize());
            }

            NotifyMainForm();

        }
        //квиклук из файла или папки с заданным процентом
        public async void CreateQuicklook(string path, string pathToQuicklookCatalog, double percent, string format) 
        {
            this.format = format;
            if (!IsPathValid(path, pathToQuicklookCatalog))
            {
                return;
            }
            ProgressDialog = new ProgressDialog();
            this.percent = percent;
            this.pathToQuicklookCatalog = pathToQuicklookCatalog;
            pathToPicture = path;

            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                await Task.Run(() => CreateQuicklookOneImagePercent());
            }
            else
            {
                await Task.Run(() => CreateQuicklookInDirectoryPercent());
            }

            NotifyMainForm();
        }

        /// <summary>
        /// Следующие 3 метода создают ОДИН квиклук по стороне, площади или проценту от исходного снимка;
        /// Высчитывается процент для метода GDAL, проверяется наличие файла в каталоге квиклуков, создаётся квиклук и закрывается progressDialog;
        /// При отмене создания файл удаляется
        /// </summary>
        public void CreateQuicklookOneImageWidthSize(/*object sender, DoWorkEventArgs e*/) //ещё проекции и др.
        {
            ProgressDialog.SetImageName(pathToPicture, 1, 0);

            if (quicklookSize != 0) //если задано значение ширины квиклука, то высчитываем процент
            {
                percent = CalculatePercent(quicklookSize, pathToPicture);
            }

            Console.WriteLine(pathToPicture);
            outputFile = pathToQuicklookCatalog;
            outputFile += pathToPicture.Substring(pathToPicture.LastIndexOf(@"\"));
            outputFile = outputFile.Replace("tif", format);

            if (IsFileInCatalog(outputFile))//проверяем, есть ли в каталоге квиклуков это изображение
            {
                if (!overwriteImage)
                {
                    return;
                }
            }

            Console.WriteLine(outputFile);

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }

            CreateImage(); //создаём изображение

            ProgressDialog.SetImageName(pathToPicture, 1, 1);
            ProgressDialog.CloseProgressWindow();

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
        }
        
        public void CreateQuicklookOneImagePercent(/*object sender, DoWorkEventArgs e*/) //ещё проекции и др.
        {
            ProgressDialog.SetImageName(pathToPicture, 1, 0);

            Console.WriteLine(pathToPicture);

            outputFile = pathToQuicklookCatalog;
            outputFile += pathToPicture.Substring(pathToPicture.LastIndexOf(@"\"));
            outputFile = outputFile.Replace("tif", format);

            if (IsFileInCatalog(outputFile)) //проверяем, есть ли в каталоге квиклуков это изображение
            {
                if (!overwriteImage)
                {
                    return;
                }
            }

            Console.WriteLine(outputFile);

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }

            CreateImage(); //создаём изображение

            ProgressDialog.SetImageName(pathToPicture, 1, 1);
            ProgressDialog.CloseProgressWindow();

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
        }

        public void CreateQuicklookOneImageSquare(/*object sender, DoWorkEventArgs e*/)
        {
            ProgressDialog.SetImageName(pathToPicture, 1, 0);

            percent = CalculateSquare(square);

            Console.WriteLine(pathToPicture);
            outputFile = pathToQuicklookCatalog;
            outputFile += pathToPicture.Substring(pathToPicture.LastIndexOf(@"\"));
            outputFile = outputFile.Replace("tif", format);

            if (IsFileInCatalog(outputFile))//проверяем, есть ли в каталоге квиклуков это изображение
            {
                if (!overwriteImage)
                {
                    return;
                }
            }

            Console.WriteLine(outputFile);

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }

            CreateImage(); //создаём изображение

            ProgressDialog.SetImageName(pathToPicture, 1, 1);
            ProgressDialog.CloseProgressWindow();

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
        }

        /// <summary>
        /// Если помечен чекбокс "подкаталоги", то рекурсивно пройдёт по всем подпапкам и сделает квиклуки в папке path -> QuickLook;
        /// Иначе сделает квиклуки только в данной папке path
        /// </summary>
        /// <param name="path">путь к директории</param>
        /// <param name="quicklookType">Считать по стороне, площади или проценту снимка</param>
        private void GoToDirectories(string path, string quicklookType) //принимает путь к директории -- path
        {
            if (GoDirectories == true)
            {
                string[] directories = Directory.GetDirectories(path);

                if (directories.Length != 0)
                {
                    foreach (string dir in directories)
                    {
                        if (dir.Substring(dir.LastIndexOf(@"\")).Equals(@"\Quicklook"))
                        {
                            return;
                        }
                        else
                        {
                            GoToDirectories(dir, quicklookType);
                        }
                    }
                }
            }
            

            string[] fileNamesMass = Directory.GetFiles(path, "*.tif");
            numOfPictures = fileNamesMass.Length;


            for (int i = 0; i < numOfPictures; i++)
            {
                if (isCanceled == true)
                {
                    return;
                }
                pathToPicture = fileNamesMass[i];

                ProgressDialog.SetImageName(fileNamesMass[i], numOfPictures, i);

                if (quicklookType.Equals("quicklookSize"))
                {
                    percent = CalculatePercent(quicklookSize, fileNamesMass[i]);
                }
                else if (quicklookType.Equals("square"))
                {
                    percent = CalculateSquare(square);
                }

                outputFile = pathToQuicklookCatalog;
                outputFile += fileNamesMass[i].Substring(fileNamesMass[i].LastIndexOf(@"\"));
                outputFile = outputFile.Replace("tif", format);

                if (IsFileInCatalog(outputFile)) //проверяем, есть ли в каталоге квиклуков эти изображения
                {
                    if (!overwriteImage)
                    {
                        continue;
                    }
                }

                CreateImage(); //создаём изображение
            }


        }
       
        /// <summary>
        /// Следующие 3 метода существуют непонятно почему.
        /// Но они вызывают GoToDirectories, закрывают ProgressDialog и при отмене создания квиклука удаляют файлы
        /// </summary>
        private void CreateQuicklookInDirectoryWidthSize(/*object sender, DoWorkEventArgs e*/)
        {
            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }
            
            GoToDirectories(pathToPicture, "quicklookSize");

            ProgressDialog.CloseProgressWindow();

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
            
        }

        
        public void CreateQuicklookInDirectoryPercent(/*object sender, DoWorkEventArgs e*/) //ещё проекции и др. //дописать, как предыдущую ф-ю
        {

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }
            
            GoToDirectories(pathToPicture, "percent");

            ProgressDialog.CloseProgressWindow();

            if (isCanceled) //удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
        }
        public void CreateQuicklookInDirectorySquare(/*object sender, DoWorkEventArgs e*/)
        {

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }

            GoToDirectories(pathToPicture, "square");

            ProgressDialog.CloseProgressWindow();

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                isCanceled = false;
                DeleteFiles();
            }
        }


    }
}

