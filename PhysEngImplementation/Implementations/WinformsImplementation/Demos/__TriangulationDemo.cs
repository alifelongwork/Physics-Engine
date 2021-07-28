using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsImplementation
{
    /*public partial class Form1
    {
        List<Vertex> triangulationVertices;

        private void TriangulationDemoTab_Enter(object sender, EventArgs e)
        {
            triangulationVertices = null;
        }
        private void TriangulationdemoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                triangulationVertices?.Add(new Vertex(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Right && triangulationVertices != null && triangulationVertices.Count >= 3)
            {
                triangulationDrawPolygon(new Polygon(triangulationVertices.ToArray()));
                triangulationVertices = null;
            }
        }
        private void TriangulationdemoPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (triangulationVertices == null)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(TriangulationdemoPictureBox.Width, TriangulationdemoPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (triangulationVertices.Count > 0)
            {
                for (int i = 0; i < triangulationVertices.Count - 1; i++)
                {
                    int j = i + 1;

                    graphics.DrawLine(Pens.Black, triangulationVertices[i], triangulationVertices[j]);
                }
                graphics.DrawLine(Pens.Black, triangulationVertices[triangulationVertices.Count - 1], e.Location);
            }
            TriangulationdemoPictureBox.Image = bitmap;
        }

        void triangulationDrawPolygon(Polygon polygon)
        {
            Bitmap bitmap = new Bitmap(TriangulationdemoPictureBox.Width, TriangulationdemoPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            Polygon[] triangles = polygon.Triangulation();

            foreach (Polygon triangle in triangles)
            {
                triangle.Draw(graphics);
            }

            TriangulationdemoPictureBox.Image = bitmap;
        }


        private void TriangulationResetButton_Click(object sender, EventArgs e)
        {
            triangulationVertices = new List<Vertex>();
        }
    }*/
}
