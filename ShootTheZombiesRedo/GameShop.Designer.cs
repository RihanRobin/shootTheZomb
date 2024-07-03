namespace ShootTheZombiesRedo
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
            this.components = new System.ComponentModel.Container();
            this.shopTimer = new System.Windows.Forms.Timer(this.components);
            this.door = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.door)).BeginInit();
            this.SuspendLayout();
            // 
            // shopTimer
            // 
            this.shopTimer.Enabled = true;
            this.shopTimer.Interval = 20;
            this.shopTimer.Tick += new System.EventHandler(this.TimerEvent);
            // 
            // door
            // 
            this.door.BackColor = System.Drawing.Color.Transparent;
            this.door.Location = new System.Drawing.Point(425, -7);
            this.door.Name = "door";
            this.door.Size = new System.Drawing.Size(134, 27);
            this.door.TabIndex = 1;
            this.door.TabStop = false;
            // 
            // GameShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.ControlBox = false;
            this.Controls.Add(this.door);
            this.MaximizeBox = false;
            this.Name = "GameShop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameShop";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPaintEvent);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.door)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer shopTimer;
        private System.Windows.Forms.PictureBox door;
    }
}