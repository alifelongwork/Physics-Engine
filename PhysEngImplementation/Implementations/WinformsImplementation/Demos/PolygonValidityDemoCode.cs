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
        Polygon validityPolygon;
        int validitySelectedVertexIndex;

        void DrawPolygonValidity()
        {
            Bitmap bitmap = new Bitmap(PolygonValidityPictureBox.Width, PolygonValidityPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (validityPolygon == null)
            {
                validityPolygon = Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, 5, 100, bitmap.Width / 2f, bitmap.Height / 2f);
            }

            if (validitySelectedVertexIndex != -1)
            {
                Line line1 = validityPolygon.Edges[validitySelectedVertexIndex];
                Line line2 = validityPolygon.Edges[(validitySelectedVertexIndex + validityPolygon.Edges.Length - 1) % validityPolygon.Edges.Length];
                //$"{line2.Rotation - line1.Rotation}"
                graphics.DrawString(Polygon.AngleBetweenEdges(line1, line2).ToString(), new Font(FontFamily.GenericSansSerif, 15), Brushes.Black, 25, 25);
            }
            validityPolygon.Draw(graphics);
            PolygonValidityPictureBox.Image = bitmap;
        }

        private void PolygonValidityTab_Enter(object sender, EventArgs e)
        {
            DrawPolygonValidity();
        }

        private void PolygonValidityPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < validityPolygon.Vertices.Length; i++)
            {
                if (Vector2D.Distance(validityPolygon.Vertices[i], new Vector2D(e.X, e.Y)) < 5)
                {
                    validitySelectedVertexIndex = i;
                    break;
                }
            }
        }

        private void PolygonValidityPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (validitySelectedVertexIndex != -1)
            {
                validityPolygon.TranslateVertexBy(new Vector2D(e.X - validityPolygon.Vertices[validitySelectedVertexIndex].X, e.Y - validityPolygon.Vertices[validitySelectedVertexIndex].Y), validitySelectedVertexIndex);

                validityPolygon.Pen = validityPolygon.IsValidPolygon ? validityPolygon.DefaultPen : Pens.Green;
                validityPolygon.Brush = validityPolygon.IsValidPolygon ? validityPolygon.DefaultBrush : Brushes.Green;
            }
            DrawPolygonValidity();
        }

        private void PolygonValidityPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            validitySelectedVertexIndex = -1;
        }
    }
}
