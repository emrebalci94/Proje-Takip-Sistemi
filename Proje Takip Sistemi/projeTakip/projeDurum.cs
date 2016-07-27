using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
namespace projeTakip
{
    public partial class projeDurum : Form
    {
        public projeDurum()
        {
            InitializeComponent();
        }
        public string projeAdi,projeId;
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable tablo = new DataTable();
      
        private void idBul()
        {
            try
            {
                baglanti.Open();
                tablo.Clear();
                SqlDataAdapter adtr = new SqlDataAdapter("Use projeler Select * From proje where gorevBasligi='" + projeAdi + "' and durum='0'", baglanti);
                adtr.Fill(tablo);
                foreach (DataRow dr in tablo.Rows)
                {
                    projeId = dr["id"].ToString();
                }
                baglanti.Close();
            }
            catch (Exception)
            {

                baglanti.Close();
            }
        }

        private void listele( string a)
        {
            try
            {

                tablo.Clear();
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();
                flowLayoutPanel3.Controls.Clear();
                SqlDataAdapter adtr = new SqlDataAdapter("Use projeler Select * From " + a, baglanti);
                baglanti.Open();
                adtr.Fill(tablo);
                foreach (DataRow ekle in tablo.Rows)
                {
                    if (ekle["baslangic"].ToString() == "1")
                    {
                        Button b = new Button();
                        b.Location = new Point(b.Location.X, b.Location.Y + 20);
                        b.Width = 320;
                        b.Height = 50;
                        b.Font = new Font("Trajan Pro", 9, FontStyle.Regular);
                        b.Text = ekle["gAdi"].ToString();
                        b.Cursor = Cursors.Hand;
                        b.ForeColor = Color.Orange ;
                        b.BackColor = Color.Transparent;
                        b.FlatStyle = FlatStyle.Flat;
                        flowLayoutPanel1.Controls.Add(b);
                        b.BringToFront();
                        b.MouseEnter += new EventHandler(b_MouseEnter);
                        b.MouseLeave += new EventHandler(b_MouseLeave);
                        b.Click += new EventHandler(b_Click);
                    }
                    else if (ekle["devam"].ToString() == "1")
                    {
                        Button b = new Button();
                        b.Width = 320;
                        b.Height = 50;
                        b.Font = new Font("Trajan Pro", 9, FontStyle.Regular);
                        b.Text = ekle["gAdi"].ToString();
                        b.Cursor = Cursors.Hand;
                        b.ForeColor = Color.Orange;
                        b.BackColor = Color.Transparent;
                        b.FlatStyle = FlatStyle.Flat;
                        flowLayoutPanel2.Controls.Add(b);
                        b.MouseEnter += new EventHandler(b_MouseEnter);
                        b.MouseLeave += new EventHandler(b_MouseLeave);
                        b.Click += new EventHandler(b_Click);
                    }
                    else if (ekle["bitis"].ToString() == "1")
                    {
                        Button b = new Button();
                        b.Width = 320;
                        b.Height = 50;
                        b.Font = new Font("Trajan Pro", 9, FontStyle.Regular);
                        b.Text = ekle["gAdi"].ToString();
                        b.Cursor = Cursors.Hand;
                        b.BackColor = Color.Transparent;
                        b.ForeColor = Color.Orange;
                        b.FlatStyle = FlatStyle.Flat;
                        flowLayoutPanel3.Controls.Add(b);
                        b.MouseEnter += new EventHandler(b_MouseEnter);
                        b.MouseLeave += new EventHandler(b_MouseLeave);
                        b.Click += new EventHandler(b_Click);
                    }
                }
                baglanti.Close();
            }
            catch (Exception)
            {
                baglanti.Close();
            }
        }
    
        void b_Click(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor==Color.Transparent)
            {
                (sender as Button).BackColor = Color.FromArgb(0,30,0);
               // (sender as Button).BackColor = Color.DarkGreen;
               // (sender as Button).ForeColor = Color.DarkRed;
                listBox1.Items.Add(gAdi); 
            }
            else if ( (sender as Button).BackColor == Color.FromArgb(0, 30, 0))
            {
                 (sender as Button).BackColor = Color.Transparent;
                listBox1.Items.Remove(gAdi); 
            }
          
          
        }

        void b_MouseEnter(object sender, EventArgs e)
        {
            gAdi = (sender as Button).Text;
            (sender as Button).Font = new Font("Trajan Pro", 8, FontStyle.Italic);
         
            baglanti.Open();
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("Use projeler Select * From " + projeAdi + " Where gAdi='" + gAdi + "'", baglanti);
            adtr.Fill(tablo);
            foreach (DataRow dr in tablo.Rows)
            {
                (sender as Button).Text = dr["gGorevli"].ToString();
            }
            baglanti.Close();
        }
        string gAdi;
        void b_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).Text = gAdi;
            (sender as Button).Font = new Font((sender as Button).Font.Name, 9, FontStyle.Regular);
           // (sender as Button).BackColor = Color.Transparent;
        }

        private void projeDurum_Load(object sender, EventArgs e)
        {
          
            idBul();

           listele(projeAdi);
            
            
            
            //label1.Location((this.Width-label1.Width)/2,37);
            label1.Location = new Point((this.Width - label1.Width) / 2, 37);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count!=0)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                komut.CommandText = "use projeler Update " + projeAdi + " Set baslangic=0, devam=1,bitis=0 Where gAdi='" + listBox1.Items[i].ToString() + "'";
                komut.ExecuteNonQuery();
                }
                baglanti.Close();
                listele(projeAdi);
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen Görev Seçtiğinizden Emin Olun", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    komut.CommandText = "use projeler Update " + projeAdi + " Set baslangic=1, devam=0,bitis=0 Where gAdi='" + listBox1.Items[i].ToString() + "'";
                    komut.ExecuteNonQuery();
                }
                baglanti.Close();
                listele(projeAdi);
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen Görev Seçtiğinizden Emin Olun", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    komut.CommandText = "use projeler Update " + projeAdi + " Set baslangic=0, devam=0, bitis=1 Where gAdi='" + listBox1.Items[i].ToString() + "'";
                    komut.ExecuteNonQuery();
                }
                baglanti.Close();
                listele(projeAdi);
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen Görev Seçtiğinizden Emin Olun", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    komut.CommandText = "use projeler Update " + projeAdi + " Set baslangic=0, devam=1, bitis=0 Where gAdi='" + listBox1.Items[i].ToString() + "'";
                    komut.ExecuteNonQuery();
                }
                baglanti.Close();
                listele(projeAdi);
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen Görev Seçtiğinizden Emin Olun", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count==0 && flowLayoutPanel2.Controls.Count==0)
            {
                pictureBox6.Visible = true;
            }
            else
            {
                pictureBox6.Visible = false;
            }
        }
        bool sonlandır;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Görev Bitirilsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "SP_RENAME '" + projeAdi + "', '" + projeAdi + projeId + "'";
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "use projeler Update proje Set bitis='" + DateTime.Now.ToShortDateString() + "', durum=1 Where gorevBasligi='" + projeAdi + "' and durum<>1";
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    anaForm anaform = new anaForm();
                    anaform = (anaForm)Application.OpenForms["anaForm"];
                    anaform.projeler();
                    sonlandır = true;
                    this.Hide();

                }
                catch (Exception h)
                {
                    MessageBox.Show("Program Bir Hata İle Karşılaştı. Lütfen Programı Yeniden Başlatın...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }

            } 
        }
    }
}
