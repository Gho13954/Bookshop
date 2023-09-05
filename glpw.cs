using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop
{
    public partial class glpw : Form
    {
        public glpw()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if(UPass.Text==Login.glpass)
            {
                Login.glpass = UNP.Text;
                MessageBox.Show("管理员密码修改成功");
                Books obj = new Books();
                obj.Show();
                this.Hide();
            }
            else
                MessageBox.Show("旧密码输入错误");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }
    }
}
