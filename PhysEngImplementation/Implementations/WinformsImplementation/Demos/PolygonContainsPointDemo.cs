using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImplementationsLibrary;

namespace WinformsImplementation
{
    public partial class Form1
    {
        Polygon polygonForPolygonContainsPoint;
        Line lineForPolygonContainsPoint;
        int numberOfSidesForPolygonContainsPoint = 4;
        Vertex selectedVertexForPolygonContainsPoint = null;
        Point mousePositionForPolygonContainsPoint = Point.Empty;

        void DrawPolygonContainsPoint()
        {
            Bitmap bitmap = new Bitmap(PolygonContainsPointPictureBox.Width, PolygonContainsPointPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            int count = 0;
            foreach (Line line in polygonForPolygonContainsPoint.Edges)
            {
                if (line.Intersects(lineForPolygonContainsPoint))
                {
                    count++;
                }
            }

            polygonForPolygonContainsPoint.Draw(graphics);
            graphics.DrawLine(Pens.Green, 0, lineForPolygonContainsPoint.Start.Y, lineForPolygonContainsPoint.End.X, lineForPolygonContainsPoint.End.Y);
            graphics.DrawString($"Number Of Intersections: {count} => {count % 2 == 1}", SystemFonts.DefaultFont, Brushes.Black, 25, 25);
            graphics.DrawString($"Contains Point: {polygonForPolygonContainsPoint.Contains(mousePositionForPolygonContainsPoint)}", SystemFonts.DefaultFont, Brushes.Black, 25, 50);

            PolygonContainsPointPictureBox.Image = bitmap;
        }


        private void PolygonContainsPointPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            polygonForPolygonContainsPoint.RotateBy(e.Delta / 1000f);

            DrawPolygonContainsPoint();
        }

        private void PolygonContainsPointDemoTab_Enter(object sender, EventArgs e)
        {
            polygonForPolygonContainsPoint = Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, numberOfSidesForPolygonContainsPoint, 100, PolygonContainsPointPictureBox.Width / 2, PolygonContainsPointPictureBox.Height / 2);
            polygonForPolygonContainsPoint.RotateBy((float)Math.PI / 4);
            lineForPolygonContainsPoint = new Line(new Vector2D(0, 0), Vector2D.Zero);

            DrawPolygonContainsPoint();
        }

        private void PolygonContainsPointPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mousePositionForPolygonContainsPoint = e.Location;

            lineForPolygonContainsPoint.Start += new Vertex(polygonForPolygonContainsPoint.MinX - lineForPolygonContainsPoint.Start.X, e.Y - lineForPolygonContainsPoint.Start.Y);
            lineForPolygonContainsPoint.End += new Vertex(e.X - lineForPolygonContainsPoint.End.X, e.Y - lineForPolygonContainsPoint.End.Y);


            if (selectedVertexForPolygonContainsPoint != null)
            {
                selectedVertexForPolygonContainsPoint.Translate(e.X - selectedVertexForPolygonContainsPoint.X, e.Y - selectedVertexForPolygonContainsPoint.Y);
            }

            DrawPolygonContainsPoint();
        }
        private void PolygonContainsPointPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedVertexForPolygonContainsPoint == null)
            {
                foreach (Vertex vertex in polygonForPolygonContainsPoint.Vertices)
                {
                    if (Vector2D.Distance(vertex, new Vector2D(e.X, e.Y)) < 10)
                    {
                        selectedVertexForPolygonContainsPoint = vertex;
                        break;
                    }
                }

                if (selectedVertexForPolygonContainsPoint == null)
                {
                    if (e.Button == MouseButtons.Left && numberOfSidesForPolygonContainsPoint > 3)
                    {
                        numberOfSidesForPolygonContainsPoint--;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        numberOfSidesForPolygonContainsPoint++;
                    }
                    polygonForPolygonContainsPoint = Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, numberOfSidesForPolygonContainsPoint, 100, PolygonContainsPointPictureBox.Width / 2, PolygonContainsPointPictureBox.Height / 2);
                }
            }
            else
            {
                selectedVertexForPolygonContainsPoint = null;
            }
            DrawPolygonContainsPoint();
        }

    }
}
