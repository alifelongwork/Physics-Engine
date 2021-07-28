using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImplementationsLibrary;

namespace WinformsImplementation
{
    public partial class Form1
    {
        QuadTree2<BouncingCircle> quadTree2;
        List<BouncingCircle> quadTree2Circles;
        bool quadTree2ShouldResetColor = true;
        Circle quadTree2SelectedCircle = null;

        void DrawQuadTree2()
        {
            Bitmap bitmap = new Bitmap(QuadTree2PictureBox.Width, QuadTree2PictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            quadTree2.Draw(graphics);
            foreach (Circle circle in quadTree2Circles)
            {
                if (quadTree2ShouldResetColor)
                {
                    circle.Color = Brushes.Black;
                }
                circle.Draw(graphics);
            }

            QuadTree2PictureBox.Image = bitmap;
        }

        private void QuadTree2DemoTab_Enter(object sender, EventArgs e)
        {
            quadTree2 = new QuadTree2<BouncingCircle>(new Rectangle(0, 0, QuadTree2PictureBox.Width - 1, QuadTree2PictureBox.Height - 1), 4);
            quadTree2Circles = new List<BouncingCircle>();
            DrawQuadTree2();

        }

        private void QuadTree2PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!QuadTree2EditModeCheckBox.Checked)
            {
                BouncingCircle circle = new BouncingCircle(new Vector2D(e.X, e.Y), (int)QuadTree2RadiusUpDown.Value, 2, (float)(random.NextDouble() * 2 * Math.PI - Math.PI));
                quadTree2.Add(circle);
                quadTree2Circles.Add(circle);
                DrawQuadTree2();
            }
            else
            {
                if (quadTree2SelectedCircle == null)
                {
                    foreach (Circle circle in quadTree2Circles)
                    {
                        if (circle.Rectangle.Contains(e.Location))
                        {
                            quadTree2SelectedCircle = circle;
                            break;
                        }
                    }
                }
                else
                {
                    quadTree2SelectedCircle = null;
                }
            }
        }
        private void QuadTree2PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!QuadTree2EditModeCheckBox.Checked)
            {
                quadTree2ShouldResetColor = true;
                foreach (BouncingCircle circle in quadTree2Circles)
                {
                    if (circle.Rectangle.Contains(e.Location))
                    {
                        quadTree2ShouldResetColor = false;
                        List<BouncingCircle> circlesToChangeColor = quadTree2.Retrieve(circle);
                        circle.Color = Brushes.Blue;
                        foreach (BouncingCircle circleToChangeColor in circlesToChangeColor)
                        {
                            circleToChangeColor.Color = Brushes.Blue;
                        }
                    }
                }
            }
            if (quadTree2SelectedCircle != null)
            {
                quadTree2SelectedCircle.TranslateTo(e.X, e.Y);

                quadTree2 = new QuadTree2<BouncingCircle>(quadTree2.Bounds, quadTree2.MaxNumberOfItemsPerQuadrant);
                quadTree2.AddRange(quadTree2Circles);
            }
            DrawQuadTree2();
        }

        private void QuadTree2StepButton_Click(object sender, EventArgs e)
        {
            foreach(BouncingCircle circle in quadTree2Circles)
            {
                circle.Update(QuadTree2PictureBox.Width, QuadTree2PictureBox.Height);
            }

            quadTree2 = new QuadTree2<BouncingCircle>(quadTree2.Bounds, quadTree2.MaxNumberOfItemsPerQuadrant);
            quadTree2.AddRange(quadTree2Circles);
            DrawQuadTree2();
        }

        private void QuadTree2AutoUpdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            QuadTree2Timer.Enabled = !QuadTree2Timer.Enabled;
        }
        private void QuadTree2Timer_Tick(object sender, EventArgs e)
        {
            foreach (BouncingCircle circle in quadTree2Circles)
            {
                circle.Update(QuadTree2PictureBox.Width, QuadTree2PictureBox.Height);
            }

            quadTree2 = new QuadTree2<BouncingCircle>(quadTree2.Bounds, quadTree2.MaxNumberOfItemsPerQuadrant);
            quadTree2.AddRange(quadTree2Circles);
            DrawQuadTree2();
        }
        private void QuadTree2UpdateSpeed_Scroll(object sender, EventArgs e)
        {
            QuadTree2Timer.Interval = QuadTree2UpdateSpeed.Value;
        }
    }
}
