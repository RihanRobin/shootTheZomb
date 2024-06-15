using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Game_Development
{
    internal class Bullet
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();

        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.Green;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            switch (direction)
            {
                case "left":
                    bullet.Left -= speed;
                    break;
                case "right":
                    bullet.Left += speed;
                    break;
                case "up":
                    bullet.Top -= speed;
                    break;
                case "down":
                    bullet.Top += speed;
                    break;
            }

            if (Math.Abs(bullet.Left - bulletLeft) > 200 || Math.Abs(bullet.Top - bulletTop) > 200)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }
        }
    }
}
