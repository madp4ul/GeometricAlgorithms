using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public interface IDrawable : IDisposable
    {
        Transformation Transformation { get; set; }
        void Draw(ICamera camera);
    }
}
