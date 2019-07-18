using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame
{
    public interface IViewProjectionEffect
    {
        Matrix ViewProjectionMatrix { set; get; }
    }
}
