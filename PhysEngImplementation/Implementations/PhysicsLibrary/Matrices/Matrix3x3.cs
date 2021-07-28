using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsLibrary
{
    public class Matrix3x3
    {
        public UnitF[,] Values;

        public UnitF this[int i, int j]
        {
            get
            {
                return Values[i, j];
            }
        }

        public UnitF X => Values[2, 0];
        public UnitF Y => Values[2, 1];
        public float Theta => (float)Math.Acos(Values[0, 0]);

        public Matrix3x3(UnitF m11, UnitF m12, UnitF m13, UnitF m21, UnitF m22, UnitF m23, UnitF m31, UnitF m32, UnitF m33)
        {
            Values = new UnitF[3, 3];
            Values[0, 0] = m11;
            Values[0, 1] = m12;
            Values[0, 2] = m13;

            Values[1, 0] = m21;
            Values[1, 1] = m22;
            Values[1, 2] = m23;

            Values[2, 0] = m31;
            Values[2, 1] = m32;
            Values[2, 2] = m33;
        }
        public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, bool usingPixel)
        {
            Values = new UnitF[3, 3];
            Values[0, 0] = new UnitF(m11, usingPixel);
            Values[0, 1] = new UnitF(m12, usingPixel);
            Values[0, 2] = new UnitF(m13, usingPixel);

            Values[1, 0] = new UnitF(m21, usingPixel);
            Values[1, 1] = new UnitF(m22, usingPixel);
            Values[1, 2] = new UnitF(m23, usingPixel);

            Values[2, 0] = new UnitF(m31, usingPixel);
            Values[2, 1] = new UnitF(m32, usingPixel);
            Values[2, 2] = new UnitF(m33, usingPixel);
        }
        public Matrix3x3(UnitF[,] values)
        {
            if (values.Length != 3 || values.GetLength(0) != 3)
            {
                throw new ArgumentOutOfRangeException("values");
            }

            Values = values;
        }

        public void Transform(Matrix3x3 transformationMatrix)
        {
            Matrix3x3 newThis = this * transformationMatrix;
            Values[0, 0] = newThis.Values[0, 0];
            Values[0, 1] = newThis.Values[0, 1];
            Values[0, 2] = newThis.Values[0, 2];

            Values[1, 0] = newThis.Values[1, 0];
            Values[1, 1] = newThis.Values[1, 1];
            Values[1, 2] = newThis.Values[1, 2];

            Values[2, 0] = newThis.Values[2, 0];
            Values[2, 1] = newThis.Values[2, 1];
            Values[2, 2] = newThis.Values[2, 2];
        }

        public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right)
        {
            return new Matrix3x3(
                left.Values[0, 0] + right.Values[0, 0],
                left.Values[0, 1] + right.Values[0, 1],
                left.Values[0, 2] + right.Values[0, 2],

                left.Values[1, 0] + right.Values[1, 0],
                left.Values[1, 1] + right.Values[1, 1],
                left.Values[1, 2] + right.Values[1, 2],

                left.Values[2, 0] + right.Values[2, 0],
                left.Values[2, 1] + right.Values[2, 1],
                left.Values[2, 2] + right.Values[2, 2]);
        }
        public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right)
        {
            return new Matrix3x3(
                left.Values[0, 0] - right.Values[0, 0],
                left.Values[0, 1] - right.Values[0, 1],
                left.Values[0, 2] - right.Values[0, 2],

                left.Values[1, 0] - right.Values[1, 0],
                left.Values[1, 1] - right.Values[1, 1],
                left.Values[1, 2] - right.Values[1, 2],

                left.Values[2, 0] - right.Values[2, 0],
                left.Values[2, 1] - right.Values[2, 1],
                left.Values[2, 2] - right.Values[2, 2]);
        }
        public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
        {

            UnitF m11 = left.Values[0, 0] * right.Values[0, 0] + left.Values[0, 1] * right.Values[1, 0] + left.Values[0, 2] * right.Values[2, 0];
            UnitF m12 = left.Values[0, 0] * right.Values[0, 1] + left.Values[0, 1] * right.Values[1, 1] + left.Values[0, 2] * right.Values[2, 1];
            UnitF m13 = left.Values[0, 0] * right.Values[0, 2] + left.Values[0, 1] * right.Values[1, 2] + left.Values[0, 2] * right.Values[2, 2];

            UnitF m21 = left.Values[1, 0] * right.Values[0, 0] + left.Values[1, 1] * right.Values[1, 0] + left.Values[1, 2] * right.Values[2, 0];
            UnitF m23 = left.Values[1, 0] * right.Values[0, 2] + left.Values[1, 1] * right.Values[1, 2] + left.Values[1, 2] * right.Values[2, 2];
            UnitF m22 = left.Values[1, 0] * right.Values[0, 1] + left.Values[1, 1] * right.Values[1, 1] + left.Values[1, 2] * right.Values[2, 1];

            UnitF m31 = left.Values[2, 0] * right.Values[0, 0] + left.Values[2, 1] * right.Values[1, 0] + left.Values[2, 2] * right.Values[2, 0];
            UnitF m32 = left.Values[2, 0] * right.Values[0, 1] + left.Values[2, 1] * right.Values[1, 1] + left.Values[2, 2] * right.Values[2, 1];
            UnitF m33 = left.Values[2, 0] * right.Values[0, 2] + left.Values[2, 1] * right.Values[1, 2] + left.Values[2, 2] * right.Values[2, 2];

            return new Matrix3x3(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }
        public static Matrix3x3 operator *(Matrix3x3 left, UnitF right)
        {
            UnitF m11 = left.Values[0, 0] * right;
            UnitF m12 = left.Values[0, 1] * right;
            UnitF m13 = left.Values[0, 2] * right;

            UnitF m21 = left.Values[1, 0] * right;
            UnitF m22 = left.Values[1, 1] * right;
            UnitF m23 = left.Values[1, 2] * right;

            UnitF m31 = left.Values[2, 0] * right;
            UnitF m32 = left.Values[2, 1] * right;
            UnitF m33 = left.Values[2, 2] * right;

            return new Matrix3x3(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }

        public static Matrix3x3 Identity => new Matrix3x3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, true);

        public static Matrix3x3 CreateTranslation(UnitF x, UnitF y)
        {
            return new Matrix3x3(UnitF.One, UnitF.Zero, UnitF.Zero, UnitF.Zero, UnitF.One, UnitF.Zero, x, y, UnitF.One);
        }
        public static Matrix3x3 CreateRotationZ(float radians)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            /* [c   s   0]
             * [-s  c   0]
             * [0   0   1]*/

            UnitF m11 = new UnitF(c);
            UnitF m12 = new UnitF(s);
            UnitF m22 = new UnitF(c);
            UnitF m21 = new UnitF(-s);

            return new Matrix3x3(m11, m12, UnitF.Zero, m21, m22, UnitF.Zero, UnitF.Zero, UnitF.Zero, UnitF.One);
        }
        public static Matrix3x3 CreateRotationZ(float radians, UnitF centerX, UnitF centerY)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            UnitF x = (UnitF)(centerX * (1 - c) + centerY * s);
            UnitF y = (UnitF)(centerY * (1 - c) - centerX * s);

            /* [c   s   0]
             * [-s  c   0]
             * [x   y   1] */

            UnitF m11 = new UnitF(c);
            UnitF m12 = new UnitF(s);
            UnitF m22 = new UnitF(c);
            UnitF m21 = new UnitF(-s);

            return new Matrix3x3(m11, m12, UnitF.Zero, m21, m22, UnitF.Zero, UnitF.Zero, UnitF.Zero, UnitF.One);
        }
    }
}
