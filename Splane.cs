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
    public partial class Splane : Form
    {
        public Splane()
        {
            InitializeComponent();
        }

        private void Splane_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int startpos = 0,r;
        Random rd = new Random();
        bool k = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(k)
            {
                if (MyprogressBar.Value >= 80)
                {
                    startpos += 1;
                    MyprogressBar.Value = startpos;
                    Pcbl.Text = startpos + "%";
                }
                else {
                    r = rd.Next(0, 11);
                    startpos += r;
                    MyprogressBar.Value = startpos;
                    Pcbl.Text = startpos + "%";
                }
                
            }
            
            
            if(MyprogressBar.Value== 100)
            {
                k= false; 
                MyprogressBar.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }
    }
}
