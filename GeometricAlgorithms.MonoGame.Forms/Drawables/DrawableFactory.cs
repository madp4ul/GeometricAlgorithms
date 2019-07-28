using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class DrawableFactory
    {
        internal ContentProvider ContentProvider { get; set; }

        public DrawableFactory(Microsoft.Xna.Framework.GameServiceContainer services, GraphicsDevice device)
        {
            ContentProvider = new ContentProvider(services, device);
        }

        public IDrawable CreatePointCloud(IEnumerable<Vector3> points, int radius)
        {
            var xnaPoints = points
                .Select(v => v.ToXna())
                .ToArray();

            return new PointCloud(ContentProvider.PointEffect, xnaPoints, radius);
        }

        public IDrawable CreateBoundingBoxRepresentation(BoundingBox[] boxes, Func<BoundingBox, Color> colorGenerator = null)
        {
            Func<BoundingBox, Microsoft.Xna.Framework.Color> xnaColorGenerator = null;
            if (colorGenerator != null)
            {
                xnaColorGenerator = (box) =>
                {
                    var c = colorGenerator(box);
                    return new Microsoft.Xna.Framework.Color(c.R, c.G, c.B, c.A);
                };
            }

            return new BoundingBoxRepresentation(ContentProvider.GraphicsDevice, boxes, xnaColorGenerator);
        }

        public IDrawable CreateHighlightedPointCloud(IEnumerable<Vector3> points, int radius)
        {
            var xnaPoints = points
                .Select(v => v.ToXna())
                .ToArray();

            return new HighlightedPointCloud(ContentProvider.PointEffect, xnaPoints, radius);
        }
    }
}
