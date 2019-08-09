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
    public partial class ProgressDialog : Form
    {
        private BackgroundWorker backgroundWorker;
        public bool IsWorkPaused { get; set; } = false;

        private System.ComponentModel.DoWorkEventHandler createQuicklook;

        private QuicklookProgress quicklookProgress = new QuicklookProgress();


        public delegate void MyDelegate(string imageName, int numberOfImages, int currentImage);

        public void DelegateMethod(string imageName, int numberOfImages, int currentImage)
        {
            numOfImagesLabel.Text = $"Снимок: {imageName}";
            numOfImagesComplited.Maximum = numberOfImages;
            numOfImagesComplited.Value = currentImage;
        }

        public void SetImageName(string imageName, int numberOfImages, int currentImage)
        {
            object[] myArray = new object[3];

            myArray[0] = imageName;
            myArray[1] = numberOfImages;
            myArray[2] = currentImage;
            numOfImagesComplited.BeginInvoke(new MyDelegate(DelegateMethod), myArray);
        }

        public ProgressDialog()
        {
            InitializeComponent();

            numOfImagesComplited.Minimum = 0;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            
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

        public System.ComponentModel.DoWorkEventHandler DoWork
        {
            get
            {
                return createQuicklook;
            }
            set
            {
                createQuicklook = value;
                backgroundWorker.DoWork += value;
            }
        }
        


        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        private void CanselButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                IsWorkPaused = true;
                
                
            }
        }

        public void StartAsyncTask()
        {
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync(quicklookProgress);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //numOfImagesLabel.Text = $"Снимок: {((QuicklookProgress)e.UserState).NameOfImage}";
            //numOfImagesComplited.Maximum = ((QuicklookProgress)e.UserState).NumOfImages;

            quicklookCreatingProgressBar.Value = e.ProgressPercentage;
            quicklookCreatingPercent.Text = $"{e.ProgressPercentage}%";

            //if(quicklookCreatingProgressBar.Value == 100)
            //{
            //    numOfImagesComplited.Value += 1;
            //}
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
