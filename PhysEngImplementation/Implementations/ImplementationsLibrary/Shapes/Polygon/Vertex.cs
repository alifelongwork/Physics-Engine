using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace ImplementationsLibrary
{
    public class Vertex
    {
        private Matrix3x3MathArray matrix { get; set; }

        public float X => matrix.X;
        public float Y => matrix.Y;

        public Vertex(float x, float y)
        {
            matrix = Matrix3x3MathArray.CreateTranslation(x, y);
        }

        public void SetPosition(Vector2D vector) => SetPosition(vector.X, vector.Y);
        public void SetPosition(float x, float y)
        {
            float deltaX = x - X;
            float deltaY = y - Y;
            matrix *= Matrix3x3MathArray.CreateTranslation(deltaX, deltaY);
        }

        public void Translate(Vector2D vector) => Translate(vector.X, vector.Y);
        public void Translate(float x, float y)
        {
            matrix *= Matrix3x3MathArray.CreateTranslation(x, y);
        }

        internal void Rotate(float radians)
        {
            matrix *= Matrix3x3MathArray.CreateRotationZ(radians);
        }

        public Rectangle AsRectangle()
        {
            int size = 10;
            return new Rectangle((int)X - size/2, (int)Y - size/2, size, size);
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex vertex &&
                   EqualityComparer<Matrix3x3MathArray>.Default.Equals(matrix, vertex.matrix) &&
                   X == vertex.X &&
                   Y == vertex.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1468326271;
            hashCode = hashCode * -1521134295 + EqualityComparer<Matrix3x3MathArray>.Default.GetHashCode(matrix);
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static implicit operator Vector2D(Vertex v) => new Vector2D(v.X, v.Y);
        public static implicit operator Point(Vertex v) => new Point((int)v.X, (int)v.Y);
        public static Vertex operator *(Vertex v, float f)
        {
            return new Vertex(v.X * f, v.Y * f);
        }
        public static Vertex operator +(Vertex v1, Vertex v2)
        {
            return new Vertex(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static bool operator ==(Vertex v1, Vertex v2)
        {
            if (Equals(v1, null) && Equals(v2, null))
            {
                return true;
            }
            if (Equals(v1, null) || Equals(v2, null))
            {
                return false;
            }
            return v1.X == v2.X && v1.Y == v2.Y;
        }
        public static bool operator ==(Vector2D v1, Vertex v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }
        public static bool operator !=(Vector2D v1, Vertex v2)
        {
            return !(v1 == v2);
        }
        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return !(v1 == v2);
        }
    }
}
