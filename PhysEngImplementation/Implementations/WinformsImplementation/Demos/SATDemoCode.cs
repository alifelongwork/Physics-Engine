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
        MouseEventArgs SATms;
        MouseEventArgs SATlms;
        Polygon[] SATpolygons;
        Polygon SATselectedPolygon;


        public void SwitchAABB()
        {
            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].ShowAABB = !SATpolygons[i].ShowAABB;
            }
        }

        public void SwitchNormals()
        {
            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].ShowNormals = !SATpolygons[i].ShowNormals;
            }
        }

        public void SwitchSAT()
        {
            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].ShowSAT = !SATpolygons[i].ShowSAT;
            }
        }

        public void SetEdgeToFocusOn(int polygon, int edge)
        {
            for (int i = 0; i < SATpolygons.Length; i++)
            {
                if (i != polygon)
                {
                    SATpolygons[i].EdgeToFocusOn = -1;
                }
                else
                {
                    SATpolygons[i].EdgeToFocusOn = edge;
                }
            }
        }

        void DrawSAT()
        {
            Bitmap bitmap = new Bitmap(SATDemoPictureBox.Width, SATDemoPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            if (SATselectedPolygon != null)
            {
                _ = SATpolygons[0].SeparatingAxisTheoremWithSimulation(SATpolygons[1]);

                if (SATpolygons[0].SeparatingAxisTheoremScalar(SATpolygons[1]))
                {
                    SATpolygons[0].Pen = Pens.Blue;
                    SATpolygons[1].Pen = Pens.Blue;
                    SATpolygons[0].Brush = Brushes.Blue;
                    SATpolygons[1].Brush = Brushes.Blue;
                }
                else
                {
                    SATpolygons[0].Brush = SATpolygons[0].DefaultBrush;
                    SATpolygons[1].Brush = SATpolygons[1].DefaultBrush;
                }
            }

            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].Draw(graphics);
            }


            SATDemoPictureBox.Image = bitmap;
        }

        private void SATDemoTab_Enter(object sender, EventArgs e)
        {
            SATDemoPictureBox.MouseWheel += SATDemoPictureBox_MouseWheel;

            SATpolygons = new Polygon[2];
            SATpolygons[0] = new Polygon(new Vertex(100, 100)
                                        , new Vertex(200, 100)
                                        , new Vertex(250, 150)
                                        , new Vertex(200, 200)
                                        , new Vertex(100, 200));
            SATpolygons[0].DefaultPen = Pens.Black;
            SATpolygons[0].DefaultBrush = Brushes.Black;

            SATpolygons[1] = new Polygon(new Vertex(225, 100)
                                        , new Vertex(325, 100)
                                        , new Vertex(325, 200)
                                        , new Vertex(225, 200));
            SATpolygons[1].DefaultPen = Pens.Green;
            SATpolygons[1].DefaultBrush = Brushes.Green;
        }

        private void SATDemoPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            SATlms = SATms;
            SATms = e;

            SATselectedPolygon?.TranslateBy(new Vector2D(SATms.X - SATlms.X, SATms.Y - SATlms.Y));

            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].Pen = SATpolygons[i].Contains(SATms.Location) ? Pens.Red : SATpolygons[i].DefaultPen;
                SATpolygons[i].Brush = SATpolygons[i].Pen == Pens.Red ? Brushes.Red : SATpolygons[i].DefaultBrush;
            }

            DrawSAT();
        }

        private void SATDemoPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (SATselectedPolygon != null)
            {
                SATselectedPolygon.RotateBy(e.Delta / 2000f);
            }
            DrawSAT();
        }

        private void SATDemoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (SATselectedPolygon == null)
            {
                for (int i = 0; i < SATpolygons.Length; i++)
                {
                    if (SATpolygons[i].Contains(SATms.Location))
                    {
                        SATselectedPolygon = SATpolygons[i];
                        break;
                    }
                }
            }
            else
            {
                SATselectedPolygon = null;
            }

        }


        private void ShowSATCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SwitchSAT();
            DrawSAT();
        }

        private void ShowNormalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SwitchNormals();
            DrawSAT();
        }

        private void ShowAABBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SwitchAABB();
            DrawSAT();
        }

        private void FocusOnEdgeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
            label2.Visible = !label2.Visible;
            PolygonToFocusOn.Visible = !PolygonToFocusOn.Visible;
            EdgeToFocusOn.Visible = !EdgeToFocusOn.Visible;

            PolygonToFocusOn.Maximum = SATpolygons.Length - 1;
            EdgeToFocusOn.Maximum = SATpolygons[(int)PolygonToFocusOn.Value].Vertices.Length - 1;
            PolygonToFocusOn.Value = 0;
            EdgeToFocusOn.Value = 0;

            for (int i = 0; i < SATpolygons.Length; i++)
            {
                SATpolygons[i].ShouldFocus = !SATpolygons[i].ShouldFocus;
            }

            SetEdgeToFocusOn((int)PolygonToFocusOn.Value, (int)EdgeToFocusOn.Value);
            DrawSAT();
        }

        private void PolygonToFocusOn_ValueChanged(object sender, EventArgs e)
        {
            EdgeToFocusOn.Maximum = SATpolygons[(int)PolygonToFocusOn.Value].Vertices.Length - 1;
            EdgeToFocusOn.Value = 0;
            SetEdgeToFocusOn((int)PolygonToFocusOn.Value, (int)EdgeToFocusOn.Value);

            DrawSAT();
        }

        private void EdgeToFocusOn_ValueChanged(object sender, EventArgs e)
        {
            SetEdgeToFocusOn((int)PolygonToFocusOn.Value, (int)EdgeToFocusOn.Value);
            DrawSAT();
        }
    }
}
