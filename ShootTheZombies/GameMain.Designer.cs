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
            textBox1 = new TextBox();
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
            healthBar.Location = new Point(76, 13);
            healthBar.Margin = new Padding(3, 4, 3, 4);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(344, 31);
            healthBar.TabIndex = 0;
            // 
            // door
            // 
            door.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            door.BackColor = Color.IndianRed;
            door.Location = new Point(1002, 720);
            door.Margin = new Padding(3, 4, 3, 4);
            door.Name = "door";
            door.Size = new Size(125, 28);
            door.TabIndex = 1;
            door.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Black;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Enabled = false;
            textBox1.Font = new Font("Helvetica", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(12, 21);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(70, 18);
            textBox1.TabIndex = 2;
            textBox1.Text = "Health :";
            // 
            // GameMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1125, 748);
            Controls.Add(healthBar);
            Controls.Add(textBox1);
            Controls.Add(door);
            Margin = new Padding(3, 4, 3, 4);
            Name = "GameMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shoot The Zombies";
            Paint += FormPaintEvent;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)door).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer mainTimer;
        private ProgressBar healthBar;
        private PictureBox door;
        private TextBox textBox1;
    }
}