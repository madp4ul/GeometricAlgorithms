using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class PointCloudVertexColored : PointCloud<VertexPositionIndexColor>
    {
        public Color[] Colors { get; set; }

        public PointCloudVertexColored(
            PointEffect effect,
            Vector3[] positions,
            Color[] colors,
            int pixelWidth = 2)
            : base(effect, positions, pixelWidth)
        {
            Colors = colors ?? throw new ArgumentNullException(nameof(colors));

            if (positions.Length != colors.Length)
            {
                throw new ArgumentException();
            }

            CreateBuffers();
        }


        protected override VertexPositionIndexColor CreateVertex(Vector3 position, Corner corner, int pointIndex)
        {
            return new VertexPositionIndexColor(position, corner, Colors[pointIndex]);
        }

        protected override void ApplyEffectPass()
        {
            Effect.ApplyPointColorDrawing();
        }
    }
}
