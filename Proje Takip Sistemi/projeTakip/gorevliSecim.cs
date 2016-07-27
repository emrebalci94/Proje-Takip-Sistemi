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
    public partial class gorevliSecim : Form
    {
        public gorevliSecim()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURUZUN\ONURUZUN; Initial Catalog=projeler;Integrated Security=True");
        public static string durum;
        private void listele(string sql)
        {
            listView1.Clear();
            baglanti.Open();
            listView1.Columns.Add("ID", 150);
            listView1.Columns.Add("ADI SOYADI", 150);
            listView1.Columns.Add("E-MAİL", 150);
            listView1.Columns.Add("GÖREVİ", 70);
            listView1.View = View.Details;
            listView1.GridLines = true;
            ListViewItem l = new ListViewItem();

            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataReader o = komut.ExecuteReader();
            while (o.Read())
            {
                
                l = listView1.Items.Add(o.GetInt32(0).ToString());
                l.SubItems.Add(o.GetString(1)+" "+o.GetString(2));
                l.SubItems.Add(o.GetString(4));
                l.SubItems.Add(o.GetString(5));

            }
            baglanti.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                gorevliIslemleri g = Application.OpenForms["gorevliIslemleri"] as gorevliIslemleri;
                gorevIslemleri go = Application.OpenForms["gorevIslemleri"] as gorevIslemleri;
                if (durum == "Guncelle")
                {

                    int indexNo = g.comboBox3.FindStringExact(listView1.SelectedItems[0].SubItems[0].Text + "-" + listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        g.comboBox3.SelectedIndex = indexNo;

                    }      
                }
                else if (durum=="Sil")
                {

                    int indexNo = g.comboBox4.FindStringExact(listView1.SelectedItems[0].SubItems[0].Text + "-" + listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        g.comboBox4.SelectedIndex = indexNo;

                    }                  
                }
                else if (durum=="gorevEkle")
                {
                     int indexNo = go.comboBox2.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        go.comboBox2.SelectedIndex = indexNo;

                    }    
                }
                else if (durum=="gorevGuncelle")
                {
                     int indexNo = go.comboBox4.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        go.comboBox4.SelectedIndex = indexNo;

                    }    
                }
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void gorevliSecim_Load(object sender, EventArgs e)
        {
            listele("Select * From gorevli");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listele("Select * From gorevli Where adi like '%" + textBox1.Text + "%'");
            }
            catch (Exception)
            {

            }
        }
    }
}
