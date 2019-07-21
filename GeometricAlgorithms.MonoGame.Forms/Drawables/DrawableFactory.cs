using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class DrawableFactory
    {
        internal ContentProvider ContentProvider { get; set; }

        public DrawableFactory(GameServiceContainer services, GraphicsDevice device)
        {
            ContentProvider = new ContentProvider(services, device);

        }

        public IDrawable CreatePointCloud(Domain.Vector3[] points, int radius)
        {
            var xnaPoints = points
                .Select(v => v.ToXna())
                .ToArray();

            return new PointCloud(ContentProvider.PointEffect.Value, xnaPoints, radius);
        }

        public IDrawable CreateBoundingBoxRepresentation(Domain.BoundingBox[] boxes)
        {
            return new BoundingBoxRepresentation(ContentProvider.GraphicsDevice, boxes);
        }

    }
}
