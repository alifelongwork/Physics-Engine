using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImplementationsLibrary;
using WinformsImplementation.Properties;

namespace WinformsImplementation
{
    public partial class Form1
    {
        Polygon triangulationPolygon;
        int triangulationSelectedVertexIndex;

        void DrawTriangulation()
        {
            Bitmap bitmap = new Bitmap(TriangulationPictureBox.Width, TriangulationPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (triangulationPolygon == null)
            {
                triangulationPolygon = Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, 5, 100, bitmap.Width / 2f, bitmap.Height / 2f);
                triangulationPolygon.Pen = Pens.Blue;
            }


            Polygon[] triangles = triangulationPolygon.Triangulation();
            foreach(Polygon triangle in triangles)
            {
                triangle.Draw(graphics);
            }
            triangulationPolygon.Draw(graphics);

            TriangulationPictureBox.Image = bitmap;
        }

        private void TriangulationDemoTab_Enter(object sender, EventArgs e)
        {
            DrawTriangulation();
        }

        private void TriangulationPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < triangulationPolygon.Vertices.Length; i++)
            {
                if (Vector2D.Distance(triangulationPolygon.Vertices[i], new Vector2D(e.X, e.Y)) < 5)
                {
                    triangulationSelectedVertexIndex = i;
                    break;
                }
            }
        }

        private void TriangulationPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (triangulationSelectedVertexIndex != -1)
            {
                triangulationPolygon.TranslateVertexBy(new Vector2D(e.X - triangulationPolygon.Vertices[triangulationSelectedVertexIndex].X, e.Y - triangulationPolygon.Vertices[triangulationSelectedVertexIndex].Y), triangulationSelectedVertexIndex);
            }
            DrawTriangulation();
        }

        private void TriangulationPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            triangulationSelectedVertexIndex = -1;
        }
    }
}
