using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GeometricAlgorithms.MonoGame.Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game : Microsoft.Xna.Framework.Game
    {
        private PointEffect PointEffect;
        private PointCloud Points;

        public Game()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var cm = new ResourceContentManager(Services, Properties.Resources.ResourceManager);

            PointEffect = PointEffect.FromEffect(cm.Load<Effect>("PointShader"));
            
            var rand = new Random();
            Vector3[] points = new Vector3[400000];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Vector3(
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble());
            }

            Points = new PointCloud(GraphicsDevice, points, pixelWidth: 1);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                (float)Math.PI / 3,
                GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
                0.0001f, 1000f);
            Matrix view = Matrix.CreateLookAt(new Vector3((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3), 1f, 3f),
                new Vector3(0.0f, 0.0f, 0.0f), Vector3.Up);
            Matrix world = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, 0));

            PointEffect.ViewProjectionMatrix = view * projection;
            PointEffect.WorldMatrix = world;

            Points.Draw(PointEffect);

            base.Draw(gameTime);
        }
    }
}
