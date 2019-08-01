using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Drawables
{
    public class Transformation
    {
        public Domain.Vector3 Position { get; set; }

        public float Scale { get; set; }

        public static Transformation Identity => new Transformation();

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

        public void AddTransformation(Transformation t)
        {
            Position += t.Position;
            Scale *= t.Scale;
        }

        public void SubtractTransformation(Transformation t)
        {
            Position -= t.Position;
            Scale /= t.Scale;
        }
    }
}
