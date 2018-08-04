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
    public partial class Form2 : Form
    {
        public Form2()
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
            sorgu.CommandText = "insert into malzeme (kodu,adi) values ('" + @textBox1.Text + "','" + @textBox2.Text + "')";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Malzeme Eklenmiştir.");
            else
                MessageBox.Show("Hata! Tekrar Deneyiniz.");
            textBox1.Clear();
            textBox2.Clear();
           
            baglan.Close();

            komut.CommandText = "select * from malzeme";
            da.Fill(ds, "malzeme");
            dataGridView1.DataSource = ds.Tables["malzeme"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";
           
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "delete from malzeme where kodu='" + @textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MessageBox.Show("Silme İşleminiz Gerçekleştirilmiştir");
            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "select * from malzeme";
            da.Fill(ds, "malzeme");
            dataGridView1.DataSource = ds.Tables["malzeme"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglan;
            sorgu.CommandText = "update malzeme set adi='" + @textBox2.Text + "' where kodu='" + @textBox1.Text + "'";
            if (sorgu.ExecuteNonQuery() == 1)
                MessageBox.Show(textBox2.Text + " Malzemesi Güncellendi");

            textBox1.Clear();
            textBox2.Clear();
            baglan.Close();
            komut.CommandText = "select * from malzeme";
            da.Fill(ds, "malzeme");
            dataGridView1.DataSource = ds.Tables["malzeme"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT kodu,adi FROM malzeme", baglan);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adi"].HeaderText = "Adı";

        }
    }
}
