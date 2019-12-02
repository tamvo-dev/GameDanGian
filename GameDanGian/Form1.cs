using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDanGian
{
    public partial class Form1 : Form
    {
        private GameManager game;
        
        public Form1()
        {
            InitializeComponent();
            game = 
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessengerForm messenger = new MessengerForm();
            messenger.sendMesseger(button.Text);
            messenger.Show();
        }

        private void right_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessengerForm messenger = new MessengerForm();
            messenger.sendMesseger(button.Text);
            messenger.Show();
        }

        private void left_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessengerForm messenger = new MessengerForm();
            messenger.sendMesseger(button.Text);
            messenger.Show();
        }

    }
}
