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

        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        
        bool mute = false;
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
    }
}
