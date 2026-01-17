namespace IParking.RegisterCustomer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent();
            Instance = this;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
        }
    }
}
