﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class HighlightedPointCloud : PointCloud
    {
        public Vector3 HighlightColor { get; set; }

        public HighlightedPointCloud(PointEffect effect, Vector3[] positions, Vector3 highlightColor, int pixelWidth = 2)
            : base(effect, positions, pixelWidth)
        {
            HighlightColor = highlightColor;
        }

        protected override void ApplyEffectPass()
        {
            Effect.ApplyPointHighlight();
        }

        public override void Draw(ACamera camera)
        {
            var previous = Effect.HighlightColor;
            Effect.HighlightColor = HighlightColor;

            base.Draw(camera);

            Effect.HighlightColor = previous;
        }
    }
}
