using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk
{
    public interface IRenderable : IDisposable
    {
        void Render();
    }
}
