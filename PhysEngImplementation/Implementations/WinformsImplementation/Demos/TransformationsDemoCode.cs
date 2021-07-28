using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementationsLibrary;

namespace WinformsImplementation
{
    public partial class Form1
    {
        Square square = new Square(new PointF(100, 100), new PointF(200, 100), new PointF(100, 200), new PointF(200, 200));
        Square previous;

        void DrawVisualization()
        {
            //visualizer.CreateGraphics();
            Bitmap bitmap = new Bitmap(transformationsPictureBox.Width, transformationsPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            if (showPreviousCheckBox.Checked)
            {
                Pen redPen = new Pen(Brushes.Red);
                graphics.DrawLine(redPen, previous.TopLeftPoint, previous.TopRightPoint);
                graphics.DrawLine(redPen, previous.TopRightPoint, previous.BottomRightPoint);
                graphics.DrawLine(redPen, previous.BottomRightPoint, previous.BottomLeftPoint);
                graphics.DrawLine(redPen, previous.BottomLeftPoint, previous.TopLeftPoint);
            }

            Pen blackPen = new Pen(Brushes.Black);

            graphics.DrawLine(blackPen, square.TopLeftPoint, square.TopRightPoint);
            graphics.DrawLine(blackPen, square.TopRightPoint, square.BottomRightPoint);
            graphics.DrawLine(blackPen, square.BottomRightPoint, square.BottomLeftPoint);
            graphics.DrawLine(blackPen, square.BottomLeftPoint, square.TopLeftPoint);

            transformationsPictureBox.Image = bitmap;

        }

        //doesn't work
        void ShowTopLeftText()
        {
            // {square.TopLeft[0, 1]} {square.TopLeft[0, 2]} │";
            // {square.TopLeft[1, 1]} {square.TopLeft[1, 2]} │";
            // {square.TopLeft[2, 1]} {square.TopLeft[2, 2]} │";

            string line2 = $"│ {square.TopLeft[0, 0]} ";
            string line3 = $"│ {square.TopLeft[1, 0]} ";
            string line4 = $"│ {square.TopLeft[2, 0]} ";

            int maxLength = 0;

            maxLength = line2.Length > line3.Length ? line2.Length : line3.Length;
            maxLength = maxLength > line4.Length ? maxLength : line4.Length;

            for (int i = line2.Length; i < maxLength; i++)
            {
                line2 += "  ";
            }
            line2 += $"{square.TopLeft[0, 1]} ";

            for (int i = line3.Length; i < maxLength; i++)
            {
                line3 += "  ";
            }
            line3 += $"{square.TopLeft[1, 1]} ";

            for (int i = line4.Length; i < maxLength; i++)
            {
                line4 += "  ";
            }
            line4 += $"{square.TopLeft[2, 1]} ";

            maxLength = line2.Length > line3.Length ? line2.Length : line3.Length;
            maxLength = maxLength > line4.Length ? maxLength : line4.Length;

            for (int i = line2.Length; i < maxLength; i++)
            {
                line2 += "  ";
            }
            line2 += $"{square.TopLeft[0, 2]} |";

            for (int i = line3.Length; i < maxLength; i++)
            {
                line3 += "  ";
            }
            line3 += $"{square.TopLeft[1, 2]} |";

            for (int i = line4.Length; i < maxLength; i++)
            {
                line4 += "  ";
            }
            line4 += $"{square.TopLeft[2, 2]} |";

            maxLength = line2.Length > line3.Length ? line2.Length : line3.Length;
            maxLength = maxLength > line4.Length ? maxLength : line4.Length;

            string line1 = "┌";
            string line5 = "└";

            for (int i = 0; i < maxLength - 1; i++)
            {
                line1 += " ";
                line5 += " ";
            }

            line1 += '┐';
            line5 += '┘';

            MatrixValuesLabel.Text = $"Top Left Corner: \n{line1}\n{line2}\n{line3}\n{line4}\n{line5}";
        }

        private void ApplyTransformation_Click(object sender, EventArgs e)
        {
            previous = new Square(square.TopLeftPoint, square.TopRightPoint, square.BottomLeftPoint, square.BottomRightPoint);

            square.Translate(new PointF((float)deltaXUpDown.Value, (float)deltaYUpDown.Value));
            square.Rotate((float)rotationUpDown.Value);

            DrawVisualization();
        }


        private void TransformationsDemoTab_Enter(object sender, EventArgs e)
        {
            DrawVisualization();
        }
    }
}
