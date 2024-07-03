using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootTheZombiesRedo
{
    public partial class GameShop : Form
    {

        public int score { get; set; }
        public int playerHealth { get; set; } = 100;

        public int coin { get; set; }

        public int ammo { get; set; }
        public GameShop()
        {
            InitializeComponent();
            SetUp();
        }

        Image playerImage;
        List<string> playerMovements = new List<string>();
        int frameIndex = 0;
        int slowDownFrameRate = 0;
        bool goLeft, goRight, goUp, goDown;
        bool inShop = true;
        int playerX = 480;
        int playerY = 50;
        int playerHeight = 68;
        int playerWidth = 37;
        int playerSpeed = 8;
        string facing;
        Rectangle playerBounds;  // Player collision bounds
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.S) goDown = true;

            if (e.KeyCode == Keys.Right) facing = "right";
            if (e.KeyCode == Keys.Left) facing = "left";
            if (e.KeyCode == Keys.Up) facing = "up";
            if (e.KeyCode == Keys.Down) facing = "down";

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
            CheckCollisionWithDoor();
            this.Invalidate();
        }

        private void SetUp()
        {
            this.DoubleBuffered = true;
            playerMovements = Directory.GetFiles("walk", "*.png").ToList();
            playerImage = Image.FromFile(playerMovements[0]);
            playerBounds = new Rectangle(playerX, playerY, playerWidth, playerHeight);

            this.BackgroundImage = Image.FromFile("shop.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate++;
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
                facing = "left";
                AnimatePlayer(6, 11);
            }
            else if (goRight && playerX + playerWidth < this.ClientSize.Width)
            {
                playerX += playerSpeed;
                facing = "right";
                AnimatePlayer(12, 17);
            }
            else if (goUp && playerY > 0)
            {
                playerY -= playerSpeed;
                facing = "up";
                AnimatePlayer(18, 23);
            }
            else if (goDown && playerY + playerHeight < this.ClientSize.Height)
            {
                playerY += playerSpeed;
                facing = "down";
                AnimatePlayer(0, 5);
            }
            else
            {
                AnimatePlayer(0, 0);
            }

            playerBounds.X = playerX;
            playerBounds.Y = playerY;
        }

        private void CheckCollisionWithDoor()
        {
            // Assuming shopBounds is defined somewhere in your code as a Rectangle for the shop area
            if (inShop && playerBounds.IntersectsWith(door.Bounds))
            {
                this.Hide();
                shopTimer.Stop();
                GameMain frm = new GameMain();
                frm.score = score;
                frm.playerHealth = playerHealth;
                frm.ammo = ammo;
                frm.coin = coin;
                frm.Show();
                inShop = true;
            }
        }
    }
}
