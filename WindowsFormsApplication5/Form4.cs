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
    public partial class Form4 : Form
    {
        public Form4()
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

        private void button1_Click(object sender, EventArgs e)
        {

           
            int x=1;

    baglan.Open();
       
    SqlCommand sorgu = new SqlCommand();
    sorgu.Connection = baglan;
    sorgu.CommandText = "insert into malzemealis (kodu,adi,adet,fiyat,tarih,toplamtutar) values ('" + @textBox1.Text + "','" + @textBox2.Text + "', '" + @textBox4.Text + "', '" + @textBox3.Text + "', '" + @dateTimePicker1.Value.ToString("MM.dd.yyyy hh:mm:ss") + "', '" + @textBox5.Text + "')";

    x=sorgu.ExecuteNonQuery();
    if (x >= 1)
    {
        listBox1.Items.Add(textBox5.Text);
        textBox1.Clear();
        textBox2.Clear();
        textBox3.Clear();
        textBox4.Clear();
        textBox5.Clear();
    }
    else
        MessageBox.Show("Bir hata meydana geldi !!! ");
            
    baglan.Close();
    MessageBox.Show("Alış tamamlandı.");
    komut.CommandText = "SELECT kodu,adi,adet,fiyat,tarih,toplamtutar FROM malzemealis";
    da.Fill(ds, "malzemealis");
    dataGridView1.DataSource = ds.Tables["malzemealis"];
    dataGridView1.Columns["kodu"].HeaderText = "Kodu";
    dataGridView1.Columns["adi"].HeaderText = "Adı";
    dataGridView1.Columns["adet"].HeaderText = "Adet";
    dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
    dataGridView1.Columns["tarih"].HeaderText = "Tarih";
    dataGridView1.Columns["toplamtutar"].HeaderText = "Toplam Tutar"; 
          
   
    

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT kodu,adi,adet,fiyat,tarih,toplamtutar FROM malzemealis", baglan);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["adet"].HeaderText = "Adet";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["toplamtutar"].HeaderText = "Toplam Tutar"; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "delete from malzemealis  where kodu='" + @textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MessageBox.Show("Silme İşleminiz Gerçekleştirilmiştir");
            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "SELECT kodu,adi,adet,fiyat,tarih,toplamtutar FROM malzemealis";
            da.Fill(ds, "malzemealis");
            dataGridView1.DataSource = ds.Tables["malzemealis"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["adet"].HeaderText = "Adet";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["toplamtutar"].HeaderText = "Toplam Tutar"; 


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
                textBox3.Text = satir["fiyat"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "update malzemealis set adi='" + textBox2.Text + "', adet='" + textBox3.Text + "', fiyat='" + textBox4.Text + "', tarih='" + dateTimePicker1.Value + "', toplamtutar='" + textBox5.Text + "' where kodu='" + textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show(textBox2.Text + " Malzemesi Malzeme Alışı Tablosunda Güncellendi");

            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "SELECT kodu,adi,adet,fiyat,tarih,toplamtutar FROM malzemealis";
            da.Fill(ds, "malzemealis");
            dataGridView1.DataSource = ds.Tables["malzemealis"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
            dataGridView1.Columns["adet"].HeaderText = "Adet";
            dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["toplamtutar"].HeaderText = "Toplam Tutar"; 

        }

    }
}
