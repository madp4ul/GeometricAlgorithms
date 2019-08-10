using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class PointCloudAutoColored : PointCloud<VertexPositionIndex>
    {
        public PointCloudAutoColored(PointEffect effect, Vector3[] positions, int pixelWidth = 2)
            : base(effect, positions, pixelWidth)
        {
            CreateBuffers();
        }

        protected override VertexPositionIndex CreateVertex(Vector3 position, Corner corner, int pointIndex)
        {
            return new VertexPositionIndex(position, corner);
        }
    }
}
