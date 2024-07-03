namespace ShootTheZombiesRedo
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
            this.components = new System.ComponentModel.Container();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.door = new System.Windows.Forms.PictureBox();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.door)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 20;
            this.mainTimer.Tick += new System.EventHandler(this.TimerEvent);
            // 
            // door
            // 
            this.door.BackColor = System.Drawing.Color.Transparent;
            this.door.Location = new System.Drawing.Point(425, 540);
            this.door.Name = "door";
            this.door.Size = new System.Drawing.Size(134, 27);
            this.door.TabIndex = 0;
            this.door.TabStop = false;
            // 
            // healthBar
            // 
            this.healthBar.Location = new System.Drawing.Point(57, 12);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(547, 23);
            this.healthBar.TabIndex = 1;
            // 
            // GameMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.ControlBox = false;
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.door);
            this.MaximizeBox = false;
            this.Name = "GameMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shoot The Zombies";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPaintEvent);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.door)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.PictureBox door;
        private System.Windows.Forms.ProgressBar healthBar;
    }
}