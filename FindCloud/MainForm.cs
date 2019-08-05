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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Quicklook ql = new Quicklook();

            ql.CreateQuicklookInDirectory(
                @"\\Server2\gil\COSMOS_RAB\2019\Канопус В\8z",
                @"C:\Users\rez\Pictures\Saburov\forQuicklooks",
                2000);
            //(new ProgressDialog()).ShowDialog(this);
        }
    }
}
