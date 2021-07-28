using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImplementationsLibrary
{
    public struct Vector2D
    {
        public float X;
        public float Y;
        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public Vector2D(float value)
        {
            this.X = value;
            this.Y = value;
        }
        public static Vector2D One
        {
            get
            {
                return new Vector2D(1f, 1f);
            }
        }
        public static Vector2D Zero
        {
            get
            {
                return new Vector2D(0f, 0f);
            }
        }
        public static Vector2D UnitX
        {
            get
            {
                return new Vector2D(1f, 0f);
            }
        }
        public static Vector2D UnitY
        {
            get
            {
                return new Vector2D(0f, 1f);
            }
        }
        public static Vector2D Add(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        /*public static Vector2D Abs(Vector2D vector)
        {
            return new Vector2D(Math.Abs(vector.X), Math.Abs(vector.Y));
        }*/

        public static float Distance(Vector2D a, Vector2D b)
        {
            double sqDiff = Math.Pow((a.X - b.X), 2) + Math.Pow(a.Y - b.Y, 2);
            return (float)Math.Sqrt(sqDiff);
        }
        public static float DistanceSquared(Vector2D a, Vector2D b) => (float)Math.Pow(Distance(a, b), 2);
        public static float Dot(Vector2D first, Vector2D second)
        {
            return first.X * second.X + first.Y * second.Y;
        }
        public float Length()
        {
            double sqDiff = Math.Pow(X, 2) + Math.Pow(Y, 2);
            return (float)Math.Sqrt(sqDiff);
        }

        public static Vector2D operator +(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X + second.X, first.Y + second.Y);
        }
        public static Vector2D operator -(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X - second.X, first.Y - second.Y);
        }
        public static Vector2D operator -(Vector2D first)
        {
            return new Vector2D(0 - first.X, 0 - first.Y);
        }
        public static Vector2D operator *(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X * second.X, first.Y * second.Y);
        }
        public static Vector2D operator *(float first, Vector2D second)
        {
            return new Vector2D(first * second.X, first * second.Y);
        }
        public static Vector2D operator *(Vector2D first, float second)
        {
            return new Vector2D(first.X * second, first.Y * second);
        }
        public static Vector2D operator /(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X / second.X, first.Y / second.Y);
        }
        public static Vector2D operator /(Vector2D first, float second)
        {
            return new Vector2D(first.X / second, first.Y / second);
        }
    };
}
