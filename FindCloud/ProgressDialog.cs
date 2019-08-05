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

        public ProgressDialog()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
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
        // {
        //     numOfImagesComplited.Maximum = max;
        //     numOfImagesLabel.Text = $"Снимок: {0}  {numOfImagesComplited.Maximum}";
        // }

        // public void AddOneToNumOfImagesComplited()
        // {
        //     if (numOfImagesComplited.Value < numOfImagesComplited.Maximum)
        //     {
        //         numOfImagesComplited.Value += 1;
        //         numOfImagesLabel.Text = $"Снимок: {numOfImagesComplited.Value}  {numOfImagesComplited.Maximum}";
        //     }
        // }

        // public void SetCurrentImage(string name, int index)
        // {
        //     if (index > 0 && index < numOfImagesComplited.Maximum)
        //     {
        //         numOfImagesComplited.Value = index;
        //         numOfImagesLabel.Text = $"Снимок: {name}";
        //     }
        // }

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
                return _fff;
            }
            set
            {
                _fff = value;
                backgroundWorker.DoWork += value;
            }
        }
        private System.ComponentModel.DoWorkEventHandler _fff;


        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        private void CanselButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                backgroundWorker.CancelAsync();
            }
        }

        public void StartAsyncTask()
        {
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            quickloolCreatingProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
