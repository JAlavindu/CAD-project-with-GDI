namespace MyApp
{
    public partial class Form1 : Form
    {
        Point lineStart = new Point(0, 0);
        Point lineEnd = new Point(0, 0);
        Rectangle rectangle = new Rectangle(0, 0, 0, 0);
        int currentSelection = 0; // 0 - nothing, 1 - line, 2 - rectangle
        double scaleLine = 1; // scaling factor of the line
        double scaleRectangle = 1; // scaling factor of the rectangle
        double angleLine = 0; // rotation angle of the line
        double angleRectangle = 0; // rotation angle of the rectangle

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Here we assign some arbitrary values to be act as the default
            lineStart.X = 10;
            lineStart.Y = 10;
            lineEnd.X = 100;
            lineEnd.Y = 100;
            Refresh(); //This method requests the system to redraw the form
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
