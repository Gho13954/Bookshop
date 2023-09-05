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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()//数据库查询并存储到一张表中
        {
            Con.Open();
            string query = "select * from BookDb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void UpdateBook()
        {

            int newQty = stock - Convert.ToInt32(Nb.Text);
               try
                {
                    Con.Open();
                    
                    string query = "update BookDb1 set BQty='" + newQty + "' where BId='" + key + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                    Res();
                }

            }

        int n = 1,GrdToal=0;
        private void Buycar_Click(object sender, EventArgs e)
        {
            if (js)
            {
                MessageBox.Show("请你先结束现有订单再进行加入购物车活动");
            }
            else
            {
                int i;
                if(BTitle.Text=="")
                {
                    MessageBox.Show("请选择商品!!!");
                    return; 
                }
            


            
                if(!int.TryParse(Nb.Text,out i)||Convert.ToInt32(Nb.Text) < 1)
                {
                    MessageBox.Show("数量只能为大于等于1的整数!!!");
                }
                else if (Nb.Text == ""||Convert.ToInt32(Nb.Text) > stock)
                    MessageBox.Show("库存不足!!!");
                else 
                {
                    int total =Convert.ToInt32(Nb.Text)* Convert.ToInt32(Price.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(BillDGV);
                    newRow.Cells[0].Value = n;
                    newRow.Cells[1].Value =BTitle.Text;
                    newRow.Cells[2].Value=Price.Text;
                    newRow.Cells[3].Value = Nb.Text;
                    newRow.Cells[4].Value= total;
                    BillDGV.Rows.Add(newRow);
                    n++;
                    UpdateBook();
                    Nb.Text = "";
                    MessageBox.Show("购物车加入成功！！！");
                    GrdToal = GrdToal + total;
                    TotalLbl.Text = "订单总额" + GrdToal+"元";
                }
            }
        }

        int key = 0,stock=0;

        private void Res()
        {
            BTitle.Text = "";
            Qty.Text = "";
            Price.Text = "";
        }

        private void jia_Click(object sender, EventArgs e)
        {
            int i,k;
            if (!int.TryParse(Nb.Text, out i))
                Nb.Text = "1";
            else
            {
                k = Convert.ToInt32(Nb.Text) + 1;
                Nb.Text = k.ToString();
            }
        }

        private void jian_Click(object sender, EventArgs e)
        {
            int i;
            if(!int.TryParse(Nb.Text, out i) || Convert.ToInt32(Nb.Text)<=1)
            {
                MessageBox.Show("已经无法再减了！！！");
            }
            else
            {
                int k = Convert.ToInt32(Nb.Text) - 1;
                Nb.Text = k.ToString();
            }
            
        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (js)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else { MessageBox.Show("请先结算"); }
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserName.Text = Login.UserN;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Jsb_Click(object sender, EventArgs e)
        {
            
            if (BillDGV.Rows.Count==0||BillDGV.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("购物车空，请加入商品！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into DildDb1 (UName,Amount)values('" + UserName.Text + "','" +GrdToal + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    js = true;
                    wxbox.Visible = true;
                    MessageBox.Show("已支付" + GrdToal + "元\n请打印小票以结束订单");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    //populate();
                    //Res(); 
                }

            }
            
        }

        string prodname;
        bool js=false;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
                e.Graphics.DrawString("晓鱼书店", new Font("幼圆", 12, FontStyle.Bold), Brushes.Red, new Point(80));
                e.Graphics.DrawString("编号 产品 价格 数量 总计", new Font("幼圆", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
                foreach (DataGridViewRow row in BillDGV.Rows)
                {
                    prodid = Convert.ToInt32(row.Cells["Column7"].Value);
                    prodname = "" + row.Cells["Column8"].Value;
                    prodprice = Convert.ToInt32(row.Cells["Column9"].Value);
                    prodqty = Convert.ToInt32(row.Cells["Column10"].Value);
                    tottal = Convert.ToInt32(row.Cells["Column11"].Value);
                    e.Graphics.DrawString("" + prodid, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                    e.Graphics.DrawString("" + prodname, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                    e.Graphics.DrawString("" + prodprice, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                    e.Graphics.DrawString("" + prodqty, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                    e.Graphics.DrawString("" + tottal, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                    pos = pos + 20;

                }
                e.Graphics.DrawString("订单总额：" + GrdToal, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
                e.Graphics.DrawString("************晓鱼书店************", new Font("幼圆", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
                BillDGV.Rows.Clear();
                BillDGV.Refresh();
                TotalLbl.Text = "订单总额0元";
                pos = 100;
                GrdToal = 0;
                js = false;
                wxbox.Visible = false;
        }

        


        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitle.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            Qty.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Price.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            Nb.Text = "1";
            if (BTitle.Text == "")
                {
                    key = 0;
                    stock= 0;
                } 
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock= Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
                
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Res(); 
        }
    }
}
