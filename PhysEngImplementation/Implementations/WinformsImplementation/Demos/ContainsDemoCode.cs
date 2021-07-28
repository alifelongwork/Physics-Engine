using System;
using System.CodeDom;
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
        MouseEventArgs Containsms;
        MouseEventArgs Containslms;
        Polygon[] Containspolygons;
        Polygon ContainsSelectedPolygon;
        Vertex ContainsSelectedVertex;

        void DrawContains()
        {
            Bitmap bitmap = new Bitmap(ContainsDemoPictureBox.Width, ContainsDemoPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            if (ContainsSelectedPolygon != null)
            {
                int i = 0;
                int j = 1;
                if (ContainsSelectedPolygon == Containspolygons[0])
                {
                    i = 1;
                    j = 0;
                }
                if (Containspolygons[i].Contains(Containspolygons[j]))
                {
                    Containspolygons[i].Pen = Pens.Blue;
                    Containspolygons[j].Pen = Pens.Blue;
                    Containspolygons[i].Brush = Brushes.Blue;
                    Containspolygons[j].Brush = Brushes.Blue;
                }
                else
                {
                    Containspolygons[i].Pen = Containspolygons[i].DefaultPen;
                    Containspolygons[j].Pen = Containspolygons[j].DefaultPen;
                    Containspolygons[i].Brush = Containspolygons[i].DefaultBrush;
                    Containspolygons[j].Brush = Containspolygons[j].DefaultBrush;
                }
            }

            for (int i = 0; i < Containspolygons.Length; i++)
            {
                Containspolygons[i].Draw(graphics);
            }


            ContainsDemoPictureBox.Image = bitmap;
        }

        private void PolygonContainsPolygonDemoTab_Enter(object sender, EventArgs e)
        {
            ContainsDemoPictureBox.MouseWheel += ContainsDemoPictureBox_MouseWheel;

            Containspolygons = new Polygon[2];
            Containspolygons[0] = new Polygon(new Vertex(100, 100)
                                        , new Vertex(200, 100)
                                        , new Vertex(250, 150)
                                        , new Vertex(200, 200)
                                        , new Vertex(100, 200));
            Containspolygons[0].DefaultPen = Pens.Black;
            Containspolygons[0].DefaultBrush = Brushes.Black;

            Containspolygons[1] = new Polygon(new Vertex(225, 100)
                                        , new Vertex(325, 100)
                                        , new Vertex(325, 200)
                                        , new Vertex(225, 200));
            Containspolygons[1].DefaultPen = Pens.Green;
            Containspolygons[1].DefaultBrush = Brushes.Green;
        }

        private void ContainsDemoPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Containslms = Containsms;
            Containsms = e;

            ContainsSelectedPolygon?.TranslateBy(new Vector2D(Containsms.X - Containslms.X, Containsms.Y - Containslms.Y));
            ContainsSelectedVertex?.Translate(new Vector2D(Containsms.X - Containslms.X, Containsms.Y - Containslms.Y));

            for (int i = 0; i < Containspolygons.Length; i++)
            {
                Containspolygons[i].Pen = Containspolygons[i].Contains(Containsms.Location) ? Pens.Red : Containspolygons[i].DefaultPen;
                Containspolygons[i].Brush = Containspolygons[i].Pen == Pens.Red ? Brushes.Red : Containspolygons[i].DefaultBrush;
            }

            DrawContains();
        }

        private void ContainsDemoPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ContainsSelectedPolygon != null)
            {
                ContainsSelectedPolygon.RotateBy(e.Delta / 2000f);
            }
            DrawContains();
        }

        private void ContainsDemoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ContainsSelectedVertex == null)
            {
                foreach (Vertex vertex in Containspolygons[0].Vertices)
                {
                    if (vertex.AsRectangle().Contains(e.Location))
                    {
                        ContainsSelectedVertex = vertex;
                        ContainsSelectedPolygon = null;
                        return;
                    }
                }
                foreach (Vertex vertex in Containspolygons[1].Vertices)
                {
                    if (vertex.AsRectangle().Contains(e.Location))
                    {
                        ContainsSelectedVertex = vertex;
                        ContainsSelectedPolygon = null;
                        return;
                    }
                }
            }
            else
            {
                ContainsSelectedVertex = null;
                return;
            }

            if (ContainsSelectedPolygon == null)
            {
                for (int i = 0; i < Containspolygons.Length; i++)
                {
                    if (Containspolygons[i].Contains(Containsms.Location))
                    {
                        ContainsSelectedPolygon = Containspolygons[i];
                        break;
                    }
                }
            }
            else
            {
                ContainsSelectedPolygon = null;
            }

        }

    }
}
