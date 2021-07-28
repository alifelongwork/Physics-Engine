using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public struct Matrix4x4Math
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;

        public float M21;
        public float M22;
        public float M23;
        public float M24;

        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public Matrix4x4Math(Matrix3x2Math val)
        {
            this.M11 = val.M11;
            this.M12 = val.M12;
            this.M21 = val.M21;
            this.M22 = val.M22;
            this.M41 = val.M31;
            this.M42 = val.M32;

            this.M13 = this.M14 =
                this.M23 = this.M24 =
                this.M31 = this.M32 =
                this.M34 = this.M43 = 0;

            this.M44 = this.M33 = 1;
        }
        public Matrix4x4Math(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;

            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;

            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;

            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        private static readonly Matrix4x4Math _identity = new Matrix4x4Math
        (
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
        );
        public static Matrix4x4Math Identity
        {
            get { return _identity; }
        }

        public static Matrix4x4Math CreateTranslation(Vector3D displacement)
        {
            Matrix4x4Math translation;

            translation.M11 = translation.M22 = translation.M33 = 1.0f;

            translation.M12 = translation.M13 =
                translation.M14 = translation.M21 =
                translation.M23 = translation.M24 =
                translation.M31 = translation.M32 =
                translation.M34 = 0.0f;

            translation.M41 = displacement.X;
            translation.M42 = displacement.Y;
            translation.M43 = displacement.Z;
            translation.M44 = 1.0f;

            return translation;
        }

        public static Matrix4x4Math CreateTranslation(float xDisp, float yDisp, float zDisp)
        {
            Matrix4x4Math translation;

            translation.M11 = translation.M22 = translation.M33 = 1.0f;

            translation.M12 = translation.M13 =
                translation.M14 = translation.M21 =
                translation.M23 = translation.M24 =
                translation.M31 = translation.M32 =
                translation.M34 = 0.0f;

            translation.M41 = xDisp;
            translation.M42 = yDisp;
            translation.M43 = zDisp;
            translation.M44 = 1.0f;

            return translation;
        }

        public static Matrix4x4Math CreateRotationZ(float radians)
        {
            Matrix4x4Math rotate;

            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            /* [c   s   0   0]
             * [-s  c   0   0]
             * [0   0   1   0]
             * [0   0   0   1]*/

            rotate.M11 = rotate.M22 = c;
            rotate.M12 = s;
            rotate.M21 = -s;
            rotate.M33 = rotate.M44 = 1.0f;

            rotate.M13 = rotate.M14 =
                rotate.M23 = rotate.M24 =
                rotate.M31 = rotate.M32 =
                rotate.M34 = rotate.M41 =
                rotate.M42 = rotate.M43 = 0.0f;

            return rotate;
        }

        public static Matrix4x4Math CreateRotationZ(float radians, Vector3D center)
        {
            Matrix4x4Math rotate;

            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);

            float x = center.X * (1 - c) + center.Y * s;
            float y = center.Y * (1 - c) - center.X * s;

            /* [c   s   0   0]
             * [-s  c   0   0]
             * [0   0   1   0]
             * [x   y   0   1]*/

            rotate.M11 = c;
            rotate.M12 = s;
            rotate.M13 = 0.0f;
            rotate.M14 = 0.0f;

            rotate.M21 = -s;
            rotate.M22 = c;
            rotate.M23 = 0.0f;
            rotate.M24 = 0.0f;

            rotate.M31 = 0.0f;
            rotate.M32 = 0.0f;
            rotate.M33 = 1.0f;
            rotate.M34 = 0.0f;

            rotate.M41 = x;
            rotate.M42 = y;
            rotate.M43 = 0.0f;
            rotate.M44 = 1.0f;

            return rotate;
        }

        public float GetDeterminant()
        {
            /* |a b c d|    |f g h|     |e g h|     |e f h|     |e f g|
             * |e f g h| = a|j k l| - b |i k l| + c |i j l| - d |i j k|
             * |i j k l|    |n o p|     |m o p|     |m n p|     |m n o|
             * |m n o p|
             * 
             *   |f g h|
             * a |j k l| = a ( f (k * p - l * o) - g (j * p - l * n) + h (j * o - k * n))
             *   |n o p|
             *  
             *   |e g h|
             * b |i k l| = b ( e ( k * p - l * o) - g ( i * p - l * m) + h ( i * o - k * m))
             *   |m o p|
             *   
             *   |e f h|
             * c |i j l| = c ( e (j * p - l * n) - f ( i * p - l * m) + h ( i * n - j * m))
             *   |m n p|
             *   
             *   
             *   |e f g|
             * d |i j k| = d ( e (j * o - k * n) - f ( i * o - k * m) + g (i * n - j * m))
             *   |m n o|
             *   */

            float a = this.M11, b = this.M12, c = this.M13, d = this.M14;
            float e = this.M21, f = this.M22, g = this.M23, h = this.M24;
            float i = this.M31, j = this.M32, k = this.M33, l = this.M34;
            float m = this.M41, n = this.M42, o = this.M43, p = this.M44;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                    b * (e * kp_lo - g * ip_lm + h * io_km) +
                    c * (e * jp_ln - f * ip_lm + h * in_jm) -
                    d * (e * jo_kn - f * io_km + g * in_jm);
        }

        public static Matrix4x4Math Transpose(Matrix4x4Math matrix)
        {
            Matrix4x4Math transposed;

            transposed.M11 = matrix.M11;
            transposed.M12 = matrix.M21;
            transposed.M13 = matrix.M31;
            transposed.M14 = matrix.M41;

            transposed.M21 = matrix.M12;
            transposed.M22 = matrix.M22;
            transposed.M23 = matrix.M32;
            transposed.M24 = matrix.M42;

            transposed.M31 = matrix.M13;
            transposed.M32 = matrix.M23;
            transposed.M33 = matrix.M33;
            transposed.M34 = matrix.M43;

            transposed.M41 = matrix.M14;
            transposed.M42 = matrix.M24;
            transposed.M43 = matrix.M34;
            transposed.M44 = matrix.M44;

            return transposed;
        }

        public static Matrix4x4Math Add(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            Matrix4x4Math sum;

            sum.M11 = mat1.M11 + mat2.M11;
            sum.M12 = mat1.M12 + mat2.M12;
            sum.M13 = mat1.M13 + mat2.M13;
            sum.M14 = mat1.M14 + mat2.M14;

            sum.M21 = mat1.M21 + mat2.M21;
            sum.M22 = mat1.M22 + mat2.M22;
            sum.M23 = mat1.M23 + mat2.M23;
            sum.M24 = mat1.M24 + mat2.M24;

            sum.M31 = mat1.M31 + mat2.M31;
            sum.M32 = mat1.M32 + mat2.M32;
            sum.M33 = mat1.M33 + mat2.M33;
            sum.M34 = mat1.M34 + mat2.M34;

            sum.M41 = mat1.M41 + mat2.M41;
            sum.M42 = mat1.M42 + mat2.M42;
            sum.M43 = mat1.M43 + mat2.M43;
            sum.M44 = mat1.M44 + mat2.M44;

            return sum;
        }

        public static Matrix4x4Math Subtract(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            Matrix4x4Math diff;

            diff.M11 = mat1.M11 - mat2.M11;
            diff.M12 = mat1.M12 - mat2.M12;
            diff.M13 = mat1.M13 - mat2.M13;
            diff.M14 = mat1.M14 - mat2.M14;

            diff.M21 = mat1.M21 - mat2.M21;
            diff.M22 = mat1.M22 - mat2.M22;
            diff.M23 = mat1.M23 - mat2.M23;
            diff.M24 = mat1.M24 - mat2.M24;

            diff.M31 = mat1.M31 - mat2.M31;
            diff.M32 = mat1.M32 - mat2.M32;
            diff.M33 = mat1.M33 - mat2.M33;
            diff.M34 = mat1.M34 - mat2.M34;

            diff.M41 = mat1.M41 - mat2.M41;
            diff.M42 = mat1.M42 - mat2.M42;
            diff.M43 = mat1.M43 - mat2.M43;
            diff.M44 = mat1.M44 - mat2.M44;

            return diff;
        }

        public static Matrix4x4Math Multiply(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            Matrix4x4Math prod;

            //first row
            prod.M11 = mat1.M11 * mat2.M11 + mat1.M12 * mat2.M21 + mat1.M13 * mat2.M31 + mat1.M14 * mat2.M41;
            prod.M12 = mat1.M11 * mat2.M12 + mat1.M12 * mat2.M22 + mat1.M13 * mat2.M32 + mat1.M14 * mat2.M42;
            prod.M13 = mat1.M11 * mat2.M13 + mat1.M12 * mat2.M23 + mat1.M13 * mat2.M33 + mat1.M14 * mat2.M43;
            prod.M14 = mat1.M11 * mat2.M14 + mat1.M12 * mat2.M24 + mat1.M13 * mat2.M34 + mat1.M14 * mat2.M44;

            //second row
            prod.M21 = mat1.M21 * mat2.M11 + mat1.M22 * mat2.M21 + mat1.M23 * mat2.M31 + mat1.M24 * mat2.M41;
            prod.M22 = mat1.M21 * mat2.M12 + mat1.M22 * mat2.M22 + mat1.M23 * mat2.M32 + mat1.M24 * mat2.M42;
            prod.M23 = mat1.M21 * mat2.M13 + mat1.M22 * mat2.M23 + mat1.M23 * mat2.M33 + mat1.M24 * mat2.M43;
            prod.M24 = mat1.M21 * mat2.M14 + mat1.M22 * mat2.M24 + mat1.M23 * mat2.M34 + mat1.M24 * mat2.M44;

            //third row
            prod.M31 = mat1.M31 * mat2.M11 + mat1.M32 * mat2.M21 + mat1.M33 * mat2.M31 + mat1.M34 * mat2.M41;
            prod.M32 = mat1.M31 * mat2.M12 + mat1.M32 * mat2.M22 + mat1.M33 * mat2.M32 + mat1.M34 * mat2.M42;
            prod.M33 = mat1.M31 * mat2.M13 + mat1.M32 * mat2.M23 + mat1.M33 * mat2.M33 + mat1.M34 * mat2.M43;
            prod.M34 = mat1.M31 * mat2.M14 + mat1.M32 * mat2.M24 + mat1.M33 * mat2.M34 + mat1.M34 * mat2.M44;

            //fourth row
            prod.M41 = mat1.M41 * mat2.M11 + mat1.M42 * mat2.M21 + mat1.M43 * mat2.M31 + mat1.M44 * mat2.M41;
            prod.M42 = mat1.M41 * mat2.M12 + mat1.M42 * mat2.M22 + mat1.M43 * mat2.M32 + mat1.M44 * mat2.M42;
            prod.M43 = mat1.M41 * mat2.M13 + mat1.M42 * mat2.M23 + mat1.M43 * mat2.M33 + mat1.M44 * mat2.M43;
            prod.M44 = mat1.M41 * mat2.M14 + mat1.M42 * mat2.M24 + mat1.M43 * mat2.M34 + mat1.M44 * mat2.M44;

            return prod;
        }

        public static Matrix4x4Math operator -(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            return Subtract(mat1, mat2);
        }

        public static Matrix4x4Math operator +(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            return Add(mat1, mat2);
        }

        public static Matrix4x4Math operator *(Matrix4x4Math mat1, Matrix4x4Math mat2)
        {
            return Multiply(mat1, mat2);
        }
    }
}
