namespace ShootTheZombies
{
    partial class ControlsScreen
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
            panel1 = new Panel();
            timerControls = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(984, 561);
            panel1.TabIndex = 0;
            // 
            // timerControls
            // 
            timerControls.Enabled = true;
            timerControls.Interval = 10000;
            timerControls.Tick += timerControls_Tick;
            // 
            // ControlsScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(panel1);
            Name = "ControlsScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ControlsScreen";
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private System.Windows.Forms.Timer timerControls;
    }
}