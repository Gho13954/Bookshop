using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            populate();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()//数据库查询并存储到一张表中
        {
            Con.Open();
            string query = "select * from UserDb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "")
            {
                MessageBox.Show("信息缺失，无法保存！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserDb1 values('" + UName.Text + "','" + UPhone.Text + "','" + UAdd.Text + "','" + UPass.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息保存成功！！！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户信息保存失败\n错误日志:" + ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                    Res();
                }

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {

        }
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UName.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhone.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            UAdd.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            UPass.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UName.Text == "")
                key = 0;
            else
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void Pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Dash obj = new Dash();
            obj.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Res()
        {
            UName.Text = "";
            UPhone.Text = "";
            UAdd.Text = "";
            UPass.Text = "";
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Res();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "")
            {
                MessageBox.Show("信息缺失，无法修改！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    //string query = "update BookDb1 set BTitle=" + BTitle.Text + ",BAuthor=" + BAut.Text + ",BCat=" + BCat.SelectedItem.ToString() + ",BQty=" + Qty.Text + ",BPrice=" + Price.Text + "where BId=" + key + "";
                    string query = "update UserDb1 set UName='" + UName.Text + "',UPhone='" + UPhone.Text + "',UAdd='" + UAdd.Text + "',UPassword='" + UPass.Text + "' where Uid='" + key + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息修改成功！！！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户信息修改失败\n错误日志:" + ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                    Res();
                }

            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("信息缺失,无法删除！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from UserDb1 where UId=" + key + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息删除成功！！！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户信息删除失败\n错误日志:" + ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                    Res();
                    key = 0;
                }

            }
        }

        private void glpassw_Click(object sender, EventArgs e)
        {
            glpw obj = new glpw();
            obj.Show();
            this.Hide();
        }
    }
}
