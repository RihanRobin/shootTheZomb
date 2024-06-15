namespace ShootTheZombies
{
    partial class GameShop
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
            shopTimer = new System.Windows.Forms.Timer(components);
            door = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)door).BeginInit();
            SuspendLayout();
            // 
            // shopTimer
            // 
            shopTimer.Enabled = true;
            shopTimer.Interval = 20;
            shopTimer.Tick += TimerEvent;
            // 
            // door
            // 
            door.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            door.BackColor = Color.IndianRed;
            door.Location = new Point(0, 0);
            door.Name = "door";
            door.Size = new Size(109, 21);
            door.TabIndex = 2;
            door.TabStop = false;
            // 
            // GameShop
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(984, 561);
            Controls.Add(door);
            Name = "GameShop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shoot The Zombies";
            Paint += FormPaintEvent;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)door).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer shopTimer;
        private PictureBox door;
    }
}