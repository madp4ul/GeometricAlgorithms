using GeometricAlgorithms.MonoGame;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public class MonoGameControl : InvalidationControl
    {
        public List<Drawables.IDrawable> Drawables { get; set; }
        public DrawableFactory DrawableFactory { get; private set; }

        public MonoGameControl()
        {
            Drawables = new List<Drawables.IDrawable>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            DrawableFactory = new DrawableFactory(Editor.services);

            var rand = new Random();
            Domain.Vector3[] points = new Domain.Vector3[4000];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Domain.Vector3(
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble());
            }

            Drawables.Add(DrawableFactory.CreatePointCloud(points, 2));

            var t = new Timer();
            t.Interval = 100;
            t.Tick += (o, e) => Draw();
            t.Start();
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.graphics.Clear(Color.Black);

            Editor.graphics.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            foreach (var drawable in Drawables)
            {
                drawable.Draw();
            }
        }
    }
}
