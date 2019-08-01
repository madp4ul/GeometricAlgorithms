using GeometricAlgorithms.Domain.Drawables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Extensions
{
    static class TransformationExtensions
    {
        public static Matrix GetWorldMatrix(this Transformation transformation)
        {
            return Matrix.CreateScale(transformation.Scale) * Matrix.CreateTranslation(transformation.Position.ToXna());
        }
    }
}
