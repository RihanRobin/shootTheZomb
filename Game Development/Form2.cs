using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Development
{
    public partial class Form2 : Form
    {
        bool left, right, up, down;
        int playerSpeed = 5;
        bool inShop = true;
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (right == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }
            if (left == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (up == true && player.Top > 0)
            {
                player.Top -= playerSpeed;
            }
            if (down == true && player.Top + player.Width < this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }
            if (inShop == true && player.Bounds.IntersectsWith(shop.Bounds))
            {
                this.Hide();
                timer1.Stop();
                FormGame frm = new FormGame();
                frm.Show();
                inShop = false;
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                up = true;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                down = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                up = false;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                down = false;
            }
        }
    }
}
