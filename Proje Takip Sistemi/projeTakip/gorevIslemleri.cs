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
    public partial class gorevIslemleri : Form
    {
        public gorevIslemleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        bool durum;
        private string duzenleGorevli(ComboBox c, ComboBox y)
        {
            try
            {
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From " + c.Text + " Where gAdi='" + y.Text + "'", baglanti);
                DataTable tablo = new DataTable();
                baglanti.Open();
                adtr.Fill(tablo);
                baglanti.Close();
                string gonder = "";
                foreach (DataRow dt in tablo.Rows)
                {
                    if (dt["gGorevli"] != "")
                    {
                        gonder = dt["gGorevli"].ToString();
                        break;
                    }

                }
                return gonder;
            }
            catch (Exception)
            {
                baglanti.Close();
                return "";
            }
        }

        private bool ara(string a,ComboBox c)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("use projeler Select * From "+  c.Text+" where gAdi='" + a+ "'", baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adtr.Fill(tablo);
            baglanti.Close();
            bool bak = false;
            foreach (DataRow dt in tablo.Rows)
            {
                if (dt["gAdi"].ToString() == a)
                {
                    bak = true;
                    break;
                }
            }

            return bak;
        }

        private void projeIsimleri(ComboBox c)
        {
            try
            {
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From proje where durum='0'", baglanti);
                DataTable tablo = new DataTable();
                baglanti.Open();
                adtr.Fill(tablo);
                baglanti.Close();
                c.Items.Clear();
                foreach (DataRow dt in tablo.Rows)
                {
                    c.Items.Add(dt["gorevBasligi"].ToString());
                }
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

                baglanti.Close();
            }
       
            
        }

        private void gorevliIsimler(ComboBox c)
        {
            try
            {
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From gorevli ", baglanti);
                DataTable tablo = new DataTable();
                baglanti.Open();
                adtr.Fill(tablo);
                baglanti.Close();
                c.Items.Clear();
                foreach (DataRow dt in tablo.Rows)
                {
                    c.Items.Add(dt["adi"].ToString() + " " + dt["soyad"].ToString());
                }
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                baglanti.Close();
               
            }
         
        }

        private void gorevIsimleri(ComboBox c,ComboBox y)
        {
            try
            {
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From " + y.Text, baglanti);
                DataTable tablo = new DataTable();
                baglanti.Open();
                adtr.Fill(tablo);
                baglanti.Close();
                c.Items.Clear();
                foreach (DataRow dt in tablo.Rows)
                {
                    c.Items.Add(dt["gAdi"].ToString());
                }
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                baglanti.Close();  
            }
          
        }
      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            projeIsimleri(comboBox1);
            gorevliIsimler(comboBox2);
            if (comboBox1.Items.Count==0 || comboBox2.Items.Count==0)
            {
                MessageBox.Show("Aktif Proje Bulunmamaktadır veya Görevli Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBox8.Visible = false;
            }
            else
            {
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
         
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void gorevIslemleri_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Size = new Size(500, 750);

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            projeIsimleri(comboBox3);
            gorevliIsimler(comboBox4);
            gorevIsimleri(comboBox5, comboBox3);
            if (comboBox3.Items.Count == 0 || comboBox4.Items.Count==0 || comboBox5.Items.Count==0)
            {
                MessageBox.Show("Aktif Proje Bulunmamaktadır, Görevli Bulunmamaktadır veya Güncellenicek Görev Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBox7.Visible = false;
            }
            else
            {
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
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            projeIsimleri(comboBox6);
            gorevIsimleri(comboBox7, comboBox6);
      
            if (comboBox6.Items.Count == 0)
            {
                MessageBox.Show("Aktif Proje Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            else
            {
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
            
        }

        private void pictureBox8_Click(object sender, EventArgs e) //Görev Ekle
        {
            
            if (comboBox1.Text=="" || comboBox2.Text=="")
            {
                 MessageBox.Show("Henüz Bir Projeniz ya da Görevliniz Bulunmamaktadır. Lütfen Proje ve Görevlileri Oluşturunuz...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (durum==true)
            {
                 MessageBox.Show("Böyle Bir Görev Zaten Oluşturulmuş...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            else
            {
                try
                {
                    DialogResult mesaj = MessageBox.Show("Görev Eklensin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mesaj==DialogResult.Yes)
                    {
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.CommandText = "Insert Into " + comboBox1.Text.ToString() + "(gAdi,gGorevli,baslangic) Values('" + textBox1.Text + "','" + comboBox2.Text + "','1')";
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Görev Başarı İle Eklendi...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        gorevliIsimler(comboBox2);
                            projeIsimleri(comboBox1);
                    }
                 
                }
                catch (Exception)
                {
                      
                  MessageBox.Show("Program Bir Hata İle Karşılaştı.Lütfen Programı Tekrar Başlatıp Yeniden İşlem Yapınız...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  baglanti.Close();
                }
            }

        }

        private void pictureBox9_Click(object sender, EventArgs e)//Görev Sil
        {
            if (comboBox6.Text == "" || comboBox7.Text == "")
            {
                MessageBox.Show("Henüz Bir Projeniz ya da Göreviniz Bulunmamaktadır...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult mesaj = MessageBox.Show("Görev Silinsin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mesaj == DialogResult.Yes)
                    {
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.CommandText = "Delete From " + comboBox6.Text+ " Where gAdi='"+comboBox7.Text+"'";
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Görev Başarı İle Silindi...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gorevIsimleri(comboBox7, comboBox6);
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Program Bir Hata İle Karşılaştı.Lütfen Programı Tekrar Başlatıp Yeniden İşlem Yapınız...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            durum = ara(textBox1.Text, comboBox1);
        }

        private void pictureBox7_Click(object sender, EventArgs e)//Görev Güncelle
        {
            if (comboBox3.Text == "" || comboBox4.Text == "")
            {
                MessageBox.Show("Henüz Bir Projeniz ya da Göreviniz Bulunmamaktadır...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox5.Text=="")
            {
                 MessageBox.Show("Lütfen Bilgileri Güncellenecek Olan Görevi Seçiniz...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            else if (textBox2.Text=="")
            {
                 MessageBox.Show("Görev Adı Kısmı Boş Bırakılamaz...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            else
            {
                try
                {
                    DialogResult mesaj = MessageBox.Show("Görev Güncellensin mi ?", "Proje Takip Sistemi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mesaj == DialogResult.Yes)
                    {
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.CommandText = "Update " + comboBox3.Text + " Set gAdi='"+textBox2.Text+"',gGorevli='"+comboBox4.Text +"' Where gAdi='" + comboBox5.Text + "'";
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Görev Başarı İle Güncellendi...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        projeIsimleri(comboBox3);
                        gorevliIsimler(comboBox4);
                        gorevIsimleri(comboBox5, comboBox3);
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Program Bir Hata İle Karşılaştı.Lütfen Programı Tekrar Başlatıp Yeniden İşlem Yapınız...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
           string bak=duzenleGorevli(comboBox3,comboBox5);
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf(bak);
            textBox2.Text = comboBox5.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            gorevIsimleri(comboBox5, comboBox3);
            if (comboBox5.Items.Count>0)
            {
                gorevliIsimler(comboBox4);
            }
            else
            {
                comboBox4.Items.Clear();
                textBox2.Clear();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            gorevIsimleri(comboBox7, comboBox6);
        }

        private void gorevIslemleri_Load(object sender, EventArgs e)
        {

        }

        private void gorevIslemleri_VisibleChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            gorevliSecim.durum = "gorevEkle";
            gorevliSecim g = new gorevliSecim();
            g.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            gorevliSecim.durum = "gorevGuncelle";
            gorevliSecim g = new gorevliSecim();
            g.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            projeSecim.durum = "gorevEkle";
            projeSecim g = new projeSecim();
            g.ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            projeSecim.durum = "gorevGuncelle";
            projeSecim g = new projeSecim();
            g.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            projeSecim.durum = "gorevSil";
            projeSecim g = new projeSecim();
            g.ShowDialog();
        }

    }
}
