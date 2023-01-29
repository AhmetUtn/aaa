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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace atm_otomasyonu
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public Form2 frm2;
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
                label2.Text = money.ToString();

            }
            dr.Close();
            cmd.Dispose();
            cnn.Close();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            bakiyesorgu();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex ==0)
            {
                //telefon
                label4.Text= "90";
            }

            if (comboBox1.SelectedIndex == 1)
            {
                //İnternet
                label4.Text = "180";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                //su
                label4.Text = "50";
            }

            if (comboBox1.SelectedIndex == 3)
            {
                //elektirik
                label4.Text = "200";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//geri
            this.Close();
            frm2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {// kapat
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {//öde
            
            cek = Convert.ToInt32(label4.Text);
            para = Convert.ToInt32(money);
            if (cek>para)
            {
                MessageBox.Show("Bakiyeniz yetersiz");
            }
            else
            {
                sonuc = para - cek;
                SqlCommand cmd = new SqlCommand("Update users Set money=@money Where user_id=@user_id", cnn);
                cmd.Parameters.AddWithValue("@user_id", frm2.id_mus);
                cmd.Parameters.AddWithValue("@money", sonuc);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Ödeme yapıldı");
                bakiyesorgu();
            }
            
        }
    }
}
