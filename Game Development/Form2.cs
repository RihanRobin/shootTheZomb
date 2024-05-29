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
            if (right == true)
            {
                playerShop.Left += playerSpeed;
            }
            if (left == true)
            {
                playerShop.Left -= playerSpeed;
            }
            if (up == true)
            {
                playerShop.Top -= playerSpeed;
            }
            if (down == true)
            {
                playerShop.Top += playerSpeed;
            }
            if (inShop == true && playerShop.Bounds.IntersectsWith(shop.Bounds))
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
