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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=CODER\\SQLEXPRESS;Initial Catalog=kayit;Integrated Security=True;");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand komut = new SqlCommand();

        private void button5_Click(object sender, EventArgs e)
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
            sorgu.CommandText = "insert into malzemesatis (kodu,adii,adett,fiyatt,tarihh,toplamtutarr) values ('" + @textBox1.Text + "','" + @textBox2.Text + "', '" + @textBox3.Text + "', '" + @textBox4.Text + "', '" + @dateTimePicker1.Value.ToString("MM.dd.yyyy hh:mm:ss") + "', '" + @textBox5.Text + "')";

            if (sorgu.ExecuteNonQuery() == 1)
                listBox1.Items.Add(textBox5.Text);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            baglan.Close();
            MessageBox.Show("Alış tamamlandı.");
            komut.CommandText = "SELECT kodu,adii,adett,fiyatt,tarihh,toplamtutarr FROM malzemesatis";
            da.Fill(ds, "malzemesatis");
            dataGridView1.DataSource = ds.Tables["malzemesatis"];
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT kodu,adii,adett,fiyatt,tarihh,toplamtutarr FROM malzemesatis", baglan);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {



                DataRow satir;
                string bm;
                bm = "Data Source=CODER\\SQLEXPRESS;Initial Catalog=kayit;Integrated Security=True;";
                baglan.ConnectionString = bm;
                baglan = new SqlConnection(bm);
                baglan.Open();
                komut.CommandText = "select * from fiyat where Kodu= '" + textBox1.Text + "'";
                da.SelectCommand = komut;
                komut.Connection = baglan;
                da.Fill(ds, "fiyat");
                satir = ds.Tables["fiyat"].Rows[0];
                textBox2.Text = satir["adi"].ToString();
                textBox3.Text = satir["toplam"].ToString();
                baglan.Close();


            }
            catch { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox5.Text = Convert.ToString(Convert.ToDouble(textBox4.Text) * Convert.ToDouble(textBox3.Text));
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int toplam = 0, sayi = 0, eleman = 0;
            eleman = listBox1.Items.Count;
            while (sayi <= eleman - 1)
            {
                toplam += Int16.Parse(listBox1.Items[sayi].ToString());

                sayi++;
            }
            MessageBox.Show("Toplam Tutar: " + toplam + (" TL"));
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "update malzemesatis set adii='" + textBox2.Text + "', adett='" + textBox3.Text + "', fiyatt='" + textBox4.Text + "', tarihh='" + dateTimePicker1.Value + "', toplamtutarr='" + textBox5.Text + "' where kodu='" + textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show(textBox2.Text + " Malzemesi Malzeme Alışı Tablosunda Güncellendi");

            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "SELECT kodu,adii,adett,fiyatt,tarihh,toplamtutarr FROM malzemesatis";
            da.Fill(ds, "malzemesatis");
            dataGridView1.DataSource = ds.Tables["malzemesatis"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "delete from malzemesatis  where kodu='" + @textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MessageBox.Show("Silme İşleminiz Gerçekleştirilmiştir");
            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "SELECT kodu,adii,adett,fiyatt,tarihh,toplamtutarr FROM malzemesatis";
            da.Fill(ds, "malzemesatis");
            dataGridView1.DataSource = ds.Tables["malzemesatis"];

        }
    }
}
