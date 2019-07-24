using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Providers
{
    public interface IDrawableFactoryProvider
    {
        MonoGame.Forms.Drawables.DrawableFactory DrawableFactory { get; }
    }
}
