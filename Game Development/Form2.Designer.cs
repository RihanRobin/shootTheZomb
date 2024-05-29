namespace Game_Development
{
    partial class Form2
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
            this.screen = new System.Windows.Forms.Panel();
            this.shop = new System.Windows.Forms.PictureBox();
            this.playerShop = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.screen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerShop)).BeginInit();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.BackColor = System.Drawing.Color.Black;
            this.screen.Controls.Add(this.shop);
            this.screen.Controls.Add(this.playerShop);
            this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screen.Location = new System.Drawing.Point(0, 0);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(800, 450);
            this.screen.TabIndex = 1;
            // 
            // shop
            // 
            this.shop.BackColor = System.Drawing.Color.Lime;
            this.shop.Location = new System.Drawing.Point(0, 0);
            this.shop.Name = "shop";
            this.shop.Size = new System.Drawing.Size(68, 21);
            this.shop.TabIndex = 1;
            this.shop.TabStop = false;
            // 
            // playerShop
            // 
            this.playerShop.BackColor = System.Drawing.Color.White;
            this.playerShop.Location = new System.Drawing.Point(22, 27);
            this.playerShop.Name = "playerShop";
            this.playerShop.Size = new System.Drawing.Size(20, 20);
            this.playerShop.TabIndex = 0;
            this.playerShop.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.screen);
            this.Name = "Form2";
            this.Text = "Shop";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyUp);
            this.screen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.shop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerShop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel screen;
        private System.Windows.Forms.PictureBox shop;
        private System.Windows.Forms.PictureBox playerShop;
        private System.Windows.Forms.Timer timer1;
    }
}