using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace FindCloud
{
    //надо допилить:
    //получить размер изображения, чтобы узнать, насколько его уменьшать (закончено или почти)
    //прогресс-бар, чтобы пользователь видел, сколько осталось ждать квиклук
    //минимальный интерфейс, который будет выводить картинки
    //многопоточность
    public class Quicklook
    {
        private const int QUICKLOOK_SIZE = 2000;

        private static ProgressDialog ProgressDialog { get; set; } = new ProgressDialog();

        //public string InputFile { get; set; }

        public Quicklook()
        {

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressDialog.UpdateProgress(e.ProgressPercentage);

            //this.label1.Text = e.ProgressPercentage.ToString() + "% complete";
        }

        //private void bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BackgroundWorker worker = (BackgroundWorker)sender;

        //    for (int i = 0; i < 100; ++i)
        //    {
        //        // report your progres
        //        worker.ReportProgress(i);

        //        // pretend like this a really complex calculation going on eating up CPU time
        //        System.Threading.Thread.Sleep(100);
        //    }
        //    e.Result = "42";
        //}


        private double CalculatePercent(int maxSize, string InputFile)  //высчитываем, какой процент изображения останется от исходного 
        {
            double inputFileWidth;
            double inputFileHeight;
            double coefficient;
            double quicklookWidth = maxSize;
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

            quicklookHeight = (maxSize / inputFileWidth) * inputFileHeight;

            coefficient = Math.Sqrt((quicklookHeight*quicklookWidth) / (inputFileWidth*inputFileHeight));
            Console.WriteLine(coefficient);

            coefficient = coefficient * 100; //получаем коэффициент в процентах
            Console.WriteLine(coefficient);

            return coefficient;
        }

        public void CreateQuicklookOneImage(string pathToPicture, string pathToQuicklookCatalog, int quicklookSize = 2000) //ещё проекции и др.
        {
            if (quicklookSize <= 0 || quicklookSize >= 10000)               
            {
                throw new Exception("слишом маленький или большой размер квиклука");
            }

            var pct = CalculatePercent(quicklookSize, pathToPicture);

            CreateQuicklookOneImage(pathToPicture, pathToQuicklookCatalog, pct);
        }

        public void CreateQuicklookOneImage(string pathToPicture, string pathToQuicklookCatalog, double sizePercent = 20) //ещё проекции и др.
        {
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука
            
            Console.WriteLine(pathToPicture);
            string outputFile = pathToQuicklookCatalog;
            outputFile += pathToPicture.Substring(pathToPicture.LastIndexOf(@"\"));
            outputFile = outputFile.Replace("tif", "jpg");

            Console.WriteLine(outputFile);

            Gdal.AllRegister();

            Dataset ds = Gdal.Open(pathToPicture, Access.GA_ReadOnly);

            //ProgressDialog.SetMaximumNumOfImagesComplited(1); //ставим максимум изображений для прогресс бара

            var options =
                new GDALTranslateOptions(new[]
                {
                    "-of", "JPEG",
                    "-outsize", $"{sizePercent}%", $"{sizePercent}%",
                    "-co", "WORLDFILE=YES",
                });
            try
            {
                Dataset newDs = Gdal.wrapper_GDALTranslate(outputFile, ds, options,
                    new Gdal.GDALProgressFuncDelegate(ProgressFunc), null);
            }
            catch
            {

            }
        }

        //string a = "Мир";
        //int g = 0;

        //System.Windows.Forms.MessageBox.Show(string.Format("Привет, {0}! {1:4}", a, g));
        public void CreateCloseWindowMessageBox()
        {
            System.Windows.Forms.MessageBox.Show("dfd");
        }

        public static int ProgressFunc(double Complete, IntPtr Message, IntPtr Data)
        {
            _worker.ReportProgress((int)(100 * Complete));

            //if (Complete == 0)
            //{
            //    ProgressDialog = new ProgressDialog();
            //    ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука
            //}

            //ProgressDialog.UpdateProgress((int)(Complete * 100));

            //ProgressDialog.Refresh();

            //if (Complete == 1) //если создали квиклук, обновляем прогресс бар количества изображений
            //{
            //    ProgressDialog.AddOneToNumOfImagesComplited();
            //}

            return 1;
        }

        private  static BackgroundWorker _worker;

        private int quicklookSize;

        private string pathToQuicklookCatalog;

        private string pathToPicture;

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Gdal.AllRegister();

            if (quicklookSize <= 0 || quicklookSize >= 10000)                           //сделать обработку второго прогресс бара и ещё что-то
            {
                throw new Exception("слишом маленький или большой размер квиклука");
            }

            string[] fileNamesMass = Directory.GetFiles(pathToPicture, "*.tif");



            _worker = sender as BackgroundWorker;

            //_worker = worker;

            for (int i = 0; i < fileNamesMass.Length; i++)
            {

                var pct = CalculatePercent(quicklookSize, fileNamesMass[i]);

                //Console.WriteLine(fileNamesMass[i]);

                string outputFile = pathToQuicklookCatalog;
                outputFile += fileNamesMass[i].Substring(fileNamesMass[i].LastIndexOf(@"\"));
                outputFile = outputFile.Replace("tif", "jpg");

                Dataset ds = Gdal.Open(fileNamesMass[i], Access.GA_ReadOnly);

                var options =
                    new GDALTranslateOptions(new[]
                    {
                        "-of", "JPEG",
                        "-outsize", $"{pct}%", $"{pct}%",
                        "-co", "WORLDFILE=YES",
                    });

                try
                {
                    //ProgressDialog.SetCurrentImage(fileNamesMass[i], i + 1);

                    Dataset newDs = Gdal.wrapper_GDALTranslate(outputFile, ds, options, ProgressFunc, null);
                }
                catch (Exception _e)
                {
                    System.Windows.Forms.MessageBox.Show(_e.Message);
                }
            }

            //for (int i = 1; i <= 10; i++)
            //{
            //    if (worker.CancellationPending == true)
            //    {
            //        e.Cancel = true;
            //        break;
            //    }
            //    else
            //    {
            //        // Perform a time consuming operation and report progress.
            //        System.Threading.Thread.Sleep(500);
            //        worker.ReportProgress(i * 10);
            //    }
            //}
        }



        public void CreateQuicklookInDirectory(string pathToPicture, string pathToQuicklookCatalog, int quicklookSize) 
        {




            //ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука


            this.quicklookSize = quicklookSize;
            this.pathToQuicklookCatalog = pathToQuicklookCatalog;
            this.pathToPicture = pathToPicture;

            ProgressDialog.DoWork += DoWork;
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm);
            ProgressDialog.StartAsyncTask();




            //ProgressDialog.SetMaximumNumOfImagesComplited(fileNamesMass.Length); //ставим максимум количества изображений для прогресс бара



        }

        public void CreateQuicklookInDirectory(string pathToPicture, string pathToQuicklookCatalog, double sizePercent = 20) //ещё проекции и др. //дописать, как предыдущую ф-ю
        {
            ProgressDialog.Show(System.Windows.Forms.Form.ActiveForm); //показываем процесс создания квиклука

            Gdal.AllRegister();

            string[] fileNamesMass = Directory.GetFiles(pathToPicture, "*.tif");

            //ProgressDialog.SetMaximumNumOfImagesComplited(fileNamesMass.Length); //ставим максимум количества изображений для прогресс бара

            for (int i = 0; i < fileNamesMass.Length; i++)
            {
                //InputFile = fileNamesMass[i];
                Console.WriteLine(fileNamesMass[i]);

                string outputFile = pathToQuicklookCatalog;
                outputFile += fileNamesMass[i].Substring(fileNamesMass[i].LastIndexOf(@"\"));
                outputFile = outputFile.Replace("tif", "jpg");

                Dataset ds = Gdal.Open(fileNamesMass[i], Access.GA_ReadOnly);

                var options =
                    new GDALTranslateOptions(new[]
                    {
                    "-of", "JPEG",
                    "-outsize", $"{sizePercent}%", $"{sizePercent}%",
                    "-co", "WORLDFILE=YES",
                    });

                try
                {
                    Dataset newDs = Gdal.wrapper_GDALTranslate(outputFile, ds, options,
                    new Gdal.GDALProgressFuncDelegate(ProgressFunc), null);
                }
                catch
                {

                }

            }
        }


        //int oldCompl = 0;
        //int newCompl;




    }
}

