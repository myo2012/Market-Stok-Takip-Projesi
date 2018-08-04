using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection("Data Source=CODER\\SQLEXPRESS;Initial Catalog=kayit;Integrated Security=True;");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand komut = new SqlCommand();

        private void button1_Click(object sender, EventArgs e)
        {
           

            SqlDataAdapter da = new SqlDataAdapter("SELECT kodu,adii,adett,fiyatt,tarihh,toplamtutarr FROM malzemesatis WHERE tarihh > '" + @dateTimePicker3.Value.ToString("MM.dd.yyyy hh:mm:ss") + "' AND tarihh <'" + @dateTimePicker4.Value.ToString("MM.dd.yyyy hh:mm:ss") + "'", baglan);
            ds.Clear();
            da.Fill(ds, "malzemesatis");
            dataGridView1.DataSource = ds.Tables["malzemesatis"];
            dataGridView1.Columns["kodu"].HeaderText = "Kodu";
            dataGridView1.Columns["adii"].HeaderText = "Adı";
            dataGridView1.Columns["adett"].HeaderText = "Adet";
            dataGridView1.Columns["fiyatt"].HeaderText = "Fiyat";
            dataGridView1.Columns["tarihh"].HeaderText = "Tarih";
            dataGridView1.Columns["toplamtutarr"].HeaderText = "Toplam Tutar"; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
          

            SqlDataAdapter da = new SqlDataAdapter("SELECT kodu,adi,adet,fiyat,tarih,toplamtutar FROM malzemealis WHERE Tarih > '" + @dateTimePicker1.Value.ToString("MM.dd.yyyy hh:mm:ss") + "' AND Tarih <'" + @dateTimePicker2.Value.ToString("MM.dd.yyyy hh:mm:ss") + "'", baglan);
            ds.Clear();
            da.Fill(ds, "malzemealis");
            dataGridView2.DataSource = ds.Tables["malzemealis"];
            dataGridView2.Columns["kodu"].HeaderText = "Kodu";
            dataGridView2.Columns["adi"].HeaderText = "Adı";
            dataGridView2.Columns["adet"].HeaderText = "Adet";
            dataGridView2.Columns["fiyat"].HeaderText = "Fiyat";
            dataGridView2.Columns["tarih"].HeaderText = "Tarih";
            dataGridView2.Columns["toplamtutar"].HeaderText = "Toplam Tutar";

         

       
          


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select kodu,adi,adet,fiyat,tarih,toplamtutar from malzemealis WHERE Tarih > '" + @dateTimePicker1.Value.ToString("MM.dd.yyyy hh:mm:ss") + "' AND Tarih <'" + @dateTimePicker2.Value.ToString("MM.dd.yyyy hh:mm:ss") + "'", baglan);
            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                listBox1.Items.Add("KODU---ADI---ADET---FİYAT---TARİH-------TOPLAMTUTAR");
                listBox1.Items.Add(dr["kodu"].ToString() +"       "+ (dr["adi"].ToString()) +"      "+ (dr["adet"].ToString()) +"       "+ (dr["fiyat"].ToString()) +"       "+ (dr["tarih"].ToString()) +"     "+ (dr["toplamtutar"].ToString()));
                
            }

            baglan.Close();

            StreamWriter yazi = new StreamWriter("C:\\deneme.txt");
            yazi.Write(listBox1.Items.ToString());
            yazi.Close();
        
        }

    


       
        

        

        

      

        

      
    }
}
