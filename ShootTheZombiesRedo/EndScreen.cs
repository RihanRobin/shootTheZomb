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
        public int score { get; set; }
        public int coin { get; set; }
        public EndScreen()
        {
            InitializeComponent();

            labelStats.Text = "Your Score Was : " + score;
        }
    }
}
