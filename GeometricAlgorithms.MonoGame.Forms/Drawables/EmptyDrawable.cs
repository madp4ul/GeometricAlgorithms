using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class EmptyDrawable : IDrawable
    {


        public Transformation Transformation { get; set; }

        public EmptyDrawable()
        {
            Transformation = Transformation.Identity;
        }

        public void Dispose()
        {

        }

        public void Draw(ICamera camera)
        {
        }
    }
}
