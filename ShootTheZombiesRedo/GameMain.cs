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
    public partial class GameMain : Form
    {
        // Images and movements for player and zombie animations
        Image playerImage;
        List<string> playerMovements = new List<string>();

        // Player and game state variables
        int frameIndex = 0;
        int slowDownFrameRate = 0;
        bool goLeft, goRight, goUp, goDown;
        bool inShop = false;
        bool isAmmoDropped = false;
        
        int playerX = 485;
        int playerY = 485;
        int playerHeight = 68;
        int playerWidth = 37;
        int playerSpeed = 10;
        int zombieSpeed = 2;
        Random rnd = new Random();
        string shootDirection;
        Rectangle playerBounds;  // Player collision bounds

        Dictionary<PictureBox, int> zombieFrameIndices = new Dictionary<PictureBox, int>();
        Dictionary<PictureBox, string> zombieDirections = new Dictionary<PictureBox, string>();

        public int score { get; set; }
        public int coin { get; set; }
        public int ammo { get; set; } = 5;
        public int playerHealth { get; set; } = 100;

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
                zombie.Top = rnd.Next(50, 450);

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
            labelScore.Text = "Score : " + score;
            labelCoin.Text = "Money : $" + coin;

            if (ammo == 0 && !isAmmoDropped)
            {
                DropAmmo();
                isAmmoDropped = true;
            }

            foreach (Control x in this.Controls)
            {
                // Check for ammo pickups
                if (x is PictureBox && x.Tag == "ammo" && ((PictureBox)x).Bounds.IntersectsWith(playerBounds))
                {
                    this.Controls.Remove((PictureBox)x);
                    ((PictureBox)x).Dispose();
                    ammo += 5;
                    isAmmoDropped = false;
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
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            score++;
                            coin+=3;
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
            playerBounds = new Rectangle(playerX, playerY, playerWidth, playerHeight);

            

            this.BackgroundImage = Image.FromFile("background.png");
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
            if (goLeft && playerX > 10)
            {
                playerX -= playerSpeed;
                AnimatePlayer(6, 11);
            }
            else if (goRight && playerX + playerWidth < this.ClientSize.Width - 10)
            {
                playerX += playerSpeed;
                AnimatePlayer(12, 17);
            }
            else if (goUp && playerY > 30)
            {
                playerY -= playerSpeed;
                AnimatePlayer(18, 23);
            }
            else if (goDown && playerY + playerHeight < this.ClientSize.Height - 10)
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

            if (!inShop && playerBounds.IntersectsWith(door.Bounds))
            {
                
                mainTimer.Stop();
                GameShop frm = new GameShop();
                frm.score = score;
                frm.playerHealth = playerHealth;
                frm.ammo = ammo;
                frm.coin = coin;
                frm.Show();
                this.Hide();
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
                EndScreen form2 = new EndScreen(score, coin);

                mainTimer.Stop();
                mainTimer.Dispose();
                
                form2.Show();
                this.Close();

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
            zombie.Image = Image.FromFile("walkdown (1).png");
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
            if (zombie1.Left < zombie2.Left) zombie1.Left -= 5;
            else zombie1.Left += 5;

            if (zombie1.Top < zombie2.Top) zombie1.Top -= 5;
            else zombie1.Top += 5;
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
            isAmmoDropped = true;
            PictureBox ammo = new PictureBox();
            ammo.Image = Image.FromFile("fireammo1.png");
            ammo.BackColor = Color.Transparent;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = rnd.Next(10, this.ClientSize.Width - ammo.Width - 100);
            ammo.Top = rnd.Next(50, this.ClientSize.Height - ammo.Height - 100);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);


        }
    }
}