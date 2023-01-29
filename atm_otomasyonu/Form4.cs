using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atm_otomasyonu
{
    public partial class Form4 : Form
    {
        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\user_data.mdf;Integrated Security=True");

        public Form4()
        {
            InitializeComponent();
        }
        public Form2 frm2;
        public Form3 frm3;

        string money; // Alacağımız verileri tanımladık
        int yatır,sonuc,para;

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

        private void button1_Click(object sender, EventArgs e)
        {
            yatır = Convert.ToInt32(textBox1.Text);
            para = Convert.ToInt32(money);
            sonuc = para + yatır;
            SqlCommand cmd = new SqlCommand("Update users Set money=@money Where user_id=@user_id", cnn);
            cmd.Parameters.AddWithValue("@user_id", frm2.id_mus);
            cmd.Parameters.AddWithValue("@money", sonuc);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            MessageBox.Show("Paranız yatırılıdı");
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

        private void Form4_Load(object sender, EventArgs e)
        {
            
            bakiyesorgu();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
            

        }
    }
}
