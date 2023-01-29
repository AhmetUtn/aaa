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

namespace atm_otomasyonu
{
    public partial class Form1 : Form
    {
        public Form2 frm2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            frm2= new Form2();
            frm2.frm1 = this;
        }
        //
        public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\user_data.mdf;Integrated Security=True");
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        //
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int id, pwd;
        
        private void button1_Click(object sender, EventArgs e)
        {//giriş
            id= Convert.ToInt32(textBox1.Text);
            pwd = Convert.ToInt32(textBox2.Text);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM users where user_id='" + id + "' AND psw='" + pwd + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.");
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                textBox1.Focus();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //kapat 
            Application.Exit();
        }
    }
}
