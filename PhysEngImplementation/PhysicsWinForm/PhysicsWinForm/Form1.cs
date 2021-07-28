using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsWinForm
{
    
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics gfx;
        Brush brush;
        Pen pen;
        Polygon rectangle;
        VMM test;
        float yprev;
        float xprev;
        float prevAng;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pen = new Pen(new SolidBrush(Color.Black));
         
            brush = new SolidBrush(Color.Red);
            bmp = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            gfx = Graphics.FromImage(bmp);
            test = new VMM();
         
            rectangle = new Polygon(4);
            xPosition.Value = (decimal)rectangle.points[0].X;
            yPosition.Value = (decimal)rectangle.points[0].Y;
            pictureBox1.Image = bmp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            gfx.Clear(BackColor);
            rotateRadVal.Text = ((rotateShapeAngle.Value * (decimal)Math.PI) / 180).ToString("0.00 radians");
         
            yprev = (float)yPosition.Value;
            xprev = (float)xPosition.Value;
            prevAng = (float)rotateShapeAngle.Value;
            //draw the shape
            gfx.DrawLine(pen, rectangle.points[0].X, rectangle.points[0].Y, rectangle.points[1].X, rectangle.points[1].Y);
            gfx.DrawLine(pen, rectangle.points[1].X, rectangle.points[1].Y, rectangle.points[2].X, rectangle.points[2].Y);
            gfx.DrawLine(pen, rectangle.points[2].X, rectangle.points[2].Y, rectangle.points[3].X, rectangle.points[3].Y);
            gfx.DrawLine(pen, rectangle.points[3].X, rectangle.points[3].Y, rectangle.points[0].X, rectangle.points[0].Y);
            
            //draw line pointing to shapes center
            gfx.DrawLine(pen, 0, 0, rectangle.center.X, rectangle.center.Y);

            pictureBox1.Image = bmp;
        }
        private void xPosition_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rectangle.points.Length; i++)
            {
                if (xprev - (float)xPosition.Value > 0)
                {
                    rectangle.points[i].X -= (float)xPosition.Increment;
                }
                else if(xprev - (float)xPosition.Value < 0)
                {
                    rectangle.points[i].X += (float)xPosition.Increment;
                }
            }

            rectangle.center.Y = rectangle.points[0].Y + (rectangle.points[2].Y - rectangle.points[0].Y) / 2f;
            rectangle.center.X = rectangle.points[0].X + (rectangle.points[2].X - rectangle.points[0].X) / 2f;
        }
        private void yPosition_ValueChanged(object sender, EventArgs e)
        {              
            for (int i = 0; i < rectangle.points.Length; i++)
            {
                if (yprev - (float)yPosition.Value > 0)
                {
                    rectangle.points[i].Y -= (float)yPosition.Increment;
                }
                else if(yprev - (float)yPosition.Value < 0)
                {
                    rectangle.points[i].Y += (float)yPosition.Increment;
                }
            }

            rectangle.center.Y = rectangle.points[0].Y + (rectangle.points[2].Y - rectangle.points[0].Y)/2f;
            rectangle.center.X = rectangle.points[0].X + (rectangle.points[2].X - rectangle.points[0].X)/2f;

        }
        private void rotateShapeAngle_ValueChanged(object sender, EventArgs e)
        {            
            for (int i = 0; i < rectangle.points.Length; i++)
            {
                if (prevAng - (float)rotateShapeAngle.Value > 0)
                {
                    float[,] tempXY = rectangle.Translate(rectangle.points[i].X, rectangle.points[i].Y, -rectangle.center.X, -rectangle.center.Y);
                    float[,] tempRot = rectangle.Rotate(tempXY[0, 0], tempXY[1, 0], -(float)rotateShapeAngle.Increment);
                    tempXY = rectangle.Translate(tempRot[0, 0], tempRot[1, 0], rectangle.center.X, rectangle.center.Y);
                    rectangle.points[i].X = tempXY[0, 0];
                    rectangle.points[i].Y = tempXY[1, 0];
                }
                else if(prevAng - (float)rotateShapeAngle.Value < 0)
                {
                    float[,] tempXY = rectangle.Translate(rectangle.points[i].X, rectangle.points[i].Y, -rectangle.center.X, -rectangle.center.Y);
                    float[,] tempRot = rectangle.Rotate(tempXY[0, 0], tempXY[1, 0], (float)rotateShapeAngle.Increment);
                    tempXY = rectangle.Translate(tempRot[0, 0], tempRot[1, 0], rectangle.center.X, rectangle.center.Y);
                    rectangle.points[i].X = tempXY[0, 0];
                    rectangle.points[i].Y = tempXY[1, 0];
                }
            }
        }        
    }
}
