using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ShootTheZombies
{
    public partial class ControlsScreen : Form
    {
        public ControlsScreen()
        {
            InitializeComponent();
            timerControls.Interval = 1000; // 10 seconds in milliseconds
            timerControls.Enabled = true; // Ensure the timer is enabled
        }

        private void timerControls_Tick(object sender, EventArgs e)
        {
            timerControls.Stop();

            GameMain form2 = new GameMain();
            form2.Show();
            this.Close();


        }
    }
}
