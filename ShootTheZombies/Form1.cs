namespace ShootTheZombies
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ControlsScreen gameWindow = new ControlsScreen();
            gameWindow.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
