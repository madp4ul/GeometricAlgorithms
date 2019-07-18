using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Cameras
{
    public abstract class ICamera
    {
        internal abstract IInternalCamera Data { get; }
    }
}
