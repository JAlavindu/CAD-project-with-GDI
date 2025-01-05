using System.Drawing.Drawing2D;

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

        private double degToRad(double degrees)
        {
            return (double)(Math.PI / 180) * degrees;
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
            return result;
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

            // The center is calculated to do the transformation around the center of the line
            Point centerOfLine = new Point((lineStart.X + lineEnd.X) / 2, (lineStart.Y + lineEnd.Y) / 2);

            // Transformation matrix for the line
            Matrix composite = new Matrix(1, 0, 0, 1, centerOfLine.X, centerOfLine.Y);
            composite.Multiply(new Matrix((float)scaleLine, 0, 0, (float)scaleLine, 0, 0)); // Scale
            float cos = (float)Math.Cos(degToRad(angleLine));
            float sin = (float)Math.Sin(degToRad(angleLine));
            composite.Multiply(new Matrix(cos, -sin, sin, cos, 0, 0)); // Rotate
            composite.Multiply(new Matrix(1, 0, 0, 1, -centerOfLine.X, -centerOfLine.Y)); // Move back
            g.Transform = composite;
            g.DrawLine(Pens.Black, lineStart, lineEnd); // Draw the line

            // Now, rotate the rectangle around its top-left corner
            composite.Dispose(); // Discard the previous transformations
            composite = new Matrix(1, 0, 0, 1, rectangle.X, rectangle.Y);
            composite.Multiply(new Matrix((float)scaleRectangle, 0, 0, (float)scaleRectangle, 0, 0)); // Scale
            cos = (float)Math.Cos(degToRad(angleRectangle));
            sin = (float)Math.Sin(degToRad(angleRectangle));
            composite.Multiply(new Matrix(cos, -sin, sin, cos, 0, 0)); // Rotate
            composite.Multiply(new Matrix(1, 0, 0, 1, -rectangle.X, -rectangle.Y)); // Move back
            g.Transform = composite;
            g.DrawRectangle(Pens.Red, rectangle); // Draw the rectangle

            composite.Dispose(); // Clean up
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

        private void Form1_MouseHover(object sender, MouseEventArgs e)
        {
            int selection = getSelection(e.Location);
            switch (selection)
            {
                case 1: // Line is selected
                    if (e.Delta > 0) angleLine += 1; //Anticlockwise
                    else angleLine -= 1; break;// Clockwise
                case 2: // Rectangle is selected
                    if (e.Delta > 0) angleRectangle += 1; //Anticlockwise
                    else angleRectangle -= 1; break;// Clockwise
            }
            Refresh();//request redraw the form
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if the mouse is within the bounds of the rectangle
            if (e.X > rectangle.X && e.X < rectangle.Right && e.Y > rectangle.Y && e.Y < rectangle.Bottom)
            {
                // Increment the rotation angle
                angleRectangle += 1; // You can change this value to control the speed of rotation
                if (angleRectangle >= 360) angleRectangle = 0; // Reset the angle after a full rotation

                // Force a redraw of the form to apply the transformation
                this.Invalidate(); // This will call the Form1_Paint method again
            }
        }
    }
}
