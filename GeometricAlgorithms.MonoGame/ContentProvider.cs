using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame
{
    public class ContentProvider
    {
        private readonly ResourceContentManager ContentManager;

        public ContentProvider(GameServiceContainer services, GraphicsDevice device)
        {
            ContentManager = new ResourceContentManager(services, Properties.Resources.ResourceManager);
            GraphicsDevice = device;
        }

        public PointEffect PointEffect { get => LoadPointEffect(); }

        private PointEffect LoadPointEffect()
        {
            return PointRendering.PointEffect.FromEffect(ContentManager.Load<Effect>("PointShader"));
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
    }
}
