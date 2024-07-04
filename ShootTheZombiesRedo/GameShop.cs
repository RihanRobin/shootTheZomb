using Microsoft.VisualBasic.FileIO;
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

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wound Management.csv");

        //Fact List
        List<string> FirstAidFacts = new List<string>();
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
        public GameShop()
        {
            InitializeComponent();
            FirstAidFacts = ReadFactsFromCSV("Wound Management.csv");
            SetUp();
        }

        

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.S) goDown = true;

            if (e.KeyCode == Keys.M)
            {
                if (StartMenu.mute == false)
                {
                    StartMenu.mute = true;
                    StartMenu.player.Stop();
                }
                else
                {
                    StartMenu.mute = false;
                    StartMenu.player.Play();
                }
            }

            if (e.KeyCode == Keys.Right) facing = "right";
            if (e.KeyCode == Keys.Left) facing = "left";
            if (e.KeyCode == Keys.Up) facing = "up";
            if (e.KeyCode == Keys.Down) facing = "down";

            if (e.KeyCode == Keys.Escape) Application.Exit();
        }

        private List<string> ReadFactsFromCSV(string fileName)
        {
            List<string> facts = new List<string>();

            // Construct the full file path
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            // Check if the file exists
            if (File.Exists(filePath))
            {
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields != null && fields.Length > 0)
                        {
                            facts.Add(fields[0]); // Assuming first column contains facts
                        }
                    }
                }
            }

            return facts;
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

            label1.Text = "Health : " + playerHealth + "%";
            label2.Text = "Money : $" + coin;
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

            pictureBox3.Click += new EventHandler(PictureBox3_Click);
            pictureBox2.Click += new EventHandler(PictureBox2_Click);
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
            if (goLeft && playerX > 10 && !IsCollidingWithPictureBoxes("left"))
            {
                playerX -= playerSpeed;
                facing = "left";
                AnimatePlayer(6, 11);
            }
            else if (goRight && playerX + playerWidth < this.ClientSize.Width - 10 && !IsCollidingWithPictureBoxes("right"))
            {
                playerX += playerSpeed;
                facing = "right";
                AnimatePlayer(12, 17);
            }
            else if (goUp && playerY > 0 && !IsCollidingWithPictureBoxes("up"))
            {
                playerY -= playerSpeed;
                facing = "up";
                AnimatePlayer(18, 23);
            }
            else if (goDown && playerY + playerHeight < this.ClientSize.Height - 10 && !IsCollidingWithPictureBoxes("down"))
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

        private bool IsCollidingWithPictureBoxes(string direction)
        {
            Rectangle futureBounds = playerBounds;

            switch (direction)
            {
                case "left":
                    futureBounds.X -= playerSpeed;
                    break;
                case "right":
                    futureBounds.X += playerSpeed;
                    break;
                case "up":
                    futureBounds.Y -= playerSpeed;
                    break;
                case "down":
                    futureBounds.Y += playerSpeed;
                    break;
            }

            return futureBounds.IntersectsWith(pictureBox2.Bounds) || futureBounds.IntersectsWith(pictureBox3.Bounds);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            if (FirstAidFacts.Count > 0)
            {
                // Generate a random index
                Random random = new Random();
                int index = random.Next(FirstAidFacts.Count);

                // Set label3 text to a random fact
                label3.Text = FirstAidFacts[index];
            }

            if (playerHealth < 100)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to increase your health by 50 for $60 ?", "Increase Health", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    if (coin >= 60)
                    {
                        playerHealth = Math.Min(playerHealth + 50, 100); // Ensure health does not exceed 100
                        coin -= 60;
                        MessageBox.Show("Health increased by 50!");
                    }
                    else
                    {
                        MessageBox.Show("Not enough cash!");
                    }
                }
            }
            
            else
            {
                MessageBox.Show("Your health is already full.");
            }
            
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (FirstAidFacts.Count > 0)
            {
                // Generate a random index
                Random random = new Random();
                int index = random.Next(FirstAidFacts.Count);

                // Set label3 text to a random fact
                label3.Text = FirstAidFacts[index];
            }
            if (playerHealth < 100) 
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to heal fully for $100 ?", "Increase Health", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (coin >= 100)
                    {
                        playerHealth = 100; // Set health to max
                        coin -= 100;
                        MessageBox.Show("Health set to the MAX");
                    }
                    else
                    {
                        MessageBox.Show("Not enough cash!");
                    }
                }
            }
            
            else
            {
                MessageBox.Show("Your health is already full.");
            }
        }

        private void CheckCollisionWithDoor()
        {
            // Assuming shopBounds is defined somewhere in your code as a Rectangle for the shop area
            if (inShop && playerBounds.IntersectsWith(door.Bounds))
            {
                
                shopTimer.Stop();
                GameMain frm = new GameMain();
                frm.score = score;
                frm.playerHealth = playerHealth;
                frm.ammo = ammo;
                frm.coin = coin;
                frm.Show();
                this.Hide();
                inShop = true;
            }
        }
    }
}
