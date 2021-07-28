using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace PhysicsLibrary
{
    public struct Vector2D
    {
        public UnitF X { get; set; }
        public UnitF Y { get; set; }

        public float Angle => (float)Math.Atan2(Y.PixelValue, X.PixelValue);
        public Vector2D(float x, float y, bool usingPixel)
             : this(x, y, usingPixel, PhysicsHelper.Instance.ConversionFactor)
        {
        }
        public Vector2D(float x, float y, bool usingPixel, float conversionFactor)
        {
            this.X = new UnitF(x, usingPixel, conversionFactor);
            this.Y = new UnitF(y, usingPixel, conversionFactor);
        }
        public Vector2D(UnitF x, UnitF y)
        {
            this.X = x;
            this.Y = y;
        }
        public Vector2D(UnitF value)
        {
            this.X = value;
            this.Y = value;
        }
        public static Vector2D One => new Vector2D(UnitF.One);
        public static Vector2D Zero => new Vector2D(UnitF.Zero);
        public static Vector2D OneSI => new Vector2D(UnitF.OneSI);
        public static Vector2D ZeroSI => new Vector2D(UnitF.ZeroSI);
        public static Vector2D UnitX => new Vector2D(UnitF.One, UnitF.Zero);
        public static Vector2D UnitY => new Vector2D(UnitF.Zero, UnitF.One);
        public static Vector2D UnitXSI => new Vector2D(UnitF.OneSI, UnitF.ZeroSI);
        public static Vector2D UnitYSI => new Vector2D(UnitF.ZeroSI, UnitF.OneSI);

        public static Vector2D Add(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }
        public static UnitF Distance(Vector2D a, Vector2D b)
        {
            double sqDiff = Math.Pow(a.X.PixelValue - b.X.PixelValue, 2) + Math.Pow(a.Y.PixelValue - b.Y.PixelValue, 2);
            UnitF temp = new UnitF((float)Math.Sqrt(sqDiff), true);
            if (a.X.UsingSI)
            {
                temp.SwitchToOther();
            }
            return temp;
        }
        public static UnitF DistanceSquared(Vector2D a, Vector2D b)
        {
            float sqDiff = (float)(Math.Pow(a.X.PixelValue - b.X.PixelValue, 2) + Math.Pow(a.Y.PixelValue - b.Y.PixelValue, 2));
            UnitF temp = new UnitF(sqDiff, true);
            if (a.X.UsingSI)
            {
                temp.SwitchToOther();
            }
            return temp;
        }
        public static UnitF Dot(Vector2D first, Vector2D second)
        {
            return first.X * second.X + first.Y * second.Y;
        }
        public static Vector2D Lerp(Vector2D value1, Vector2D value2, float amount)
        {
            throw new Exception();  
        }
        public UnitF Length()
        {
            return Distance(Zero, this);
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
            return new Vector2D(0f - first.X, 0f - first.Y, first.X.UsingPixel, first.X.ConversionFactor);
        }
        public static Vector2D operator *(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X * second.X, first.Y * second.Y);
        }
        public static Vector2D operator *(float first, Vector2D second)
        {
            return new Vector2D(first * second.X, first * second.Y, second.X.UsingPixel, second.X.ConversionFactor);
        }
        public static Vector2D operator *(Vector2D first, float second)
        {
            return new Vector2D(first.X * second, first.Y * second, first.X.UsingPixel, first.X.ConversionFactor);
        }
        public static Vector2D operator /(Vector2D first, Vector2D second)
        {
            return new Vector2D(first.X / second.X, first.Y / second.Y);
        }
        public static Vector2D operator /(Vector2D first, float second)
        {
            return new Vector2D(first.X / second, first.Y / second, first.X.UsingPixel, first.X.ConversionFactor);
        }
        public static implicit operator Vector2D(Point point)
        {
            return new Vector2D(point.X, point.Y, true);
        }
        public static implicit operator Vertex(Vector2D point)
        {
            return new Vertex(point.X, point.Y);
        }

        public static bool operator ==(Vector2D first, Vector2D second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        public static bool operator !=(Vector2D first, Vector2D second)
        {
            return !(first == second);
        }
    };
}
