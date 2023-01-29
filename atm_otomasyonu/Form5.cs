using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atm_otomasyonu
{
    public partial class Form5 : Form
    {
        public Form2 frm2;
        public Form5()
        {
            InitializeComponent();
        }
        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\user_data.mdf;Integrated Security=True");
        string money; // Alacağımız verileri tanımladık
        int cek, sonuc, para;
        public void bakiyesorgu()
        {
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
        private void Form5_Load(object sender, EventArgs e)
        {
            bakiyesorgu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cek = Convert.ToInt32(textBox1.Text);
            para = Convert.ToInt32(money);
            sonuc = para - cek;
            SqlCommand cmd = new SqlCommand("Update users Set money=@money Where user_id=@user_id", cnn);
            cmd.Parameters.AddWithValue("@user_id", frm2.id_mus);
            cmd.Parameters.AddWithValue("@money", sonuc);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            bakiyesorgu();
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            frm2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
