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
            labelStats.Text = "Zombies Killed : " + this.score + "\nCash on hand : " + coin;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit(); // Exit game
        }
    }
}
