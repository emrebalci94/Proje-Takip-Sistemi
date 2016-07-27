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
    public partial class gorevliIslemleri : Form
    {
        public gorevliIslemleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        private bool ara(string a, string b)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From gorevli where adi='" + a + "' and soyad='" + b + "'", baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adtr.Fill(tablo);
            baglanti.Close();
            bool bak = false;
            foreach (DataRow dt in tablo.Rows)
            {
                if (dt["adi"].ToString() == a && dt["soyad"].ToString() == b)
                {
                    bak = true;
                    break;
                }
            }

            return bak;
        }

        private void Ekle()
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Insert Into gorevli(adi,soyad,tel,email,gorev) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text.ToString() + "','" + textBox3.Text + "','" + comboBox1.Text + "')";
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void Guncelle()
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Update gorevli set adi='" + textBox6.Text + "',soyad='" + textBox5.Text + "',tel='" + maskedTextBox2.Text + "',email='"+textBox4.Text+"',gorev='" + comboBox2.Text + "' where id='"+id+"'";
            komut.ExecuteNonQuery();
            baglanti.Close();  
        }

        private void Getir()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From gorevli", baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adtr.Fill(tablo);
            baglanti.Close();
            foreach (DataRow dt in tablo.Rows)
            {
                comboBox3.Items.Add(dt["id"].ToString() + "-" + dt["adi"].ToString() + " " + dt["soyad"].ToString());
                comboBox4.Items.Add(dt["id"].ToString() + "-" + dt["adi"].ToString() + " " + dt["soyad"].ToString());
            }
        }

        private void yerlestir()
        {
            if (id!="")
            {
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From gorevli where id='" + id+"'" , baglanti);
                DataTable tablo = new DataTable();
                baglanti.Open();
                adtr.Fill(tablo);
                baglanti.Close();
                foreach (DataRow dt in tablo.Rows)
                {
                    textBox6.Text = dt["adi"].ToString();
                    textBox5.Text = dt["soyad"].ToString();
                    textBox4.Text = dt["email"].ToString();
                    maskedTextBox2.Text = dt["tel"].ToString();
                    comboBox2.Text = dt["gorev"].ToString();
                } 
            }
           
        }

        private void sil()
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Delete From gorevli Where id='" + id +"'";
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        string id;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void gorevliIslemleri_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Size = new Size(500, 750);

            this.FormBorderStyle = FormBorderStyle.None;
        }

     
        private void gorevliIslemleri_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            if (groupBox1.Visible == false)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }
        bool sonuc;
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Görevli Eklensin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj == DialogResult.Yes)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && sonuc == false)
                {
                    try
                    {
                        Ekle();
                        MessageBox.Show("Görevli başarı ile eklendi.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        comboBox1.SelectedIndex = 0;
                        maskedTextBox1.Clear();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Böyle bir kullanıcı bulunmakta ya da Adı veya Soyadı kısmı boş geçilemez.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sonuc = ara(textBox1.Text, textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Items.Clear();
                Getir();
                comboBox3.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                groupBox1.Visible = false;
                groupBox3.Visible = false;
                if (groupBox2.Visible == false)
                {
                    groupBox2.Visible = true;
                }
                else
                {
                    groupBox2.Visible = false;
                }
            }
            catch (Exception)
            {
                 MessageBox.Show("Görevli Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //İd yi Almak için ve Bilgileri Yerleştirmek için...
            try
            {
                int bak;
                string topla = "";
                bak = comboBox3.Text.IndexOf("-");
                for (int i = 0; i < bak; i++)
                {
                    topla = topla + comboBox3.Text[i];
                }
                id = topla;
                yerlestir();

            }
            catch (Exception)
            {
                MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
          
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Görevli Güncellensin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj==DialogResult.Yes)
            {
                if (textBox6.Text != "" && textBox5.Text != "" && comboBox3.Text!="")
                {
                    try
                    {
                        Guncelle();
                        MessageBox.Show("Görevli Başarı İle Güncellendi.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox6.Clear();
                        textBox5.Clear();
                        textBox4.Clear();
                        comboBox2.SelectedIndex = 0;
                        maskedTextBox2.Clear();
                        comboBox3.Items.Clear();
                        Getir();
                        comboBox3.SelectedIndex = 0;
                        yerlestir();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Lütfen Görevli Seçtiğinizden Emin Olun...Adı ve Soyadı Alanları Boş Geçilemez...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox4.Items.Clear();
                Getir();
               
                comboBox4.SelectedIndex = 0;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                if (groupBox3.Visible == false)
                {
                    groupBox3.Visible = true;
                }
                else
                {
                    groupBox3.Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Görevli Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
DialogResult mesaj = MessageBox.Show("Görevli Silinsin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
if (mesaj == DialogResult.Yes)
{
    if (comboBox4.Text != "")
    {
        try
        {
            sil();
            MessageBox.Show("Görevli Başarı İle Silindi.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBox4.Items.Clear();
            Getir();
            if (comboBox4.Items.Count>0)
            {
                comboBox4.SelectedIndex = 0;
            }
            
        }
        catch (Exception)
        {

            MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
    else
    {
        MessageBox.Show("Lütfen Görevli Seçtiğinizden Emin Olun.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int bak;
                string topla = "";
                bak = comboBox4.Text.IndexOf("-");
                for (int i = 0; i < bak; i++)
                {
                    topla = topla + comboBox4.Text[i];
                }
                id = topla;
            }
            catch (Exception)
            {
                MessageBox.Show("Upps! Bir sorun ile karşılaşıldı. Lütfen programı tekrar başlatın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            gorevliSecim.durum = "Guncelle";
            gorevliSecim g = new gorevliSecim();  
            g.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            gorevliSecim.durum = "Sil";
            gorevliSecim g = new gorevliSecim();
            g.ShowDialog();
        }

    }
}
