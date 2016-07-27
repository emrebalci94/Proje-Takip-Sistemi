using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace projeTakip
{
    public partial class acilis : Form
    {
        public acilis()
        {
            InitializeComponent();
        }

        private void acilis_Load(object sender, EventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            anaForm a = new anaForm();
            a.Show();
            this.Hide();
            timer1.Stop();
        }
    }
}
