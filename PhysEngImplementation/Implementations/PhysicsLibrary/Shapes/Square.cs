using System.Drawing;

namespace PhysicsLibrary
{
    public class Square
    {
        public Matrix3x3 TopLeft { get; private set; }
        public Matrix3x3 TopRight { get; private set; }
        public Matrix3x3 BottomLeft { get; private set; }
        public Matrix3x3 BottomRight { get; private set; }

        public Vector2D TopLeftPoint => new Vector2D(TopLeft.X, TopLeft.Y);
        public Vector2D TopRightPoint => new Vector2D(TopRight.X, TopRight.Y);
        public Vector2D BottomLeftPoint => new Vector2D(BottomLeft.X, BottomLeft.Y);
        public Vector2D BottomRightPoint => new Vector2D(BottomRight.X, BottomRight.Y);

        public Vector2D Centroid
        {
            get
            {
                UnitF averageX = (UnitF)((TopLeft.X + TopRight.X + BottomLeft.X + BottomRight.X) / 4);
                UnitF averageY = (UnitF)((TopLeft.Y + TopRight.Y + BottomLeft.Y + BottomRight.Y) / 4);

                return new Vector2D(averageX, averageY);
            }
        }

        public Square(Vector2D topleft, Vector2D topright, Vector2D bottomleft, Vector2D bottomright)
        {
            TopLeft = Matrix3x3.CreateTranslation(topleft.X, topleft.Y);
            TopRight = Matrix3x3.CreateTranslation(topright.X, topright.Y);
            BottomLeft = Matrix3x3.CreateTranslation(bottomleft.X, bottomleft.Y);
            BottomRight = Matrix3x3.CreateTranslation(bottomright.X, bottomright.Y);
        }

        public void Translate(Vector2D delta)
        {
            Matrix3x3 transformationMatrix = Matrix3x3.CreateTranslation(delta.X, delta.Y);

            TopLeft.Transform(transformationMatrix);
            TopRight.Transform(transformationMatrix);
            BottomLeft.Transform(transformationMatrix);
            BottomRight.Transform(transformationMatrix);
        }

        public void Rotate(float delta)
        {
            Matrix3x3 transformationMatrix = Matrix3x3.CreateRotationZ(delta, Centroid.X, Centroid.Y);

            TopLeft.Transform(transformationMatrix);
            TopRight.Transform(transformationMatrix);
            BottomLeft.Transform(transformationMatrix);
            BottomRight.Transform(transformationMatrix);
        }
    }
}
