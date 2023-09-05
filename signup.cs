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

namespace Bookshop
{
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Save_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "")
            {
                MessageBox.Show("信息缺失，注册失败！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserDb1 values('" + UName.Text + "','" + UPhone.Text + "','" + UAdd.Text + "','" + UPass.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("注册成功！！！");
                    Login obj = new Login();
                    obj.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("注册失败\n错误日志："+ex.Message);
                }
                finally
                {
                    Con.Close();
                    
                }

            }
        }
    }
}
