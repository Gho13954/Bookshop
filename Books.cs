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

namespace Bookshop
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }
        
        private void populate()//数据库查询并存储到一张表中
        {
            Con.Open();
            string query = "select * from BookDb1";
            SqlDataAdapter sda=new SqlDataAdapter(query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds =new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Filter()//数据库查询过滤并存储到一张表中
        {
            Con.Open();
            string query = "select * from BookDb1 where BCat ='"+Catcbs.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Save_Click(object sender, EventArgs e)
        {
            if (BTitle.Text == "" || BAut.Text == "" || Qty.Text == "" || Price.Text == "" || BCat.SelectedIndex == -1)
            {
                MessageBox.Show("信息缺失，无法保存！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BookDb1 values('"+BTitle.Text+"','" +BAut.Text+ "','" +BCat.SelectedItem.ToString()+"','"+Qty.Text+"','"+Price.Text+"')";
                    SqlCommand cmd=new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息保存成功！！！");
                }catch(Exception ex)
                {
                    MessageBox.Show("书籍信息保存失败\n错误日志:"+ex.Message);
                }
                finally { Con.Close();
                           populate();
                            Res();
                }

            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (BTitle.Text == "" || BAut.Text == "" || Qty.Text == "" || Price.Text == "" || BCat.SelectedIndex == -1)
            {
                MessageBox.Show("信息缺失，无法修改！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    //string query = "update BookDb1 set BTitle=" + BTitle.Text + ",BAuthor=" + BAut.Text + ",BCat=" + BCat.SelectedItem.ToString() + ",BQty=" + Qty.Text + ",BPrice=" + Price.Text + "where BId=" + key + "";
                    string query = "update BookDb1 set BTitle='" + BTitle.Text + "',BAuthor='" + BAut.Text + "',BCat='" + BCat.SelectedItem.ToString() + "',BQty='" + Qty.Text + "',BPrice='" + Price.Text + "'where BId="+key+"'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息修改成功！！！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("书籍信息修改失败\n错误日志:" + ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                    Res();
                }

            }
        }

        private void Catcbs_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(Catcbs.SelectedItem.ToString()=="全部分类")
                populate();
            else
                Filter();
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Catcbs.SelectedIndex = 0;
            populate();
        }

        private void Res()
        {
            BTitle.Text = "";
            BAut.Text = "";
            Qty.Text = "";
            Price.Text = "";
            BCat.SelectedIndex = -1;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Res();
        }

        int key = 0;

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitle.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAut.Text= BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCat.SelectedItem= BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            Qty.Text= BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Price.Text= BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitle.Text == "")
                key = 0;
            else
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("信息缺失,无法删除！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BookDb1 where BId="+key+"";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息删除成功！！！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("书籍信息删除失败\n错误日志:" + ex.Message);
                }
                finally { Con.Close();
                    populate();
                    Res();
                    key = 0;
                }

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            User obj = new User();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Dash obj = new Dash();
            obj.Show();
            this.Hide();
        }

        private void glpassw_Click(object sender, EventArgs e)
        {
            glpw obj = new glpw();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
