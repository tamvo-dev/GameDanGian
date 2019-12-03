using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class MessengerForm : Form
    {
        public MessengerForm()
        {
            InitializeComponent();
        }

        public void sendMesseger(String msg)
        {
            this.label1.Text = msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
