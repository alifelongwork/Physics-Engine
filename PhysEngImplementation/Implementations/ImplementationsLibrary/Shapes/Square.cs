using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementationsLibrary;

namespace ImplementationsLibrary
{
    public class Square
    {
        public Matrix3x3MathArray TopLeft { get; private set; }
        public Matrix3x3MathArray TopRight { get; private set; }
        public Matrix3x3MathArray BottomLeft { get; private set; }
        public Matrix3x3MathArray BottomRight { get; private set; }

        public PointF TopLeftPoint => new PointF(TopLeft.X, TopLeft.Y);
        public PointF TopRightPoint => new PointF(TopRight.X, TopRight.Y);
        public PointF BottomLeftPoint => new PointF(BottomLeft.X, BottomLeft.Y);
        public PointF BottomRightPoint => new PointF(BottomRight.X, BottomRight.Y);

        public PointF Centroid
        {
            get
            {
                float averageX = (TopLeft.X + TopRight.X + BottomLeft.X + BottomRight.X) / 4;
                float averageY = (TopLeft.Y + TopRight.Y + BottomLeft.Y + BottomRight.Y) / 4;

                return new PointF(averageX, averageY);
            }
        }

        public Square(PointF topleft, PointF topright, PointF bottomleft, PointF bottomright)
        {
            TopLeft = Matrix3x3MathArray.CreateTranslation(topleft.X, topleft.Y);
            TopRight = Matrix3x3MathArray.CreateTranslation(topright.X, topright.Y);
            BottomLeft = Matrix3x3MathArray.CreateTranslation(bottomleft.X, bottomleft.Y);
            BottomRight = Matrix3x3MathArray.CreateTranslation(bottomright.X, bottomright.Y);
        }

        public void Translate(PointF delta)
        {
            Matrix3x3MathArray transformationMatrix = Matrix3x3MathArray.CreateTranslation(delta.X, delta.Y);

            TopLeft.Transform(transformationMatrix);
            TopRight.Transform(transformationMatrix);
            BottomLeft.Transform(transformationMatrix);
            BottomRight.Transform(transformationMatrix);
        }

        public void Rotate(float delta)
        {
            Matrix3x3MathArray transformationMatrix = Matrix3x3MathArray.CreateRotationZ(delta, Centroid.X, Centroid.Y);

            TopLeft.Transform(transformationMatrix);
            TopRight.Transform(transformationMatrix);
            BottomLeft.Transform(transformationMatrix);
            BottomRight.Transform(transformationMatrix);
        }
    }
}
