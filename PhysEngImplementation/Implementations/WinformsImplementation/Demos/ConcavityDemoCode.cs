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
        Polygon concavityPolygon;
        int concavitySelectedVertexIndex;

        void DrawConcavity()
        {
            Bitmap bitmap = new Bitmap(ConcavityPictureBox.Width, ConcavityPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (concavityPolygon == null)
            {
                concavityPolygon = Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, 5, 100, bitmap.Width / 2f, bitmap.Height / 2f);
            }

            if (concavitySelectedVertexIndex != -1)
            {
                Line line1 = concavityPolygon.Edges[concavitySelectedVertexIndex];
                Line line2 = concavityPolygon.Edges[(concavitySelectedVertexIndex + concavityPolygon.Edges.Length - 1) % concavityPolygon.Edges.Length];
                //$"{line2.Rotation - line1.Rotation}"
                graphics.DrawString(Polygon.AngleBetweenEdges(line1, line2).ToString(), new Font(FontFamily.GenericSansSerif, 15), Brushes.Black, 25, 25);
                graphics.DrawString(Polygon.AngleBetweenEdges(line2, line1).ToString(), new Font(FontFamily.GenericSansSerif, 15), Brushes.Black, 25, 50);
            }
            concavityPolygon.Draw(graphics);
            ConcavityPictureBox.Image = bitmap;
        }

        private void ConcavityDemoTab_Enter(object sender, EventArgs e)
        {
            DrawConcavity();
        }

        private void ConcavityPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < concavityPolygon.Vertices.Length; i++)
            {
                if (Vector2D.Distance(concavityPolygon.Vertices[i], new Vector2D(e.X, e.Y)) < 5)
                {
                    concavitySelectedVertexIndex = i;
                    break;
                }
            }
        }

        private void ConcavityPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (concavitySelectedVertexIndex != -1)
            {
                concavityPolygon.TranslateVertexBy(new Vector2D(e.X - concavityPolygon.Vertices[concavitySelectedVertexIndex].X, e.Y - concavityPolygon.Vertices[concavitySelectedVertexIndex].Y), concavitySelectedVertexIndex);

                concavityPolygon.Pen = concavityPolygon.IsConvex ? concavityPolygon.DefaultPen : Pens.Green;
                concavityPolygon.Brush = concavityPolygon.IsConvex ? concavityPolygon.DefaultBrush : Brushes.Green;
            }
            DrawConcavity();
        }

        private void ConcavityPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            concavitySelectedVertexIndex = -1;
        }
    }
}
