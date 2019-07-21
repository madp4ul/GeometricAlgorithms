using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Extensions
{
    static class Vector3Extensions
    {
        public static Microsoft.Xna.Framework.Vector3 ToXna(this Domain.Vector3 v)
        {
            return new Microsoft.Xna.Framework.Vector3(v.X, v.Y, v.Z);
        }
    }
}
