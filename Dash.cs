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
    public partial class Dash : Form
    {
        public Dash()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wu.sir\Documents\WhiteBookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

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

        private void label7_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            User obj = new User();
            obj.Show();
            this.Hide();
        }

        private void fflash()
        {
            Con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(BQty) from BookDb1",Con);
            DataTable dt1=new DataTable();
            sda1.Fill(dt1);
            BookSlb.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select sum(Amount) from DildDb1", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            AmountSlb.Text = dt2.Rows[0][0].ToString();

            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from UserDb1", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            UserSlb.Text = dt3.Rows[0][0].ToString();
            Con.Close();
        }

        private void Dash_Load(object sender, EventArgs e)
        {
            fflash();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            fflash();
        }

        private void glpassw_Click(object sender, EventArgs e)
        {
            glpw obj = new glpw();
            obj.Show();
            this.Hide();
        }
    }
}
