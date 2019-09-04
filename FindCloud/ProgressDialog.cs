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
    /// Форма, в которой отображается процесс создания квиклука
    /// </summary>
    public partial class ProgressDialog : Form
    {
        public bool IsWorkPaused { get; set; } = false;
        
        public delegate void SetImageNameDelegate(string imageName, int numberOfImages, int currentImage);

        public delegate void ChangeProgressDelegate(int progress);
        public delegate void CloseProgressDelegate();

        
        public void CloseProgressWindow()
        {
            this.BeginInvoke(new CloseProgressDelegate(() => this.Close()));
        }

        /// <summary>
        /// Меняет информацию о квиклуке в форме
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="numberOfImages"></param>
        /// <param name="currentImage"></param>
        private void SetImageNameMethod(string imageName, int numberOfImages, int currentImage)
        {
            numOfImagesLabel.Text = $"Снимок: {imageName}";
            numOfImagesComplited.Maximum = numberOfImages;
            numOfImagesComplited.Value = currentImage;
        }

        //Вызывает SetImageNameMethod в потоке, в котором была создана форма
        public void SetImageName(string imageName, int numberOfImages, int currentImage)
        {
            object[] myArray = new object[3];

            myArray[0] = imageName;
            myArray[1] = numberOfImages;
            myArray[2] = currentImage;
            numOfImagesComplited.BeginInvoke(new SetImageNameDelegate(SetImageNameMethod), myArray);
        }

        /// <summary>
        /// Отображает процесс создания квиклука в прогресс баре
        /// </summary>
        /// <param name="progress">значения 1-100%</param>
        private void ChangeProgressMethod(int progress)
        {
            quicklookCreatingProgressBar.Value = progress;
            quicklookCreatingPercent.Text = $"{progress}%";
        }
        public void SetQuicklookCreatingProgress(int progress)
        {
            quicklookCreatingProgressBar.BeginInvoke(new ChangeProgressDelegate(ChangeProgressMethod), progress);
        }

        public ProgressDialog()
        {
            InitializeComponent();

            numOfImagesComplited.Minimum = 0;

            //backgroundWorker = new BackgroundWorker();
            //backgroundWorker.WorkerReportsProgress = true;
            //backgroundWorker.WorkerSupportsCancellation = true;
            //this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            //this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            
        }

        // public bool IsCanseled { get; set; } = false;
        // public ProgressDialog()
        // {
        //     

        //     numOfImagesComplited.Minimum = 0;
        //     quickloolCreatingProgressBar.Minimum = 0;
        //     quickloolCreatingProgressBar.Maximum = 100;
        //    // canselButton.Click += CanselButton_Click;
        // }

        //public void SetMaximumNumOfImagesComplited(int max)
        //{
        //    numOfImagesComplited.Maximum = max;
        //    numOfImagesLabel.Text = $"Снимок: {0}  {numOfImagesComplited.Maximum}";
        //}

        //public void AddOneToNumOfImagesComplited()
        //{
        //    if (numOfImagesComplited.Value < numOfImagesComplited.Maximum)
        //    {
        //        numOfImagesComplited.Value += 1;
        //        numOfImagesLabel.Text = $"Снимок: {numOfImagesComplited.Value}  {numOfImagesComplited.Maximum}";
        //    }
        //}

        //public void SetCurrentImage(string name, int index)
        //{
        //    if (index > 0 && index < numOfImagesComplited.Maximum)
        //    {
        //        numOfImagesComplited.Value = index;
        //        numOfImagesLabel.Text = $"Снимок: {name}";
        //    }
        //}

        // public void UpdateProgress(int complete)
        // {
        //     if (complete <= 100)
        //     {
        //         quickloolCreatingProgressBar.Value = complete;
        //         quicklookCreatingPercent.Text = $"{complete}%";
        //     }
        //     else
        //     {
        //         quickloolCreatingProgressBar.Value = 100;
        //         quicklookCreatingPercent.Text = "100%";
        //     }

        // }

        //public System.ComponentModel.DoWorkEventHandler DoWork
        //{
        //    get
        //    {
        //        return createQuicklook;
        //    }
        //    set
        //    {
        //        createQuicklook = value;
        //        backgroundWorker.DoWork += value;
        //    }
        //}
        


        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}
        
        private void CanselButton_Click(object sender, EventArgs e)
        {
            IsWorkPaused = true;
            //if (backgroundWorker.WorkerSupportsCancellation == true)
            //{
            //    IsWorkPaused = true;
            //}
        }

        //public void StartAsyncTask()
        //{
        //    if (backgroundWorker.IsBusy != true)
        //    {
        //        backgroundWorker.RunWorkerAsync(quicklookProgress);
        //    }
        //}

        //private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    //numOfImagesLabel.Text = $"Снимок: {((QuicklookProgress)e.UserState).NameOfImage}";
        //    //numOfImagesComplited.Maximum = ((QuicklookProgress)e.UserState).NumOfImages;

        //    quicklookCreatingProgressBar.Value = e.ProgressPercentage;
        //    quicklookCreatingPercent.Text = $"{e.ProgressPercentage}%";

        //    //if(quicklookCreatingProgressBar.Value == 100)
        //    //{
        //    //    numOfImagesComplited.Value += 1;
        //    //}
        //}

        //private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    this.Close();
        //}
        
    }
}
