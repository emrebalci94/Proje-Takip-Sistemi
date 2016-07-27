using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projeTakip
{
    public partial class projeIslemleri : Form
    {
        public projeIslemleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        DataTable tablo = new DataTable();
        SqlCommand komut = new SqlCommand();
        private void temizle()
        {
            if (comboBox1.Items.Count>0)
            {
                comboBox1.SelectedIndex = 0; 
            }
            if (comboBox2.Items.Count>0)
            {
                  comboBox2.SelectedIndex = 0; 
            }
        }
        private void listele()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From proje", baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adtr.Fill(tablo);
            baglanti.Close();
        }
        private void projeListele()
        {
            try
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select * From proje where durum='0'", baglanti);
                SqlDataReader yaz = cmd.ExecuteReader();
                while (yaz.Read())
                {

                    comboBox1.Items.Add(yaz["gorevBasligi"]);
                    comboBox2.Items.Add(yaz["gorevBasligi"]);
                }
                baglanti.Close();
            }
            catch (Exception)
            {
                
        
            }
            
        }
        private void ekle()
        {
            string s = textBox1.Text.Replace(" ","");
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Create Table " + s + "(id int IDENTITY(1,1) primary key,gAdi nvarchar(50),gGorevli nvarchar(50),baslangic smallint,devam smallint,bitis smallint)";
            komut.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Insert into proje(gorevBasligi,gorevAdi,baslangic,bitis,durum) values('" + s+ "','" + textBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "','" + dateTimePicker2.Value.ToShortDateString() + "','0')";
            komut.ExecuteNonQuery();
            baglanti.Close();
        }


        private void projeGuncelle()
        {
            try
            {

                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Update proje set gorevBasligi='" + textBox3.Text + "',gorevAdi='" + textBox4.Text + "',baslangic='" + dateTimePicker4.Value.ToShortDateString() + "',bitis='" + dateTimePicker3.Value.ToShortDateString() + "' where gorevBasligi='" + comboBox1.Text + "'";
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "sp_rename '" + comboBox1.Text + "','" + textBox3.Text + "' ";
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İşleminiz Başarı İle Gerçekleştirildi...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                projeListele();
                temizle();
                
                
            }
            catch (Exception)
            {

                MessageBox.Show("İşleminizde Beklenmedik Bir Hata Oluştu...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                projeListele();
            }

        }
        private void projeSil()
        {
            try
            {
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Drop TABLE " + comboBox2.Text;
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "delete from proje where gorevBasligi='" + comboBox2.Text + "'";
                komut.ExecuteNonQuery();
                baglanti.Close();
         
                MessageBox.Show("İşleminiz Başarı İle Gerçekleştirildi...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                projeListele();
                temizle();


            }
            catch (Exception)
            {

                MessageBox.Show("İşleminizde Beklenmedik Bir Hata Oluştu...", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                projeListele();
            }
         
        }
        private void olustur()
        {
            try
            {
                ekle();
                listele();
                MessageBox.Show("Proje Başarı İle Oluşturuldu...", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            catch (Exception)
            {

                MessageBox.Show("Böyle Bir Proje Bulunmakta ya da Proje Başlığını ve Proje Adını Girdiğinizden Emin Olun.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void projeIslemleri_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState!= FormWindowState.Minimized)
            {
                 this.WindowState = FormWindowState.Normal;
            }
            this.Size = new Size(500, 750);

            this.FormBorderStyle = FormBorderStyle.None;
         
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            if (groupBox1.Visible==false)
            {
                groupBox1.Visible = true;
            }
            else
            {
              groupBox1.Visible = false;
            }
                
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            projeListele();
            temizle();
            if (comboBox1.Items.Count>0)
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
            else
            {
                MessageBox.Show("Aktif Proje Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            projeListele();
            temizle();
            if (comboBox2.Items.Count>0)
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
            else
            {
                MessageBox.Show("Aktif Proje Bulunmamaktadır.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text=="")
            {
                MessageBox.Show("Lütfen Proje Başlığını ve Adını boş geçmeyiniz.","Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult sonuc = MessageBox.Show("Proje Oluşturulsun mu ?","Oluşturulsun mu ?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (sonuc==DialogResult.Yes)
                {
                    olustur();
                    anaform = (anaForm)Application.OpenForms["anaForm"];
                    anaform.projeler();
                }
            }
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsPunctuation(e.KeyChar);
            if (textBox1.TextLength<1)
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            
           
            if (textBox3.Text=="" || comboBox1.Text=="")
            {
                MessageBox.Show("Lütfen Proje Başlığını Boş Geçmeyin veya Projeyi Seçmeyi Unutmayın.", "Proje Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult sonuc = MessageBox.Show("Proje Güncellensin mi ?", "Güncellensin mi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    projeGuncelle();
                    anaform = (anaForm)Application.OpenForms["anaForm"];
                    anaform.projeler();
                }
            }
            
        }

        private void projeIslemleri_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select * From proje where gorevBasligi='" + comboBox1.Text + "'", baglanti);
                SqlDataReader yaz = cmd.ExecuteReader();
                yaz.Read();
                textBox3.Text = yaz["gorevBasligi"].ToString();
                textBox4.Text = yaz["gorevAdi"].ToString();
                dateTimePicker4.Value = Convert.ToDateTime(yaz["baslangic"]);
                dateTimePicker3.Value = Convert.ToDateTime(yaz["bitis"]);
                baglanti.Close();
            }
            catch (Exception)
            {

            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Proje Silinsin mi ?", "Silinsin mi ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    projeSil();
                    anaform = (anaForm)Application.OpenForms["anaForm"];
                    anaform.projeler();
                }
            }
        }

        private void groupBox3_VisibleChanged(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count>0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        private void groupBox2_VisibleChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        anaForm anaform;
        private void textBox3_Leave(object sender, EventArgs e)
        {

           
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

            projeSecim.durum = "Guncelle";
            projeSecim p = new projeSecim();
            p.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            projeSecim.durum = "Sil";
            projeSecim p = new projeSecim();
            p.ShowDialog();
        }

 
 
    }
}
