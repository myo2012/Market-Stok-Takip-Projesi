using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=CODER\\SQLEXPRESS;Initial Catalog=kayit;Integrated Security=True;");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand komut = new SqlCommand();
        

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            
            
            sorgu.CommandText = "insert into fiyat(kodu,adi,fiyat,kar,toplam) values ('" + @textBox1.Text + "','" + @textBox2.Text + "', '" + @textBox3.Text + "','" + @textBox4.Text + "','" + @textBox5.Text + "')";
        
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Kayıt eklendi.", "Kayıt İşlemi");
            else
                MessageBox.Show("Hata!");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();


            baglan.Close();
            komut.CommandText = "select  kodu,adi,fiyat,kar,toplam from fiyat";
            da.Fill(ds, "fiyat");
            dataGridView1.DataSource = ds.Tables["fiyat"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["kar"].HeaderText = "Kar";
            dataGridView1.Columns["toplam"].HeaderText = "Toplam";
            


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                       
             try
            {

                int KayıtSayısı;
                DataTable veriler = new DataTable("malzeme");
                DataRow satir;
                KayıtSayısı = 0;
                baglan.Open();
                komut.CommandText = "select * from malzeme where Kodu= '" + textBox1.Text + "'";
                da.SelectCommand = komut;
                komut.Connection = baglan;
                da.Fill(veriler);
                satir = veriler.Rows[KayıtSayısı];
                textBox2.Text = satir[1].ToString();
                baglan.Close();
                

            }
            catch
            { }
             
             
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox5.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox4.Text));
            }
            catch { }
            }

        private void Form3_Load(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT kodu,adi,fiyat,kar,toplam FROM fiyat", baglan);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["kar"].HeaderText = "Kar";
            dataGridView1.Columns["toplam"].HeaderText = "Toplam";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "delete from fiyat where kodu='" + @textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MessageBox.Show("Silme İşleminiz Gerçekleştirilmiştir");
            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "select * from fiyat";
            da.Fill(ds, "fiyat");
            dataGridView1.DataSource = ds.Tables["fiyat"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["kar"].HeaderText = "Kar";
            dataGridView1.Columns["toplam"].HeaderText = "Toplam";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "update fiyat set adi='" + @textBox2.Text + "', fiyat= '" + @textBox3.Text + "',kar='" + @textBox4.Text + "',toplam='" + @textBox5.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show(textBox2.Text + " Malzemesi Güncellendi");

            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "select * from fiyat";
            da.Fill(ds, "fiyat");
            dataGridView1.DataSource = ds.Tables["fiyat"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["kar"].HeaderText = "Kar";
            dataGridView1.Columns["toplam"].HeaderText = "Toplam";
        }

    }
}
