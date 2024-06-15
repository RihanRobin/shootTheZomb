using Game_Development;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShootTheZombies
{
    public partial class GameMain : Form
    {
        Image playerImage;
        List<string> playerMovements = new List<string>();
        List<string> playerAttack = new List<string>();
        List<string> playerHurt = new List<string>();
        List<string> playerDeath = new List<string>();
        int frameIndex = 0;
        int slowDownFrameRate = 0;
        bool goLeft, goRight, goUp, goDown;
        bool gameOver = false;
        bool inShop = false;
        int playerHealth = 100;
        int playerX = 875;
        int playerY = 430;
        int playerHeight = 68;
        int playerWidth = 37;
        int playerSpeed = 8;
        string facing;
        Rectangle playerBounds;  // Player collision bounds

        public GameMain()
        {
            InitializeComponent();
            SetUp();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.S) goDown = true;

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

            if (e.KeyCode == Keys.Escape) Application.Exit();
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = false;
            if (e.KeyCode == Keys.D) goRight = false;
            if (e.KeyCode == Keys.W) goUp = false;
            if (e.KeyCode == Keys.S) goDown = false;
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(playerImage, playerX, playerY, playerWidth, playerHeight);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            UpdatePlayerMovement();
            CheckCollisionWithShop();
            UpdateHealthBar();
            this.Invalidate();
            
        }

        private void SetUp()
        {
            this.DoubleBuffered = true;
            playerMovements = Directory.GetFiles("walk", "*.png").ToList();
            playerImage = Image.FromFile(playerMovements[0]);
            playerAttack = Directory.GetFiles("attack", "*.png").ToList();
            playerHurt = Directory.GetFiles("hurt", "*.png").ToList();
            playerDeath = Directory.GetFiles("death", "*.png").ToList();
            playerBounds = new Rectangle(playerX, playerY, playerWidth, playerHeight);
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate ++;
            if (slowDownFrameRate == 3)
            {
                frameIndex++;
                slowDownFrameRate = 0;
            }

            if (frameIndex > end || frameIndex < start) frameIndex = start;
            playerImage = Image.FromFile(playerMovements[frameIndex]);
        }

        private void UpdatePlayerMovement()
        {
            if (goLeft && playerX > 0)
            {
                playerX -= playerSpeed;
                AnimatePlayer(6, 11);
            }
            else if (goRight && playerX + playerWidth < this.ClientSize.Width)
            {
                playerX += playerSpeed;
                AnimatePlayer(12, 17);
            }
            else if (goUp && playerY > 0)
            {
                playerY -= playerSpeed;
                AnimatePlayer(18, 23);
            }
            else if (goDown && playerY + playerHeight < this.ClientSize.Height)
            {
                playerY += playerSpeed;
                AnimatePlayer(0, 5);
            }
            else if(facing == "left")
            {
                playerImage = Image.FromFile(playerMovements[6]);
            }
            else if (facing == "right")
            {
                playerImage = Image.FromFile(playerMovements[12]);
            }
            else if (facing == "up")
            {
                playerImage = Image.FromFile(playerMovements[18]);
            }
            else if (facing == "down")
            {
                playerImage = Image.FromFile(playerMovements[0]);
            }

            else
            {
                AnimatePlayer(0, 0);
            }

            playerBounds.X = playerX;
            playerBounds.Y = playerY;
        }

        private void CheckCollisionWithShop()
        {
            // Assuming `shopBounds` is defined somewhere in your code as a Rectangle for the shop area
            if (!inShop && playerBounds.IntersectsWith(door.Bounds))
            {
                this.Hide();
                mainTimer.Stop();
                GameShop frm = new GameShop();
                frm.Show();
                inShop = true;
            }
        }

        private void UpdateHealthBar()
        {
            if (playerHealth > 0) healthBar.Value = playerHealth;
            else
            {
                gameOver = true;
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = playerX + playerWidth / 2;
            shootBullet.bulletTop = playerY + playerWidth / 2;
            shootBullet.MakeBullet(this);

            if (direction == "left")
            {
                playerImage = Image.FromFile(playerMovements[9]);
            }
            else
            {
                AnimatePlayer(4,4);
            }
        }
    }
}
