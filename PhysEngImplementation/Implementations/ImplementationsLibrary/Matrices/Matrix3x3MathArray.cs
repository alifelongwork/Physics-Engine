using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class Matrix3x3MathArray
    {
        public float[,] Values;

        public float this[int i, int j]
        {
            get
            {
                return Values[i, j];
            }
        }

        public float X => Values[2, 0];
        public float Y => Values[2, 1];
        public float Theta => (float)(Math.Acos(Values[0, 0]));

        public Matrix3x3MathArray(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            Values = new float[3, 3];
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
        public Matrix3x3MathArray(float[,] values)
        {
            if (values.Length != 3 || values.GetLength(0) != 3)
            {
                throw new ArgumentOutOfRangeException("values");
            }

            Values = values;
        }

        public void Transform(Matrix3x3MathArray transformationMatrix)
        {
            Matrix3x3MathArray newThis = this * transformationMatrix;
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

        public static Matrix3x3MathArray operator +(Matrix3x3MathArray left, Matrix3x3MathArray right)
        {
            return new Matrix3x3MathArray(
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
        public static Matrix3x3MathArray operator -(Matrix3x3MathArray left, Matrix3x3MathArray right)
        {
            return new Matrix3x3MathArray(
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
        public static Matrix3x3MathArray operator *(Matrix3x3MathArray left, Matrix3x3MathArray right)
        {

            float m11 = left.Values[0, 0] * right.Values[0, 0] + left.Values[0, 1] * right.Values[1, 0] + left.Values[0, 2] * right.Values[2, 0];
            float m12 = left.Values[0, 0] * right.Values[0, 1] + left.Values[0, 1] * right.Values[1, 1] + left.Values[0, 2] * right.Values[2, 1];
            float m13 = left.Values[0, 0] * right.Values[0, 2] + left.Values[0, 1] * right.Values[1, 2] + left.Values[0, 2] * right.Values[2, 2];

            float m21 = left.Values[1, 0] * right.Values[0, 0] + left.Values[1, 1] * right.Values[1, 0] + left.Values[1, 2] * right.Values[2, 0];
            float m23 = left.Values[1, 0] * right.Values[0, 2] + left.Values[1, 1] * right.Values[1, 2] + left.Values[1, 2] * right.Values[2, 2];
            float m22 = left.Values[1, 0] * right.Values[0, 1] + left.Values[1, 1] * right.Values[1, 1] + left.Values[1, 2] * right.Values[2, 1];

            float m31 = left.Values[2, 0] * right.Values[0, 0] + left.Values[2, 1] * right.Values[1, 0] + left.Values[2, 2] * right.Values[2, 0];
            float m32 = left.Values[2, 0] * right.Values[0, 1] + left.Values[2, 1] * right.Values[1, 1] + left.Values[2, 2] * right.Values[2, 1];
            float m33 = left.Values[2, 0] * right.Values[0, 2] + left.Values[2, 1] * right.Values[1, 2] + left.Values[2, 2] * right.Values[2, 2];

            return new Matrix3x3MathArray(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }
        public static Matrix3x3MathArray operator *(Matrix3x3MathArray left, float right)
        {
            float m11 = left.Values[0, 0] * right;
            float m12 = left.Values[0, 1] * right;
            float m13 = left.Values[0, 2] * right;

            float m21 = left.Values[1, 0] * right;
            float m22 = left.Values[1, 1] * right;
            float m23 = left.Values[1, 2] * right;

            float m31 = left.Values[2, 0] * right;
            float m32 = left.Values[2, 1] * right;
            float m33 = left.Values[2, 2] * right;

            return new Matrix3x3MathArray(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }

        public static Matrix3x3MathArray Identity => new Matrix3x3MathArray(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public static Matrix3x3MathArray CreateTranslation(float x, float y)
        {
            return new Matrix3x3MathArray(1, 0, 0, 0, 1, 0, x, y, 1);
        }
        public static Matrix3x3MathArray CreateRotationZ(float radians)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            /* [c   s   0]
             * [-s  c   0]
             * [0   0   1]*/

            float m11 = c;
            float m12 = s;
            float m22 = c;
            float m21 = -s;

            return new Matrix3x3MathArray(m11, m12, 0, m21, m22, 0, 0, 0, 1);
        }
        public static Matrix3x3MathArray CreateRotationZ(float radians, float centerX, float centerY)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            float x = centerX * (1 - c) + centerY * s;
            float y = centerY * (1 - c) - centerX * s;

            /* [c   s   0]
             * [-s  c   0]
             * [x   y   1] */

            float m11 = c;
            float m12 = s;
            float m22 = c;
            float m21 = -s;

            return new Matrix3x3MathArray(m11, m12, 0, m21, m22, 0, x, y, 1);
        }
    }
}
