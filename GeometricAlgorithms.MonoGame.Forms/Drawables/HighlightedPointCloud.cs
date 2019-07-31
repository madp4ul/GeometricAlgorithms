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
        public Color HighlightColor { get; set; }

        public HighlightedPointCloud(PointEffect effect, Vector3[] positions, Color highlightColor, int pixelWidth = 2)
            : base(effect, positions, pixelWidth)
        {
            HighlightColor = highlightColor;
        }

        protected override void ApplyEffectPass()
        {
            Effect.ApplyPointHighlight();
        }

        public override void Draw(ICamera camera)
        {
            var previous = Effect.HighlightColor;
            Effect.HighlightColor = HighlightColor.ToVector3();

            base.Draw(camera);

            Effect.HighlightColor = previous;
        }
    }
}
