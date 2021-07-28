using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class Matrix3x3Math
    {
        public float M11;
        public float M12;
        public float M13;

        public float M21;
        public float M22;
        public float M23;

        public float M31;
        public float M32;
        public float M33;

        public float X => M31;
        public float Y => M32;
        public float Theta => (float)Math.Acos(M11);

        public Matrix3x3Math(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public void Transform(Matrix3x3Math transformationMatrix)
        {
            Matrix3x3Math newThis = this * transformationMatrix;
            M11 = newThis.M11;
            M12 = newThis.M11;
            M13 = newThis.M11;

            M21 = newThis.M21;
            M22 = newThis.M22;
            M23 = newThis.M23;

            M31 = newThis.M31;
            M32 = newThis.M32;
            M33 = newThis.M33;
        }

        public static Matrix3x3Math operator +(Matrix3x3Math left, Matrix3x3Math right)
        {
            return new Matrix3x3Math(
                left.M11 + right.M11,
                left.M12 + right.M12,
                left.M13 + right.M13,
                left.M21 + right.M21,
                left.M22 + right.M22,
                left.M23 + right.M23,
                left.M31 + right.M31,
                left.M32 + right.M32,
                left.M33 + right.M33);
        }
        public static Matrix3x3Math operator -(Matrix3x3Math left, Matrix3x3Math right)
        {
            return new Matrix3x3Math(
                left.M11 - right.M11,
                left.M12 - right.M12,
                left.M13 - right.M13,
                left.M21 - right.M21,
                left.M22 - right.M22,
                left.M23 - right.M23,
                left.M31 - right.M31,
                left.M32 - right.M32,
                left.M33 - right.M33);
        }
        public static Matrix3x3Math operator *(Matrix3x3Math left, Matrix3x3Math right)
        {
            float m11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31;
            float m12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32;
            float m13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33;

            float m21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31;
            float m22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32;
            float m23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33;

            float m31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31;
            float m32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32;
            float m33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33;

            return new Matrix3x3Math(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }
        public static Matrix3x3Math operator *(Matrix3x3Math left, float right)
        {
            float m11 = left.M11 * right + left.M12 * right + left.M13 * right;
            float m12 = left.M11 * right + left.M12 * right + left.M13 * right;
            float m13 = left.M11 * right + left.M12 * right + left.M13 * right;

            float m21 = left.M21 * right + left.M22 * right + left.M23 * right;
            float m22 = left.M21 * right + left.M22 * right + left.M23 * right;
            float m23 = left.M21 * right + left.M22 * right + left.M23 * right;

            float m31 = left.M31 * right + left.M32 * right + left.M33 * right;
            float m32 = left.M31 * right + left.M32 * right + left.M33 * right;
            float m33 = left.M31 * right + left.M32 * right + left.M33 * right;

            return new Matrix3x3Math(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }

        public static Matrix3x3Math Identity => new Matrix3x3Math(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public static Matrix3x3Math CreateTranslation(float x, float y)
        {
            return new Matrix3x3Math(1, 0, 0, 0, 1, 0, x, y, 1);
        }
        public static Matrix3x3Math CreateRotationZ(float radians)
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

            return new Matrix3x3Math(m11, m12, 0, m21, m22, 0, 0, 0, 1);
        }
        public static Matrix3x3Math CreateRotationZ(float radians, float centerX, float centerY)
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

            return new Matrix3x3Math(m11, m12, 0, m21, m22, 0, x, y, 1);
        }
    }
}
