using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class HighlightedPointCloud : PointCloud
    {
        public HighlightedPointCloud(PointEffect effect, Vector3[] positions, int pixelWidth = 2)
            : base(effect, positions, pixelWidth)
        {
        }

        public override void Draw(ICamera camera)
        {
            var previous = Effect.IsHighlighted;
            Effect.IsHighlighted = true;

            base.Draw(camera);

            Effect.IsHighlighted = previous;
        }
    }
}
