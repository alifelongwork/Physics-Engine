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
        List<Polygon> polygons;
        Pen polygonPen = Pens.Black;
        Brush polygonBrush = Brushes.Black;
        int polygonSelectedIndex = -1;

        void DrawPolygons()
        {
            Bitmap bitmap = new Bitmap(PolygonPictureBox.Width, PolygonPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            foreach (Polygon polygon in polygons)
            {
                polygon.Draw(graphics);
            }

            PolygonPictureBox.Image = bitmap;
        }

        private void PolygonDemoTab_Enter(object sender, EventArgs e)
        {
            polygons = new List<Polygon>();
        }
        private void PolygonAddButton_Click(object sender, EventArgs e)
        {
            polygons.Add(Polygon.CreateRegularPolygon(polygonBrush, polygonPen, (int)PolygonNumberOfSidesUpDown.Value, (int)PolygonRadiusUpDown.Value, new Point(PolygonPictureBox.Width / 2, PolygonPictureBox.Height / 2)));
            PolygonListBox.Items.Add($"{(int)PolygonNumberOfSidesUpDown.Value} Sides");

            DrawPolygons();
        }
        private void PolygonPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (polygonSelectedIndex == -1)
            {
                for (int i = 0; i < polygons.Count; i++)
                {
                    if (polygons[i].Contains(e.X, e.Y))
                    {
                        polygonSelectedIndex = i;
                        PolygonListBox.SelectedIndex = i;

                        break;
                    }
                }
            }
            else
            {
                polygonSelectedIndex = -1;
                PolygonListBox.ClearSelected();
            }
        }

        private void PolygonPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (polygonSelectedIndex != -1)
            {
                polygons[polygonSelectedIndex].TranslateBy(e.X - polygons[polygonSelectedIndex].Centroid.X, e.Y - polygons[polygonSelectedIndex].Centroid.Y);
            }

            DrawPolygons();
        }

        private void PolygonColorPickerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                polygonPen = new Pen(colorDialog.Color);
                polygonBrush = new SolidBrush(colorDialog.Color);
            }

            PolygonColorPickerPanel.BackColor = colorDialog.Color;

            if (polygonSelectedIndex != -1)
            {
                polygons[polygonSelectedIndex].Brush = polygonBrush;
                polygons[polygonSelectedIndex].Pen = polygonPen;
                polygons[polygonSelectedIndex].DefaultBrush = polygonBrush;
                polygons[polygonSelectedIndex].DefaultPen = polygonPen;
            }
        }
        private void PolygonDemoTab_MouseWheel(object sender, MouseEventArgs e)
        {
            if (polygonSelectedIndex != -1)
            {
                polygons[polygonSelectedIndex].RotateBy(e.Delta / 500f);
            }
            DrawPolygons();
        }

        private void PolygonListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            polygonSelectedIndex = PolygonListBox.SelectedIndex;
        }
    }
}
