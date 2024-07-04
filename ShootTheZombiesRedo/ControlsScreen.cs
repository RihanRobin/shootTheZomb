using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootTheZombiesRedo
{
    public partial class ControlsScreen : Form
    {

        int imageChange = 4000;
        int totalDuration = 12000;
        int elapsedTime = 0;
        public ControlsScreen()
        {
            InitializeComponent();
            timerControls.Interval = 1000; 
            timerControls.Enabled = true;
        }

        private void timerControls_Tick(object sender, EventArgs e)
        {
            elapsedTime += timerControls.Interval;

            if (elapsedTime >= imageChange && elapsedTime < totalDuration)
            {
                // Change the image in the PictureBox
                pictureBox1.Image = Image.FromFile("slogan1.png"); // Replace with your new image
            }

            if (elapsedTime >= totalDuration)
            {
                timerControls.Stop();

                GameMain form2 = new GameMain();
                form2.Show();
                this.Close();


            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) 
            {
                timerControls.Stop();

                GameMain form2 = new GameMain();
                form2.Show();
                this.Close();
            }
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
