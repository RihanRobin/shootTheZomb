namespace ShootTheZombies
{
    partial class GameMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainTimer = new System.Windows.Forms.Timer(components);
            healthBar = new ProgressBar();
            door = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)door).BeginInit();
            SuspendLayout();
            // 
            // mainTimer
            // 
            mainTimer.Enabled = true;
            mainTimer.Interval = 20;
            mainTimer.Tick += TimerEvent;
            // 
            // healthBar
            // 
            healthBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            healthBar.Location = new Point(798, 12);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(174, 23);
            healthBar.TabIndex = 0;
            // 
            // door
            // 
            door.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            door.BackColor = Color.IndianRed;
            door.Location = new Point(877, 540);
            door.Name = "door";
            door.Size = new Size(109, 21);
            door.TabIndex = 1;
            door.TabStop = false;
            // 
            // GameMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(984, 561);
            Controls.Add(door);
            Controls.Add(healthBar);
            Name = "GameMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameMain";
            Paint += FormPaintEvent;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)door).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer mainTimer;
        private ProgressBar healthBar;
        private PictureBox door;
    }
}