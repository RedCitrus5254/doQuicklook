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
    //надо допилить:
    //получить размер изображения, чтобы узнать, насколько его уменьшать (закончено или почти)
    //прогресс-бар, чтобы пользователь видел, сколько осталось ждать квиклук (закончено)
    //минимальный интерфейс, который будет выводить картинки
    //многопоточность
    public class Quicklook
    {
        private static int numOfPictures;

        private static BackgroundWorker _worker;

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
        

        private static ProgressDialog ProgressDialog { get; set; } /*= new ProgressDialog();*/

        //public string InputFile { get; set; }

        public Quicklook()
        {

        }

        
        
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
                //  System.IO.File.Delete(file);
            }
        }

        public static int ProgressFunc(double Complete, IntPtr Message, IntPtr Data)
        {
            _worker.ReportProgress((int)(100 * Complete)/*, new QuicklookProgress(pathToPicture, numOfPictures)*/);
            
            if (ProgressDialog.IsWorkPaused)
            {
                CloseConfirm cc = new CloseConfirm();
                cc.ShowDialog();
                if (cc.IsAccepted)
                {
                    isCanceled = true;
                    _worker.CancelAsync();
                    return 0;
                }
                else
                {
                    ProgressDialog.IsWorkPaused = false;
                }
            }

            return 1;
        }

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
                    Console.WriteLine("ширина = " + inputFileWidth);
                    Console.WriteLine("высота = " + inputFileHeight);
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

        private double CalculatePercent(int maxSize, string InputFile)  //высчитываем, какой процент изображения останется от исходного 
        {
            double inputFileWidth;
            double inputFileHeight;
            double coefficient;
            double quicklookWidth;
            double quicklookHeight;
            //const int optimalSquare = 4000000;  //оптимальная площадь изображения width*height (2000*2000)

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

        private bool IsFileInCatalog(string path) //проверяет, есть ли файл в каталоге квиклуков и предлагает перезаписать его или оставить; перезаписать все или оставить все
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
        public void CreateQuicklook(string path, string pathToQuicklookCatalog, string square, string format)
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

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                ProgressDialog.DoWork += CreateQuicklookOneImageSquare;
            }
            else
            {
                ProgressDialog.DoWork += CreateQuicklookInDirectorySquare;
            }

            //ProgressDialog.DoWork += DoWork;
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);
            ProgressDialog.StartAsyncTask();
        }

        public void CreateQuicklook(string path, string pathToQuicklookCatalog, int quicklookSize, string format) //квиклук из файла или папки с заданной шириной картинки
        {
            this.format = format;
            if(!IsPathValid(path, pathToQuicklookCatalog))
            {
                return;
            }
            //ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука
            ProgressDialog = new ProgressDialog();

            this.quicklookSize = quicklookSize;
            this.pathToQuicklookCatalog = pathToQuicklookCatalog;
            pathToPicture = path;

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                ProgressDialog.DoWork += CreateQuicklookOneImageWidthSize;
            }
            else
            {
                ProgressDialog.DoWork += CreateQuicklookInDirectoryWidthSize;
            }

            //ProgressDialog.DoWork += DoWork;
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);
            ProgressDialog.StartAsyncTask();

            //ProgressDialog.SetMaximumNumOfImagesComplited(fileNamesMass.Length); //ставим максимум количества изображений для прогресс бара

        }
        public void CreateQuicklook(string path, string pathToQuicklookCatalog, double percent, string format) //квиклук из файла или папки с заданным процентом
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

            if (pathToPicture.Substring(pathToPicture.LastIndexOf(@"\")).Contains(".tif")) //если это tiff-файл
            {
                ProgressDialog.DoWork += CreateQuicklookOneImagePercent;
            }
            else
            {
                ProgressDialog.DoWork += CreateQuicklookInDirectoryPercent;
            }

            // ProgressDialog.DoWork += DoWork;
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);
            ProgressDialog.StartAsyncTask();
        }

        public void CreateQuicklookOneImageWidthSize(object sender, DoWorkEventArgs e) //ещё проекции и др.
        {
            ProgressDialog.SetImageName(pathToPicture, 1, 0);

            if (quicklookSize != 0) //если задано значение ширины квиклука, то высчитываем процент
            {
                percent = CalculatePercent(quicklookSize, pathToPicture);
            }
            
            if (quicklookSize <= 0 || quicklookSize >= 10000)               
            {
                throw new Exception("слишом маленький или большой размер квиклука");
            }

           // ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука

            _worker = sender as BackgroundWorker;

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

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
        }


        

        public void CreateQuicklookOneImagePercent(object sender, DoWorkEventArgs e) //ещё проекции и др.
        {
            //ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука
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
            _worker = sender as BackgroundWorker;

            CreateImage(); //создаём изображение

            ProgressDialog.SetImageName(pathToPicture, 1, 1);

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
        }

        public void CreateQuicklookOneImageSquare(object sender, DoWorkEventArgs e)
        {
            ProgressDialog.SetImageName(pathToPicture, 1, 0);

            percent = CalculateSquare(square);


            if (square <= 0 || square >= 100000000)
            {
                throw new Exception("слишом маленький или большой размер квиклука");
            }

            // ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука

            _worker = sender as BackgroundWorker;

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

            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
        }

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


            for (int i = 0; i < fileNamesMass.Length; i++)
            {
                if (_worker.CancellationPending == true)
                {
                    //e.Cancel = true;
                    break;
                }
                pathToPicture = fileNamesMass[i];

                ProgressDialog.SetImageName(fileNamesMass[i], fileNamesMass.Length, i);

                if (quicklookType.Equals("quicklookSize"))
                {
                    percent = CalculatePercent(quicklookSize, fileNamesMass[i]);
                }
                else if (quicklookType.Equals("square"))
                {
                    percent = CalculateSquare(square);
                }
                
                

                //Console.WriteLine(fileNamesMass[i]);

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
       
        private void CreateQuicklookInDirectoryWidthSize(object sender, DoWorkEventArgs e)
        {
            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }


             _worker = sender as BackgroundWorker;

            
             GoToDirectories(pathToPicture, "quicklookSize");
           
            
            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
            
        }

        
        public void CreateQuicklookInDirectoryPercent(object sender, DoWorkEventArgs e) //ещё проекции и др. //дописать, как предыдущую ф-ю
        {
            // ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }

            

            _worker = sender as BackgroundWorker;
            
            
            GoToDirectories(pathToPicture, "percent");
            

            if (isCanceled) //удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
        }
        public void CreateQuicklookInDirectorySquare(object sender, DoWorkEventArgs e)
        {

            try
            {
                Gdal.AllRegister();
            }
            catch
            {
                MessageBox.Show("Что-то не так с библиотекой gdal");
            }


            _worker = sender as BackgroundWorker;


            GoToDirectories(pathToPicture, "square");


            if (isCanceled)//удаляем файлы, если отменили создание квиклука
            {
                DeleteFiles();
            }
        }


    }
}

