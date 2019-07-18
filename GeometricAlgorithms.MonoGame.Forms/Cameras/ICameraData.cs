using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Cameras
{
    /// <summary>
    /// Wrap viewprojection data to hide it away from outside
    /// </summary>
    public interface ICameraData
    {
        /// <summary>
        /// Retrieve view projection from camera
        /// </summary>
        Matrix ViewProjectionMatrix { get; }
    }
}
