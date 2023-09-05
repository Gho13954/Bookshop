using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public static string UserN = "";

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda=new SqlDataAdapter("select count(*) from UserDb1 where UName='"+UName.Text+"'and UPassword='"+UPass.Text+"'",Con);
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                UserN =UName.Text;
                Billing obj=new Billing();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！！！");
            }
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        bool gl=true;
        private void label5_Click(object sender, EventArgs e)
        {
            if(gl)
            {
                GPass.Visible = true;
                GLogin.Visible = true;
                label5.Text = "收起";
                gl= false;
            }
            else
            {
                GPass.Visible = false;
                GLogin.Visible = false;
                label5.Text = "管理员登录";
                gl = true;
            }    
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }
        public static string glpass="1234";
        private void GLogin_Click(object sender, EventArgs e)
        {
            if (GPass.Text == glpass)
            {
                Books obj = new Books();
                obj.Show();
                this.Hide(); 
            }
            else
               MessageBox.Show("管理员密码错误!!!");
        }

        private void su_Click(object sender, EventArgs e)
        {
            signup obj = new signup();
            obj.Show();
            this.Hide();
        }
    }
}
