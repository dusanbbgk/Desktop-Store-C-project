using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemTrgovine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ASbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 forma2 = new Form2();
            forma2.FormToShowOnClosing = this;
            forma2.Show();
        }

        private void PNbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 forma3 = new Form3();
            forma3.FormToShowOnClosing = this;
            forma3.Show();
        }
    }
}
