using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootTheZombiesRedo
{
    public partial class StartMenu : Form
    {

        public static System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        
        public static bool mute = false;
        public StartMenu()
        {
            InitializeComponent();

            player.SoundLocation = "10 bit.wav";
            player.PlayLooping();


            this.BackgroundImage = Image.FromFile("titlebackground.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ControlsScreen gameWindow = new ControlsScreen();
            gameWindow.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Click(object sender, EventArgs e)
        {
            if (mute == false)
            {
                mute = true;
                pictureBox2.Image = Image.FromFile("mute.png");
                player.Stop();
            }
            else
            {
                mute = false;
                pictureBox2.Image = Image.FromFile("play.png");
                player.PlayLooping();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit();
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
        }
    }
}
