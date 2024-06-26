using Game_Development;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace ShootTheZombies
{
    public partial class GameMain : Form
    {
        // Images and movements for player and zombie animations
        Image playerImage;
        List<string> playerMovements = new List<string>();
        List<string> zombieMovements = new List<string>();
        List<string> playerHurt = new List<string>();
        List<string> playerDeath = new List<string>();

        // Player and game state variables
        int frameIndex = 0;
        int slowDownFrameRate = 0;
        bool goLeft, goRight, goUp, goDown;
        bool gameOver = false;
        bool inShop = false;
        int playerHealth = 100;
        int playerX = 915;
        int playerY = 465;
        int playerHeight = 68;
        int playerWidth = 37;
        int playerSpeed = 8;
        int zombieSpeed = 2;
        int ammo = 10;
        int score = 0;
        Random rnd = new Random();
        string shootDirection;
        Rectangle playerBounds;  // Player collision bounds

        Dictionary<PictureBox, int> zombieFrameIndices = new Dictionary<PictureBox, int>();
        Dictionary<PictureBox, string> zombieDirections = new Dictionary<PictureBox, string>();

        public GameMain()
        {
            InitializeComponent();
            SetUp();
            for (int i = 0; i < 5; i++)
            {
                MakeZombies(); // Initialize with 10 zombies
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Movement keys
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.S) goDown = true;

            // Shooting keys
            if (e.KeyCode == Keys.Right && ammo > 0)
            {
                shootDirection = "right";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) DropAmmo();
            }
            if (e.KeyCode == Keys.Left && ammo > 0)
            {
                shootDirection = "left";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) DropAmmo();
            }
            if (e.KeyCode == Keys.Up && ammo > 0)
            {
                shootDirection = "up";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) DropAmmo();
            }
            if (e.KeyCode == Keys.Down && ammo > 0)
            {
                shootDirection = "down";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) DropAmmo();
            }

            if (e.KeyCode == Keys.Escape) Application.Exit(); // Exit game
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Stop movement when keys are released
            if (e.KeyCode == Keys.A) goLeft = false;
            if (e.KeyCode == Keys.D) goRight = false;
            if (e.KeyCode == Keys.W) goUp = false;
            if (e.KeyCode == Keys.S) goDown = false;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                shootDirection = "none";
            }
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(playerImage, playerX, playerY, playerWidth, playerHeight);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.BackColor = Color.Transparent;

            bool overlap;
            do
            {
                overlap = false;
                zombie.Left = rnd.Next(10, 800);
                zombie.Top = rnd.Next(50, 500);

                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "zombie" && zombie.Bounds.IntersectsWith(x.Bounds))
                    {
                        overlap = true;
                        break;
                    }
                }
            }

            while (overlap);

            zombieFrameIndices[zombie] = 0;
            zombieDirections[zombie] = "down";
            this.Controls.Add(zombie);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            UpdatePlayerMovement();
            CheckCollisionWithShop();
            UpdateHealthBar();
            this.Invalidate(); // Redraw the form

            foreach (Control x in this.Controls)
            {
                // Check for ammo pickups
                if (x is PictureBox && x.Tag == "ammo" && ((PictureBox)x).Bounds.IntersectsWith(playerBounds))
                {
                    this.Controls.Remove((PictureBox)x);
                    ((PictureBox)x).Dispose();
                    ammo += 5;
                }

                // Check for zombie interactions
                if (x is PictureBox && x.Tag == "zombie")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(playerBounds)) playerHealth -= 1;

                    // Move zombies towards the player
                    MoveZombie((PictureBox)x);

                    // Check for bullet collisions with zombies
                    foreach (Control j in this.Controls)
                    {
                        if (j is PictureBox && j.Tag == "bullet" && x is PictureBox && x.Tag == "zombie" && x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            zombieFrameIndices.Remove((PictureBox)x);
                            zombieDirections.Remove((PictureBox)x);
                            MakeZombies();
                        }
                    }
                }
            }
        }

        private void SetUp()
        {
            this.DoubleBuffered = true;
            playerMovements = Directory.GetFiles("walk", "*.png").ToList();
            playerImage = Image.FromFile(playerMovements[0]);
            playerHurt = Directory.GetFiles("hurt", "*.png").ToList();
            playerDeath = Directory.GetFiles("death", "*.png").ToList();
            zombieMovements = Directory.GetFiles("zwalk", "*.png").ToList();
            playerBounds = new Rectangle(playerX, playerY, playerWidth, playerHeight);
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate++;
            if (slowDownFrameRate == 4)
            {
                frameIndex++;
                slowDownFrameRate = 0;
            }

            if (frameIndex > end || frameIndex < start) frameIndex = start;
            playerImage = Image.FromFile(playerMovements[frameIndex]);
        }

        private void AnimateZombie(PictureBox zombie, int start, int end)
        {
            int frameIndex = zombieFrameIndices[zombie];
            slowDownFrameRate++;
            if (slowDownFrameRate == 4)
            {
                frameIndex++;
                slowDownFrameRate = 0;
            }

            if (frameIndex > end || frameIndex < start) frameIndex = start;
            zombie.Image = Image.FromFile(zombieMovements[frameIndex]);
            zombieFrameIndices[zombie] = frameIndex;
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
            else if (shootDirection == "left")
            {
                playerImage = Image.FromFile(playerMovements[6]);
            }
            else if (shootDirection == "right")
            {
                playerImage = Image.FromFile(playerMovements[12]);
            }
            else if (shootDirection == "up")
            {
                playerImage = Image.FromFile(playerMovements[18]);
            }
            else if (shootDirection == "down")
            {
                playerImage = Image.FromFile(playerMovements[0]);
            }
            else
            {
                AnimatePlayer(18, 0);
            }

            playerBounds.X = playerX;
            playerBounds.Y = playerY;
        }

        private void CheckCollisionWithShop()
        {
            // Assuming shopBounds is defined somewhere in your code as a Rectangle for the shop area
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
            if (playerHealth > 0)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                // mainTimer.Stop();
                // mainTimer.Dispose();
                gameOver = true;
            }
        }

        private void MoveZombie(PictureBox zombie)
        {
            string direction = zombieDirections[zombie];
            if (zombie.Left > playerX)
            {
                zombie.Left -= zombieSpeed;
                direction = "left";
            }
            if (zombie.Left < playerX)
            {
                zombie.Left += zombieSpeed;
                direction = "right";
            }
            if (zombie.Top > playerY)
            {
                zombie.Top -= zombieSpeed;
                direction = "up";
            }
            if (zombie.Top < playerY)
            {
                zombie.Top += zombieSpeed;
                direction = "down";
            }

            zombieDirections[zombie] = direction;
            AnimateZombie(zombie, 6, 12); // Assuming zombies have 6 frames per direction

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "zombie" && x != zombie && x.Bounds.IntersectsWith(zombie.Bounds))
                {
                    ResolveZombieOverlap((PictureBox)x, zombie);
                }
            }
        }


        private void ResolveZombieOverlap(PictureBox zombie1, PictureBox zombie2)
        {
            if (zombie1.Left < zombie2.Left) zombie1.Left -= 10;
            else zombie1.Left += 10;

            if (zombie1.Top < zombie2.Top) zombie1.Top -= 10;
            else zombie1.Top += 10;
        }

        private void ShootBullet(string direction)
        {
            Bullet shoot = new Bullet();
            shoot.direction = direction;
            shoot.bulletLeft = playerX + (playerWidth / 2);
            shoot.bulletTop = playerY + (playerHeight / 2);
            shoot.MakeBullet(this);
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Image.FromFile("fireammo2.png");
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = rnd.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = rnd.Next(50, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "zombie" && ammo.Bounds.IntersectsWith(x.Bounds))
                {
                    this.Controls.Remove(ammo);
                    ammo.Dispose();
                    DropAmmo();
                    break;
                }
            }
        }
    }
}
