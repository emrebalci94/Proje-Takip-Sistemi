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
    public partial class projeSecim : Form
    {
        public projeSecim()
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
            listView1.Columns.Add("BAŞLIĞI", 150);
            listView1.Columns.Add("AÇIKLAMASI", 100);
            listView1.Columns.Add("BAŞLANGIÇ TARİHİ", 150);
            listView1.Columns.Add("BİTİŞ TARİHİ", 150);
            listView1.View = View.Details;
            listView1.GridLines = true;
            ListViewItem l = new ListViewItem();

            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataReader o = komut.ExecuteReader();
            while (o.Read())
            {

                l = listView1.Items.Add(o.GetInt32(0).ToString());
                l.SubItems.Add(o.GetString(1));
                l.SubItems.Add(o.GetString(2));
                l.SubItems.Add(o.GetString(3));
                l.SubItems.Add(o.GetString(4));

            }
            baglanti.Close();
        }
        private void projeSecim_Load(object sender, EventArgs e)
        {
            listele("Select * From proje Where durum='0'");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listele("Select * From proje Where gorevBasligi like '%" + textBox1.Text + "%' and durum='0' ");
            }
            catch (Exception)
            {

            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                projeIslemleri g = Application.OpenForms["projeIslemleri"] as projeIslemleri;
                gorevIslemleri go = Application.OpenForms["gorevIslemleri"] as gorevIslemleri;
                if (durum == "Guncelle")
                {

                    int indexNo = g.comboBox1.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        g.comboBox1.SelectedIndex = indexNo;

                    }
                }
                else if (durum == "Sil")
                {

                    int indexNo = g.comboBox2.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        g.comboBox2.SelectedIndex = indexNo;

                    }
                }
                else if (durum == "gorevEkle")
                {
                    int indexNo = go.comboBox1.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        go.comboBox1.SelectedIndex = indexNo;

                    }
                }
                else if (durum == "gorevGuncelle")
                {
                    int indexNo = go.comboBox3.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        go.comboBox3.SelectedIndex = indexNo;

                    }
                }
                else if (durum=="gorevSil")
                {
                     int indexNo = go.comboBox6.FindStringExact(listView1.SelectedItems[0].SubItems[1].Text);
                    if (indexNo != -1)
                    {
                        go.comboBox6.SelectedIndex = indexNo;

                    }
                }
                this.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
