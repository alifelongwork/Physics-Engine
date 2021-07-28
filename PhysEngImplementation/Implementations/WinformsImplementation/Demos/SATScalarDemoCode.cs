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
        MouseEventArgs SATScalarms;
        MouseEventArgs SATScalarlms;
        Polygon[] SATScalarpolygons;
        Polygon SATScalarselectedPolygon;

        float maxScalarBarSize = 20;
        float totalWidth = 100;
        float totalHeight = 200;

        void DrawSATScalar()
        {
            Bitmap bitmap = new Bitmap(SATScalarPictureBox.Width, SATScalarPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (SATScalarselectedPolygon != null)
            {
                (bool intersects, ((float min, float max) first, (float min, float max) second)[] lines) = SATScalarpolygons[0].SeparatingAxisTheoremScalarWithSimulation(SATScalarpolygons[1]);
                if (intersects)
                {
                    SATScalarpolygons[0].Pen = Pens.Blue;
                    SATScalarpolygons[1].Pen = Pens.Blue;
                    SATScalarpolygons[0].Brush = Brushes.Blue;
                    SATScalarpolygons[1].Brush = Brushes.Blue;
                }
                else
                {
                    SATScalarpolygons[0].Brush = SATScalarpolygons[0].DefaultBrush;
                    SATScalarpolygons[0].Pen = SATScalarpolygons[0].DefaultPen;
                    SATScalarpolygons[1].Brush = SATScalarpolygons[1].DefaultBrush;
                    SATScalarpolygons[1].Pen = SATScalarpolygons[1].DefaultPen;
                }

                float size = totalHeight / lines.Length - 5;
                size = size > maxScalarBarSize ? maxScalarBarSize : size;
                size = size < 1 ? 1 : size;

                float currentHeight = 15;

                foreach (var line in lines)
                {
                    float max = line.first.max > line.second.max ? line.first.max : line.second.max;
                    float min = line.first.min < line.second.min ? line.first.min : line.second.min;

                    float realWidth = max - min;
                    float scale = totalWidth / realWidth;
                    Brush red = new SolidBrush(Color.FromArgb(200, Color.Red));
                    Brush green = new SolidBrush(Color.FromArgb(200, Color.Yellow));

                    graphics.FillRectangle(red, SATScalarPictureBox.Width / 2 + line.first.min * scale, currentHeight, (line.first.max - line.first.min) * scale, size);
                    graphics.FillRectangle(green, SATScalarPictureBox.Width / 2 + line.second.min * scale, currentHeight, (line.second.max - line.second.min) * scale, size);

                    var temp = line;
                    if (temp.first.min > temp.second.min)
                    {
                        temp = (temp.second, temp.first);
                    }
                    if (temp.first.max < temp.second.min)
                    {
                        graphics.DrawRectangle(Pens.Black, SATScalarPictureBox.Width / 2 + min * scale, currentHeight, realWidth * scale, size);
                    }
                    currentHeight += size + 5;
                }
            }

            for (int i = 0; i < SATScalarpolygons.Length; i++)
            {
                SATScalarpolygons[i].Draw(graphics);
            }

            SATScalarPictureBox.Image = bitmap;

        }

        private void SATScalarDemoTab_Enter(object sender, EventArgs e)
        {
            SATScalarpolygons = new Polygon[2];
            SATScalarpolygons[0] = new Polygon(new Vertex(100, 100)
                                        , new Vertex(200, 100)
                                        , new Vertex(250, 150)
                                        , new Vertex(200, 200)
                                        , new Vertex(100, 200));
            SATScalarpolygons[0].DefaultPen = Pens.Black;
            SATScalarpolygons[0].DefaultBrush = Brushes.Black;

            SATScalarpolygons[1] = new Polygon(new Vertex(225, 100)
                                        , new Vertex(325, 100)
                                        , new Vertex(325, 200)
                                        , new Vertex(225, 200));
            SATScalarpolygons[1].DefaultPen = Pens.Green;
            SATScalarpolygons[1].DefaultBrush = Brushes.Green;
            DrawSATScalar();
        }


        private void SATScalarPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (SATScalarselectedPolygon == null)
            {
                for (int i = 0; i < SATScalarpolygons.Length; i++)
                {
                    if (SATScalarpolygons[i].Contains(e.Location))
                    {
                        SATScalarselectedPolygon = SATScalarpolygons[i];
                        break;
                    }
                }
            }
            else
            {
                SATScalarselectedPolygon = null;
            }
            DrawSATScalar();
        }

        private void SATScalarPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            SATScalarlms = SATScalarms;
            SATScalarms = e;

            SATScalarselectedPolygon?.TranslateBy(SATScalarms.X - SATScalarlms.X, SATScalarms.Y - SATScalarlms.Y);

            for (int i = 0; i < SATScalarpolygons.Length; i++)
            {
                SATScalarpolygons[i].Pen = SATScalarpolygons[i].Contains(e.Location) ? Pens.Red : SATScalarpolygons[i].DefaultPen;
                SATScalarpolygons[i].Brush = SATScalarpolygons[i].Pen == Pens.Red ? Brushes.Red : SATScalarpolygons[i].DefaultBrush;
            }
            DrawSATScalar();
        }
        private void SATScalarPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            SATScalarselectedPolygon?.RotateBy(e.Delta / 2000f);

            DrawSATScalar();
        }
    }
}
