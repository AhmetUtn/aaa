using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atm_otomasyonu
{
    public partial class Form3 : Form
    {
        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\user_data.mdf;Integrated Security=True");

        public Form3()
        {
            InitializeComponent();
            frm4=new Form4();
            frm4.frm3 = this;
        }
        public Form2 frm2;
        Form4 frm4= new Form4();
        private void Form3_Load(object sender, EventArgs e)
        {// yükleme 

            string money; // Alacağımız verileri tanımladık
            cnn.Open(); // Bağlantıyı açtık
            SqlCommand cmd = new SqlCommand("Select * from users where user_id=@deger ", cnn);
            cmd.Parameters.AddWithValue("@deger", frm2.id_mus); //Girdiğimiz şifreye uygun kişiyi istedik
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // Eğer veritabanında girdiğimiz şifre var ise bilgilerini değişkenlere atadık..
                money = dr["money"].ToString();
                label3.Text = money.ToString();

            }
            dr.Close();
            cmd.Dispose();
            cnn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //geri
            this.Hide();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
