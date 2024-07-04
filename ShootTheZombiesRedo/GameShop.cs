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


        //Fact List
        List<string> FirstAidFacts = new List<string>
            {
                "First Aid Fact : First aid is the immediate care given to someone who is injured or suddenly becomes ill.",
                "First Aid Fact : It's crucial to stay calm and assess the situation before taking action in a first aid scenario.",
                "First Aid Fact : Knowing how to perform CPR (cardiopulmonary resuscitation) can save someone's life if their heart stops beating.",
                "First Aid Fact : Applying pressure to a bleeding wound can help stop or slow down bleeding until medical help arrives.",
                "First Aid Fact : Elevating an injured limb can reduce swelling and pain.",
                "First Aid Fact : Never move someone who might have a spinal injury unless it's absolutely necessary to prevent further harm.",
                "First Aid Fact : Keeping a first aid kit at home and in your car is important for emergencies.",
                "First Aid Fact : Knowing how to recognize signs of a heart attack or stroke can prompt quick action and save a life.",
                "First Aid Fact : Learning the Heimlich maneuver can help clear a blocked airway in someone who is choking.",
                "First Aid Fact : Treating minor burns with cool, running water can ease pain and prevent further damage.",
                "First Aid Fact : Always check for allergies before administering any medication or treatment to someone else.",
                "First Aid Fact : Knowing how to apply a sling or splint can stabilize a broken bone or injured joint.",
                "First Aid Fact : Understanding when and how to call emergency services (like 911) is crucial in serious situations.",
                "First Aid Fact : Keeping yourself safe while providing first aid is important; don't put yourself in danger.",
                "First Aid Fact : Reassuring and comforting someone who is injured or ill can help reduce panic and stress.",
                "First Aid Fact : It's important to monitor someone's breathing and pulse while providing first aid.",
                "First Aid Fact : Learning basic first aid techniques can give you confidence to act in an emergency.",
                "First Aid Fact : Understanding the basics of wound care and infection prevention is essential.",
                "First Aid Fact : Clear communication with emergency responders can help them provide the best care upon arrival.",
                "First Aid Fact : Regularly refreshing your first aid skills through courses or practice can keep your knowledge up-to-date."
            };
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
                DialogResult dialogResult = MessageBox.Show("Do you want to increase your health by 50 for 50 coins?", "Increase Health", MessageBoxButtons.YesNo);
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
                        MessageBox.Show("Not enough coins!");
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
            if (playerHealth < 100) 
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to heal fully for 100 coins?", "Increase Health", MessageBoxButtons.YesNo);
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
                        MessageBox.Show("Not enough coins!");
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
