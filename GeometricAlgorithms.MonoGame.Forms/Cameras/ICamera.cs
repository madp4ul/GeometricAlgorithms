using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Cameras
{
    public abstract class ICamera
    {
        internal abstract ICameraData Data { get; }

        public abstract Vector3 Position { get; set; }

        public abstract Vector3 Forward { get; }
        public abstract Vector3 Up { get; }
    }
}
