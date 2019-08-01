using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
                      float m32, float m33, float m34, float m41, float m42, float m43, float m44)
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

        private static Matrix4x4 identity = new Matrix4x4(1f, 0f, 0f, 0f,
                                                    0f, 1f, 0f, 0f,
                                                    0f, 0f, 1f, 0f,
                                                    0f, 0f, 0f, 1f);

        public static Matrix4x4 Identity
        {
            get { return identity; }
        }

        // required for OpenGL 2.0 projection Matrix4x4 stuff
        public static float[] ToFloatArray(Matrix4x4 mat)
        {
            float[] matarray = {
                                    mat.M11, mat.M12, mat.M13, mat.M14,
                                    mat.M21, mat.M22, mat.M23, mat.M24,
                                    mat.M31, mat.M32, mat.M33, mat.M34,
                                    mat.M41, mat.M42, mat.M43, mat.M44
                                };
            return matarray;
        }

        public static Matrix4x4 Add(Matrix4x4 Matrix4x41, Matrix4x4 Matrix4x42)
        {
            Matrix4x41.M11 += Matrix4x42.M11;
            Matrix4x41.M12 += Matrix4x42.M12;
            Matrix4x41.M13 += Matrix4x42.M13;
            Matrix4x41.M14 += Matrix4x42.M14;
            Matrix4x41.M21 += Matrix4x42.M21;
            Matrix4x41.M22 += Matrix4x42.M22;
            Matrix4x41.M23 += Matrix4x42.M23;
            Matrix4x41.M24 += Matrix4x42.M24;
            Matrix4x41.M31 += Matrix4x42.M31;
            Matrix4x41.M32 += Matrix4x42.M32;
            Matrix4x41.M33 += Matrix4x42.M33;
            Matrix4x41.M34 += Matrix4x42.M34;
            Matrix4x41.M41 += Matrix4x42.M41;
            Matrix4x41.M42 += Matrix4x42.M42;
            Matrix4x41.M43 += Matrix4x42.M43;
            Matrix4x41.M44 += Matrix4x42.M44;
            return Matrix4x41;
        }


        public static void Add(ref Matrix4x4 Matrix4x41, ref Matrix4x4 Matrix4x42, out Matrix4x4 result)
        {
            result.M11 = Matrix4x41.M11 + Matrix4x42.M11;
            result.M12 = Matrix4x41.M12 + Matrix4x42.M12;
            result.M13 = Matrix4x41.M13 + Matrix4x42.M13;
            result.M14 = Matrix4x41.M14 + Matrix4x42.M14;
            result.M21 = Matrix4x41.M21 + Matrix4x42.M21;
            result.M22 = Matrix4x41.M22 + Matrix4x42.M22;
            result.M23 = Matrix4x41.M23 + Matrix4x42.M23;
            result.M24 = Matrix4x41.M24 + Matrix4x42.M24;
            result.M31 = Matrix4x41.M31 + Matrix4x42.M31;
            result.M32 = Matrix4x41.M32 + Matrix4x42.M32;
            result.M33 = Matrix4x41.M33 + Matrix4x42.M33;
            result.M34 = Matrix4x41.M34 + Matrix4x42.M34;
            result.M41 = Matrix4x41.M41 + Matrix4x42.M41;
            result.M42 = Matrix4x41.M42 + Matrix4x42.M42;
            result.M43 = Matrix4x41.M43 + Matrix4x42.M43;
            result.M44 = Matrix4x41.M44 + Matrix4x42.M44;

        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4x4 x &&
                   M11 == x.M11 &&
                   M12 == x.M12 &&
                   M13 == x.M13 &&
                   M14 == x.M14 &&
                   M21 == x.M21 &&
                   M22 == x.M22 &&
                   M23 == x.M23 &&
                   M24 == x.M24 &&
                   M31 == x.M31 &&
                   M32 == x.M32 &&
                   M33 == x.M33 &&
                   M34 == x.M34 &&
                   M41 == x.M41 &&
                   M42 == x.M42 &&
                   M43 == x.M43 &&
                   M44 == x.M44;
        }

        public bool Equals(Matrix4x4 other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            var hashCode = 856511749;
            hashCode = hashCode * -1521134295 + M11.GetHashCode();
            hashCode = hashCode * -1521134295 + M22.GetHashCode();
            hashCode = hashCode * -1521134295 + M24.GetHashCode();
            hashCode = hashCode * -1521134295 + M32.GetHashCode();
            hashCode = hashCode * -1521134295 + M34.GetHashCode();
            hashCode = hashCode * -1521134295 + M43.GetHashCode();
            return hashCode;
        }

        public static Matrix4x4 operator +(Matrix4x4 Matrix4x41, Matrix4x4 Matrix4x42)
        {
            Matrix4x4.Add(ref Matrix4x41, ref Matrix4x42, out Matrix4x41);
            return Matrix4x41;
        }

        public static bool operator ==(Matrix4x4 Matrix4x41, Matrix4x4 Matrix4x42)
        {
            return (
                Matrix4x41.M11 == Matrix4x42.M11 &&
                Matrix4x41.M12 == Matrix4x42.M12 &&
                Matrix4x41.M13 == Matrix4x42.M13 &&
                Matrix4x41.M14 == Matrix4x42.M14 &&
                Matrix4x41.M21 == Matrix4x42.M21 &&
                Matrix4x41.M22 == Matrix4x42.M22 &&
                Matrix4x41.M23 == Matrix4x42.M23 &&
                Matrix4x41.M24 == Matrix4x42.M24 &&
                Matrix4x41.M31 == Matrix4x42.M31 &&
                Matrix4x41.M32 == Matrix4x42.M32 &&
                Matrix4x41.M33 == Matrix4x42.M33 &&
                Matrix4x41.M34 == Matrix4x42.M34 &&
                Matrix4x41.M41 == Matrix4x42.M41 &&
                Matrix4x41.M42 == Matrix4x42.M42 &&
                Matrix4x41.M43 == Matrix4x42.M43 &&
                Matrix4x41.M44 == Matrix4x42.M44
                );
        }

        public static bool operator !=(Matrix4x4 Matrix4x41, Matrix4x4 Matrix4x42)
        {
            return (
                Matrix4x41.M11 != Matrix4x42.M11 ||
                Matrix4x41.M12 != Matrix4x42.M12 ||
                Matrix4x41.M13 != Matrix4x42.M13 ||
                Matrix4x41.M14 != Matrix4x42.M14 ||
                Matrix4x41.M21 != Matrix4x42.M21 ||
                Matrix4x41.M22 != Matrix4x42.M22 ||
                Matrix4x41.M23 != Matrix4x42.M23 ||
                Matrix4x41.M24 != Matrix4x42.M24 ||
                Matrix4x41.M31 != Matrix4x42.M31 ||
                Matrix4x41.M32 != Matrix4x42.M32 ||
                Matrix4x41.M33 != Matrix4x42.M33 ||
                Matrix4x41.M34 != Matrix4x42.M34 ||
                Matrix4x41.M41 != Matrix4x42.M41 ||
                Matrix4x41.M42 != Matrix4x42.M42 ||
                Matrix4x41.M43 != Matrix4x42.M43 ||
                Matrix4x41.M44 != Matrix4x42.M44
                );
        }

        public static Matrix4x4 operator *(Matrix4x4 Matrix4x41, Matrix4x4 Matrix4x42)
        {
            Matrix4x4 Matrix4x4;
            Matrix4x4.M11 = (((Matrix4x41.M11 * Matrix4x42.M11) + (Matrix4x41.M12 * Matrix4x42.M21)) + (Matrix4x41.M13 * Matrix4x42.M31)) + (Matrix4x41.M14 * Matrix4x42.M41);
            Matrix4x4.M12 = (((Matrix4x41.M11 * Matrix4x42.M12) + (Matrix4x41.M12 * Matrix4x42.M22)) + (Matrix4x41.M13 * Matrix4x42.M32)) + (Matrix4x41.M14 * Matrix4x42.M42);
            Matrix4x4.M13 = (((Matrix4x41.M11 * Matrix4x42.M13) + (Matrix4x41.M12 * Matrix4x42.M23)) + (Matrix4x41.M13 * Matrix4x42.M33)) + (Matrix4x41.M14 * Matrix4x42.M43);
            Matrix4x4.M14 = (((Matrix4x41.M11 * Matrix4x42.M14) + (Matrix4x41.M12 * Matrix4x42.M24)) + (Matrix4x41.M13 * Matrix4x42.M34)) + (Matrix4x41.M14 * Matrix4x42.M44);
            Matrix4x4.M21 = (((Matrix4x41.M21 * Matrix4x42.M11) + (Matrix4x41.M22 * Matrix4x42.M21)) + (Matrix4x41.M23 * Matrix4x42.M31)) + (Matrix4x41.M24 * Matrix4x42.M41);
            Matrix4x4.M22 = (((Matrix4x41.M21 * Matrix4x42.M12) + (Matrix4x41.M22 * Matrix4x42.M22)) + (Matrix4x41.M23 * Matrix4x42.M32)) + (Matrix4x41.M24 * Matrix4x42.M42);
            Matrix4x4.M23 = (((Matrix4x41.M21 * Matrix4x42.M13) + (Matrix4x41.M22 * Matrix4x42.M23)) + (Matrix4x41.M23 * Matrix4x42.M33)) + (Matrix4x41.M24 * Matrix4x42.M43);
            Matrix4x4.M24 = (((Matrix4x41.M21 * Matrix4x42.M14) + (Matrix4x41.M22 * Matrix4x42.M24)) + (Matrix4x41.M23 * Matrix4x42.M34)) + (Matrix4x41.M24 * Matrix4x42.M44);
            Matrix4x4.M31 = (((Matrix4x41.M31 * Matrix4x42.M11) + (Matrix4x41.M32 * Matrix4x42.M21)) + (Matrix4x41.M33 * Matrix4x42.M31)) + (Matrix4x41.M34 * Matrix4x42.M41);
            Matrix4x4.M32 = (((Matrix4x41.M31 * Matrix4x42.M12) + (Matrix4x41.M32 * Matrix4x42.M22)) + (Matrix4x41.M33 * Matrix4x42.M32)) + (Matrix4x41.M34 * Matrix4x42.M42);
            Matrix4x4.M33 = (((Matrix4x41.M31 * Matrix4x42.M13) + (Matrix4x41.M32 * Matrix4x42.M23)) + (Matrix4x41.M33 * Matrix4x42.M33)) + (Matrix4x41.M34 * Matrix4x42.M43);
            Matrix4x4.M34 = (((Matrix4x41.M31 * Matrix4x42.M14) + (Matrix4x41.M32 * Matrix4x42.M24)) + (Matrix4x41.M33 * Matrix4x42.M34)) + (Matrix4x41.M34 * Matrix4x42.M44);
            Matrix4x4.M41 = (((Matrix4x41.M41 * Matrix4x42.M11) + (Matrix4x41.M42 * Matrix4x42.M21)) + (Matrix4x41.M43 * Matrix4x42.M31)) + (Matrix4x41.M44 * Matrix4x42.M41);
            Matrix4x4.M42 = (((Matrix4x41.M41 * Matrix4x42.M12) + (Matrix4x41.M42 * Matrix4x42.M22)) + (Matrix4x41.M43 * Matrix4x42.M32)) + (Matrix4x41.M44 * Matrix4x42.M42);
            Matrix4x4.M43 = (((Matrix4x41.M41 * Matrix4x42.M13) + (Matrix4x41.M42 * Matrix4x42.M23)) + (Matrix4x41.M43 * Matrix4x42.M33)) + (Matrix4x41.M44 * Matrix4x42.M43);
            Matrix4x4.M44 = (((Matrix4x41.M41 * Matrix4x42.M14) + (Matrix4x41.M42 * Matrix4x42.M24)) + (Matrix4x41.M43 * Matrix4x42.M34)) + (Matrix4x41.M44 * Matrix4x42.M44);
            return Matrix4x4;
        }
    }
}

