using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class Circle : IHasRectangle
    {
        protected Matrix3x3MathArray matrix { get; set; }
        public float Radius { get; protected set; }
        public Vector2D Position => new Vector2D(matrix.X, matrix.Y);
        public float X => matrix.X;
        public float Y => matrix.Y;

        public Rectangle Rectangle => new Rectangle((int)(X - Radius), (int)(Y - Radius), 2 * (int)Radius, 2 * (int)Radius);

        public Brush Color = Brushes.Black;

        public Circle(float x, float y, float radius)
        {
            matrix = Matrix3x3MathArray.CreateTranslation(x, y);
            Radius = radius;
        }
        public Circle(Vector2D position, float radius)
            : this(position.X, position.Y, radius) { }

        public void TranslateTo(Vector2D newPoint) => TranslateTo(newPoint.X, newPoint.Y);
        public void TranslateTo(float newX, float newY)
        {
            matrix.Transform(Matrix3x3MathArray.CreateTranslation(newX - X, newY - Y));
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Color, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
    }

    public class MovingCircle : Circle
    {
        public float Speed { get; set; }
        public float Direction { get; set; }

        public MovingCircle(Vector2D position, float radius, float speed, float direction)
            : this(position.X, position.Y, radius, speed, direction)
        {

        }

        public MovingCircle(float x, float y, float radius, float speed, float direction)
            : base(x, y, radius)
        {
            Speed = speed;
            Direction = direction;
        }

        public void Update()
        {
            matrix *= Matrix3x3MathArray.CreateTranslation((float)(Speed * Math.Cos(Direction)), (float)(Speed * Math.Sin(Direction)));
        }

    }
    public class BouncingCircle : MovingCircle
    {
        public BouncingCircle(Vector2D position, float radius, float speed, float direction) : base(position, radius, speed, direction)
        {
        }

        public BouncingCircle(float x, float y, float radius, float speed, float direction) : base(x, y, radius, speed, direction)
        {
        }

        public void Update(int maxX, int maxY)
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
