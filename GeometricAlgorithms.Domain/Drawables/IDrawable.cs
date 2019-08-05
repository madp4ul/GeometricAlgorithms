using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Drawables
{
    public interface IDrawable : IDisposable
    {
        Transformation Transformation { get; set; }
        void Draw(ACamera camera);
    }
}
