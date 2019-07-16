using GeometricAlgorithms.MonoGame.Shader;
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

        public ContentProvider(GameServiceContainer services)
        {
            ContentManager = new ResourceContentManager(services, Properties.Resources.ResourceManager);
        }

        public PointEffect LoadPointEffect()
        {
            return PointEffect.FromEffect(ContentManager.Load<Effect>("PointShader"));
        }
    }
}
