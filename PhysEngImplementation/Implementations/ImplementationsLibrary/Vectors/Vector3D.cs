using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImplementationsLibrary
{
    public struct Vector3D
    {
        public float X;
        public float Y;
        public float Z;
        public Vector3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public static Vector3D One
        {
            get
            {
                return new Vector3D(1f, 1f, 1f);
            }
        }
        public static Vector3D Zero
        {
            get
            {
                return new Vector3D(0f, 0f, 0f);
            }
        }
        public static Vector3D UnitX
        {
            get
            {
                return new Vector3D(1f, 0f, 0f);
            }
        }
        public static Vector3D UnitY
        {
            get
            {
                return new Vector3D(0f, 1f, 0f);
            }
        }
        public static Vector3D UnitZ
        {
            get
            {
                return new Vector3D(0f, 0f, 1f);
            }
        }
        public static Vector3D Add(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static float Distance(Vector3D a, Vector3D b)
        {
            double sqDiff = Math.Pow((a.X - b.X), 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2);
            return (float)Math.Sqrt(sqDiff);
        }
        public static float DistanceSquared(Vector3D a, Vector3D b) => (float)Math.Pow(Distance(a, b), 2);
        public static float Dot(Vector3D first, Vector3D second)
        {
            return first.X * second.X + first.Y * second.Y + first.X * second.Z;
        }
        public float Length()
        {
            double sqDiff = Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2);
            return (float)Math.Sqrt(sqDiff);
        }

        public static Vector3D operator +(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }
        public static Vector3D operator -(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
        }
        public static Vector3D operator -(Vector3D first)
        {
            return new Vector3D(0 - first.X, 0 - first.Y, 0 - first.Z);
        }
        public static Vector3D operator *(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.X * second.X, first.Y * second.Y, first.Z * second.Z);
        }
        public static Vector3D operator *(float first, Vector3D second)
        {
            return new Vector3D(first * second.X, first * second.Y, first * second.Z);
        }
        public static Vector3D operator *(Vector3D first, float second)
        {
            return new Vector3D(first.X * second, first.Y * second, first.Z * second);
        }
        public static Vector3D operator /(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.X / second.X, first.Y / second.Y, first.Z / second.Z);
        }
        public static Vector3D operator /(Vector3D first, float second)
        {
            return new Vector3D(first.X / second, first.Y / second, first.Z / second);
        }
    };
}
