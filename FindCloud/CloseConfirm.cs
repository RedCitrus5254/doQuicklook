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
    /// Форма открывается, если нажать кнопку "отмена", когда создаётся квиклук;
    /// 
    /// </summary>
    public partial class CloseConfirm : Form
    {
        public bool IsAccepted { get; set; }
        public CloseConfirm()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsAccepted = false;
            this.Close();
        }
    }
}
