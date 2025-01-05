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

        public int getSelection(Point p)
        {
            int result = 0;
            //Obtain the bounding box of the line
            int maxX = Math.Max(lineStart.X, lineEnd.X);
            int maxY = Math.Max(lineStart.Y, lineEnd.Y);
            int minX = Math.Min(lineStart.X, lineEnd.X);
            int minY = Math.Min(lineStart.Y, lineEnd.Y);
            //Check whether the point is in the bounding box of the line
            if (p.X > minX && p.Y > minY &&
            p.X < maxX && p.Y < maxY)
                result = 1;
            //Check whether the point is in the rectangle
            if (p.X > rectangle.X && p.Y > rectangle.Y &&
            p.X < rectangle.Right && p.Y < rectangle.Bottom)
                result = 2;
            return

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
            //Here we assign some arbitrary values to be act as the default
            rectangle.X = 120;
            rectangle.Y = 120;
            rectangle.Width = 80;
            rectangle.Height = 40;
            Refresh(); //This method requests the system to redraw the form
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.Black, lineStart, lineEnd);
            g.DrawRectangle(Pens.Red, rectangle);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int selection = getSelection(e.Location);//Selected Object
            if (e.Button == MouseButtons.Left)
            {// Scale up
                switch (selection)
                {
                    case 1: scaleLine += 0.1; break; //Scale up of the line
                    case 2: scaleRectangle += 0.1; break; // Scale up the rectangle
                }
            }
            else if (e.Button == MouseButtons.Right)
            { //Scale Down
                switch (selection)
                {
                    case 1: scaleLine -= 0.1; break;//Scale down the line
                    case 2: scaleRectangle -= 0.1; break; //scale down the rectangle
                }
            }
            Refresh();//request redraw the form
        }
    }
}
