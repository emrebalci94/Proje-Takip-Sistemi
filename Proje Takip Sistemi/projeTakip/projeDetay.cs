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
    public partial class projeDetay : Form
    {
        public projeDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        string projeId;
        private void projeListele()
        {
            try
            {
                comboBox1.Items.Clear();
                listView1.Clear();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select * From proje where durum='1'", baglanti);
                SqlDataReader yaz = cmd.ExecuteReader();
                while (yaz.Read())
                {

                    comboBox1.Items.Add(yaz["id"]+"-"+yaz["gorevBasligi"]);

                }
                baglanti.Close();
            }
            catch (Exception)
            {


            }

        }
        private void BitirilenprojeListele(string cmd)
        {
            try
            {
                listView2.Clear();
                baglanti.Open();
                listView2.Columns.Add("Görev Başlığı", 150);
                listView2.Columns.Add("Görev Adı", 150);
                listView2.Columns.Add("Baslangıç Tarihi", 100);
                listView2.Columns.Add("Bitiş Tarihi", 100);
                listView2.View = View.Details;
                listView2.GridLines = true;
                ListViewItem l = new ListViewItem();

                SqlCommand komut = new SqlCommand(cmd, baglanti);
                SqlDataReader o = komut.ExecuteReader();
                while (o.Read())
                {
                    l = listView2.Items.Add(o.GetString(1));
                    l.SubItems.Add(o.GetString(2));
                    l.SubItems.Add(o.GetString(3));
                    l.SubItems.Add(o.GetString(4));
                }
                baglanti.Close();
            }
            catch (Exception)
            {




            }

        }

        private void projeDurumListele(string cmd)
        {
            try
            {
                listView3.Clear();
                baglanti.Open();
                listView3.Columns.Add("Görev Başlığı", 140);
                listView3.Columns.Add("Görev Adı", 140);
                listView3.Columns.Add("Durum", 100);
                listView3.View = View.Details;
                listView3.GridLines = true;
                ListViewItem l = new ListViewItem();

                SqlCommand komut = new SqlCommand(cmd, baglanti);
                SqlDataReader o = komut.ExecuteReader();
                while (o.Read())
                {
                    l = listView3.Items.Add(o.GetString(1));
                    l.SubItems.Add(o.GetString(2));
                    if (o.GetInt32(5) == 0)
                    {
                        l.SubItems.Add("Başlanmış");
                    }
                    else if (o.GetInt32(5)== 1)
                    {
                        l.SubItems.Add("Bitirilmiş");
                    }
                    else
                    {
                        l.SubItems.Add("Bilgiye Ulaşılamadı");
                    }
                   
                }
                baglanti.Close();
            }
            catch (Exception h)
            {

                MessageBox.Show(h.Message);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                groupBox1.Visible = true;
                groupBox2.Visible=false;
                groupBox3.Visible = false;
                projeListele();
                if (comboBox1.Items.Count >0)
                {
                      comboBox1.SelectedIndex = 0;
                }
              
                

            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        string table,cmd;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                table = "";
                int bak;
                string topla = "";
                bak = comboBox1.Text.IndexOf("-");
                for (int i = 0; i < bak; i++)
                {
                    topla = topla + comboBox1.Text[i];
                }
                for (int i = bak+1; i < comboBox1.Text.Length; i++)
                {
                    table = table + comboBox1.Text[i]; 
                }
                projeId = topla;
                listView1.Clear();
               // table = comboBox1.Text.ToLower();
                cmd = "select * from " + table+projeId;
                baglanti.Open();
                listView1.Columns.Add("Görevli Adı", 150);
                listView1.Columns.Add("Görevi", 150);
                listView1.View = View.Details;
                listView1.GridLines = true;
                ListViewItem l = new ListViewItem();

                SqlCommand komut = new SqlCommand(cmd, baglanti);
                SqlDataReader o = komut.ExecuteReader();
                while (o.Read())
                {
                    l = listView1.Items.Add(o.GetString(2));
                    l.SubItems.Add(o.GetString(1));
                }
                baglanti.Close();
            }
            catch (Exception)
            {

                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (groupBox2.Visible == false)
            {
                groupBox3.Visible = false;
                groupBox2.Visible=true;
                groupBox1.Visible = false;
                BitirilenprojeListele("select * from proje where durum='1'");
                //this.Width = 600;
            }
            else
            {
                groupBox2.Visible = false;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (groupBox3.Visible == false)
            {
                
                groupBox3.Visible = true;
                groupBox2.Visible = false;
                groupBox1.Visible = false;
                projeDurumListele("select * from proje");
                
            }
            else if (groupBox3.Visible == true)
            {
                groupBox3.Visible = false;
            }
        }

        private void projeDetay_Load(object sender, EventArgs e)
        {
           

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BitirilenprojeListele("select * from proje where gorevBasligi like '%"+textBox1.Text+"%' and durum='1'");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            projeDurumListele("select * from proje where gorevBasligi like '%"+textBox2.Text+"%'");
        }
    }
}
