using GeometricAlgorithms.Domain.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Interfaces
{
    public interface IDrawableFactoryProvider
    {
        IDrawableFactory DrawableFactory { get; }
    }
}
