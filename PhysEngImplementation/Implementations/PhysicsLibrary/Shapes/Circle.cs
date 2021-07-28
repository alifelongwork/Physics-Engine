using System;
using System.Drawing;

namespace PhysicsLibrary
{
    public class Circle
    {
        protected Matrix3x3 matrix { get; set; }
        public UnitF Radius { get; protected set; }
        public Vector2D Position => new Vector2D(matrix.X, matrix.Y);
        public UnitF X => matrix.X;
        public UnitF Y => matrix.Y;

        public System.Drawing.Rectangle Rectangle => new System.Drawing.Rectangle((int)(X - Radius), (int)(Y - Radius), 2 * (int)Radius, 2 * (int)Radius);

        //public Brush Color = Brushes.Black;

        public Circle(UnitF x, UnitF y, UnitF radius)
        {
            matrix = Matrix3x3.CreateTranslation(x, y);
            Radius = radius;
        }
        public Circle(Vector2D position, UnitF radius)
            : this(position.X, position.Y, radius) { }

        public void TranslateTo(Vector2D newPoint) => TranslateTo(newPoint.X, newPoint.Y);
        public void TranslateTo(UnitF newX, UnitF newY)
        {
            matrix.Transform(Matrix3x3.CreateTranslation(newX - X, newY - Y));
        }
        /*
        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Color, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
        */
    }

    public class MovingCircle : Circle
    {
        public UnitF Speed { get; set; }
        public float Direction { get; set; }

        public MovingCircle(Vector2D position, UnitF radius, UnitF speed, float direction)
            : this(position.X, position.Y, radius, speed, direction)
        {

        }

        public MovingCircle(UnitF x, UnitF y, UnitF radius, UnitF speed, float direction)
            : base(x, y, radius)
        {
            Speed = speed;
            Direction = direction;
        }

        public void Update()
        {
            matrix *= Matrix3x3.CreateTranslation((UnitF)(Speed * (float)Math.Cos(Direction)), (UnitF)(Speed * (float)Math.Sin(Direction)));
        }

    }
    public class BouncingCircle : MovingCircle
    {
        public BouncingCircle(Vector2D position, UnitF radius, UnitF speed, float direction) : base(position, radius, speed, direction)
        {
        }

        public BouncingCircle(UnitF x, UnitF y, UnitF radius, UnitF speed, float direction) : base(x, y, radius, speed, direction)
        {
        }

        public void Update(UnitF maxX, UnitF maxY)
        {
            if (X - Radius < 0 || X + Radius > maxX)
            {
                Direction = (float)(Math.PI - Direction);
            }

            if (Y - Radius < 0 || Y + Radius > maxY)
            {
                Direction = -Direction;
            }

            base.Update();
        }
    }
}
