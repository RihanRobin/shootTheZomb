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
    public partial class EndScreen : Form
    {
        int score;
        public EndScreen(int score, int coin)
        {
            InitializeComponent();

            this.score = score;
            labelStats.Text = "Zombies Killed : " + this.score + "\nCash on hand : $" + coin;
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

        private void button1_Click(object sender, EventArgs e)
        {
            GameMain frm = new GameMain();
            frm.score = 0;
            frm.playerHealth = 100;
            frm.ammo = 5;
            frm.coin = 0;
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit game
        }
    }
}
