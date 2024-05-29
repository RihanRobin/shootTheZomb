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
    public partial class FormGame : Form
    {
        //Declare Bools
        bool left, right, up, down;
        bool gameOver = false;
        bool inShop = false;
        int playerHealth = 100;
        int playerSpeed = 5;
        int zombieSpeed = 3;
        string facing = "up";
        string shootDirection;
        //Declare Lists
        List<PictureBox> zombieList = new List<PictureBox>();
        public FormGame()
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
            if (inShop == false && player.Bounds.IntersectsWith(shop.Bounds))
            {
                this.Hide();
                timer1.Stop();
                Form2 frm = new Form2();
                frm.Show();
                inShop = true;
            }
            if (playerHealth > 0)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Player Movement

            if (e.KeyCode == Keys.D)
            {
                right = true;
                facing = "right";

            }
            if (e.KeyCode == Keys.A)
            {
                left = true;
                facing = "left";
            }
            if (e.KeyCode == Keys.W)
            {
                up = true;
                facing = "up";
            }
            if (e.KeyCode == Keys.S)
            {
                down = true;
                facing = "down";
            }

            //Bullet Movement

            if (e.KeyCode == Keys.Right)
            {
                facing = "right";
                shootDirection = "right";
            }
            if (e.KeyCode == Keys.Left)
            {
                facing = "left";
                shootDirection = "left";
            }
            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                shootDirection = "up";
            }
            if (e.KeyCode == Keys.Down)
            {
                facing = "down";
                shootDirection = "down";
            }

            //Exit

            if ( e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
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
