﻿using Game_Development;
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
        int ammo = 5;
        Random rnd = new Random();
        string shootDirection;
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

            if (e.KeyCode == Keys.Right && ammo > 0)
            {
                shootDirection = "right";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) // if ammo is less than 1
                {
                    DropAmmo(); // invoke the drop ammo function
                }
            }
            if (e.KeyCode == Keys.Left && ammo > 0)
            {
                shootDirection = "left";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) // if ammo is less than 1
                {
                    DropAmmo(); // invoke the drop ammo function
                }
            }
            if (e.KeyCode == Keys.Up && ammo > 0)
            {
                shootDirection = "up";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) // if ammo is less than 1
                {
                    DropAmmo(); // invoke the drop ammo function
                }
            }
            if (e.KeyCode == Keys.Down && ammo > 0)
            {
                shootDirection = "down";
                ammo--;
                ShootBullet(shootDirection);
                if (ammo < 1) // if ammo is less than 1
                {
                    DropAmmo(); // invoke the drop ammo function
                }
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
            slowDownFrameRate++;
            if (slowDownFrameRate == 4)
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
                shootDirection = "none";
                AnimatePlayer(6, 11);
            }
            else if (goRight && playerX + playerWidth < this.ClientSize.Width)
            {
                playerX += playerSpeed;
                shootDirection = "none";
                AnimatePlayer(12, 17);
            }
            else if (goUp && playerY > 0)
            {
                playerY -= playerSpeed;
                shootDirection = "none";
                AnimatePlayer(18, 23);
            }
            else if (goDown && playerY + playerHeight < this.ClientSize.Height)
            {
                playerY += playerSpeed;
                shootDirection = "none";
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
        }
        private void DropAmmo()
        {
            // this function will make a ammo image for this game
            PictureBox ammo = new PictureBox(); // create a new instance of the picture box
            ammo.Image = Image.FromFile("fireball.png"); // assignment the ammo image to the picture box
            ammo.SizeMode = PictureBoxSizeMode.AutoSize; // set the size to auto size
            ammo.Left = rnd.Next(10, 890); // set the location to a random left
            ammo.Top = rnd.Next(50, 600); // set the location to a random top
            ammo.Tag = "ammo"; // set the tag to ammo
            this.Controls.Add(ammo); // add the ammo picture box to the screen
            ammo.BringToFront(); // bring it to front
        }
    }
}
