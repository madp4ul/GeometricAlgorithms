using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class CompositeDrawable : IDrawable
    {
        public List<IDrawable> Drawables { get; private set; }
        public Transformation Transformation { get; set; }

        public CompositeDrawable(params IDrawable[] drawables)
        {
            Transformation = Transformation.Identity;
            Drawables = new List<IDrawable>();
            Drawables.AddRange(drawables);
        }

        public void Dispose()
        {
            foreach (var drawable in Drawables)
            {
                drawable.Dispose();
            }
        }

        public void Draw(ICamera camera)
        {
            foreach (var drawable in Drawables)
            {
                drawable.Transformation.AddTransformation(Transformation);
                drawable.Draw(camera);
                drawable.Transformation.SubtractTransformation(Transformation);
            }
        }
    }
}
