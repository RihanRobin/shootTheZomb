using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        Image Player;
        List<string> playerMovements = new List<string>();
        int steps = 0;
        int slowDownFramerate = 0;
        int playerHeight = 100;
        int playerWidth = 100;
        int playerX;
        int playerY;

        //Declare Lists
        List<PictureBox> zombieList = new List<PictureBox>();
        public FormGame()
        {
            InitializeComponent();
            SetUp();
        }

        private void MainTimerEvent(object sender, EventArgs e)
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
            if (down == true && player.Top + player.Height < this.ClientSize.Height)
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

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
        }
        private void SetUp()
        {

        }

        private void AnimatePlayer(int start, int end)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Player Movement

            if (e.KeyCode == Keys.D)
            {
                right = true;
                facing = "right";
                player.Image = Properties.Resources.SorceressRightWalk_0;

            }
            if (e.KeyCode == Keys.A)
            {
                left = true;
                facing = "left";
                player.Image = Properties.Resources.SorceressLeftWalk_0;
            }
            if (e.KeyCode == Keys.W)
            {
                up = true;
                facing = "up";
                player.Image = Properties.Resources.SorceressUpWalk_0;
            }
            if (e.KeyCode == Keys.S)
            {
                down = true;
                facing = "down";
                player.Image = Properties.Resources.SorceressDownWalk_0;
            }

            //Bullet Movement

            if (e.KeyCode == Keys.Right)
            {
                facing = "right";
                ShootBullet(facing);
            }
            if (e.KeyCode == Keys.Left)
            {
                facing = "left";
                ShootBullet(facing);
            }
            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                ShootBullet(facing);
            }
            if (e.KeyCode == Keys.Down)
            {
                facing = "down";
                ShootBullet(facing);
            }

            //Exit

            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
            if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            if (e.KeyCode == Keys.W)
            {
                up = false;
            }
            if (e.KeyCode == Keys.S)
            {
                down = false;
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + player.Width / 2;
            shootBullet.bulletTop = player.Top + player.Height / 2;
            shootBullet.MakeBullet(this);
        }
    }
}
