using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class logic : Form
    {
        public string log;
        public string pass;
        public logic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength==0||textBox2.TextLength==0)
            {
                return;
            }
            log=textBox1.Text;
            pass=textBox2.Text;
            this.Close();
        }
    }
}
