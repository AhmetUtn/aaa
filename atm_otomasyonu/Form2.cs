using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atm_otomasyonu
{
    public partial class Form2 : Form
    {

        public Form1 frm1;
        Form3 frm3= new Form3();
        Form4 frm4= new Form4();
        Form5 frm5= new Form5();
        Form6 frm6= new Form6();
        public Form2()
        {
            InitializeComponent();
            frm3= new Form3();
            frm3.frm2 = this;
            frm4= new Form4();
            frm4.frm2 = this;
            frm5=new Form5();
            frm5.frm2 = this;
            frm6= new Form6();
            frm6.frm2= this;

        }
        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\user_data.mdf;Integrated Security=True");
        public int id_mus;
        private void Form2_Load(object sender, EventArgs e)
        {
            id_mus = frm1.id;
            string user_name; // Alacağımız verileri tanımladık
            cnn.Open(); // Bağlantıyı açtık
            SqlCommand cmd = new SqlCommand("Select * from users where user_id=@deger ", cnn);
            cmd.Parameters.AddWithValue("@deger",id_mus); //Girdiğimiz şifreye uygun kişiyi istedik
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // Eğer veritabanında girdiğimiz şifre var ise bilgilerini değişkenlere atadık..
                user_name = dr["user_name"].ToString();
                label3.Text=user_name.ToString();
                    
            }
            dr.Close();
            cmd.Dispose();
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//bakiye sorgu
           frm3.Show();
           this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {//para yatırma
            frm4.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {// para çekme
            frm5.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {//ödeme
            frm6.Show();
            this.Hide();    
        }
    }
}
