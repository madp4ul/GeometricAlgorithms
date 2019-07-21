using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class Transformation
    {
        public Domain.Vector3 Position { get; set; }

        public float Scale { get; set; }

        public Transformation(Domain.Vector3 position, float scale)
        {
            Position = position;
            Scale = scale;
        }

        public Transformation()
        {
            Position = new Domain.Vector3();
            Scale = 1;
        }

        internal Matrix GetWorldMatrix()
        {
            return Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position.ToXna());
        }
    }
}
