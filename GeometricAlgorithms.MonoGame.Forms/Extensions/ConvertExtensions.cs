using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Extensions
{
    public static class ConvertExtensions
    {
        public static Microsoft.Xna.Framework.Vector3 ToXna(this Domain.Vector3 v)
        {
            return new Microsoft.Xna.Framework.Vector3(v.X, v.Y, v.Z);
        }

        public static Microsoft.Xna.Framework.Color ToXnaColor(this Domain.Vector3 v)
        {
            return new Microsoft.Xna.Framework.Color(v.X, v.Y, v.Z);
        }

        public static Matrix4x4 ToDomain(this Microsoft.Xna.Framework.Matrix m)
        {
            return new Matrix4x4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static Microsoft.Xna.Framework.Matrix ToXna(this Matrix4x4 m)
        {
            return new Microsoft.Xna.Framework.Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }
    }
}
