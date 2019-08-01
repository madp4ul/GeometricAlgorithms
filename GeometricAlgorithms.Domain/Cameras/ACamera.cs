using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Cameras
{
    public abstract class ACamera
    {
        public abstract Vector3 Position { get; set; }

        public abstract Vector3 Forward { get; }
        public abstract Vector3 Up { get; }

        public abstract Matrix4x4 ViewProjection { get; }
    }
}
