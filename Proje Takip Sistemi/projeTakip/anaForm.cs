using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace projeTakip
{
    public partial class anaForm : Form
    {
        public anaForm()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        gorevIslemleri gi = new gorevIslemleri();
        gorevliIslemleri goi = new gorevliIslemleri();
        projeIslemleri p = new projeIslemleri();
        projeDurum pdurum = new projeDurum();
        projeDetay pdetay = new projeDetay();
        string projeid;
        public void projeler()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From proje", baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adtr.Fill(tablo);
            baglanti.Close();
            flowLayoutPanel1.Controls.Clear();
            foreach (DataRow bak in tablo.Rows)
            {
                if (bak["durum"].ToString() == "0")
                {  
                    PictureBox b = new PictureBox();
                    Label l = new Label();
                    b.Width = 250;
                    b.Height = 50;
                    b.Text = bak["gorevBasligi"].ToString();
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.BackgroundImage = Image.FromFile(@"images\2015\ufakButonlar\projeler_butonu_alt.png");
                    b.BackgroundImageLayout = ImageLayout.Stretch;
                    b.Cursor = Cursors.Hand;
                    b.Click += new EventHandler(b_Click);
                    l.Text = bak["gorevBasligi"].ToString();
                    l.Font = new Font("Trajan Pro", 9);
                    l.Location = new Point(20, l.Location.Y+15);
                    b.Controls.Add(l);
                    flowLayoutPanel1.Controls.Add(b);
                    
                }
             
            }
        }


        void b_Click(object sender, EventArgs e)// Projelerden Birine Tıklanınca
        {
            pdurum.Hide();
            pdurum.label1.Text = (sender as PictureBox).Text + " Projesi";
            pdurum.textBox1.Text = (sender as PictureBox).Text + " Projesi";
            pdurum.projeAdi = (sender as PictureBox).Text;
            pdurum.TopLevel = false;
            pdurum.Location = new Point(461, 176);
            this.Controls.Add(pdurum);
            pdurum.Show();
            pdurum.BringToFront();
            gi.Hide();
            p.Hide();
            goi.Hide();
            pdetay.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)//proje
        {
            p.TopLevel = false;
            p.Location = new Point(700,176);
            this.Controls.Add(p);
            p.Show();
            p.BringToFront();
            gi.Hide();
            goi.Hide();
            pdurum.Hide();
            pdetay.Hide();

        }

        private void pictureBox3_Click(object sender, EventArgs e)//görevli
        {       
            goi.TopLevel = false;
            goi.Location = new Point(700, 176);
            this.Controls.Add(goi);
            goi.Show();
            goi.BringToFront();
            p.Hide();
            gi.Hide();
            pdurum.Hide();
            pdetay.Hide();

        }

        private void pictureBox4_Click(object sender, EventArgs e)//görev
        {
            gi.TopLevel = false;
            gi.Location = new Point(700, 176);
            this.Controls.Add(gi);
            gi.Show();
            gi.BringToFront();
            p.Hide();
            goi.Hide();
            pdurum.Hide();
            pdetay.Hide();
        }

        private void anaForm_Load(object sender, EventArgs e)
        {
            projeler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pdetay.TopLevel = false;
            pdetay.Location = new Point(700, 176);
            this.Controls.Add(pdetay);
            pdetay.Show();
            pdetay.BringToFront();
            p.Hide();
            goi.Hide();
            gi.Hide();
            pdurum.Hide();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            hakkimizda h = new hakkimizda();
            h.ShowDialog();
            

            
            
        }

    }
}
