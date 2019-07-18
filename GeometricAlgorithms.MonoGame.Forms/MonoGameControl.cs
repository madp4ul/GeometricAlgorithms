﻿using GeometricAlgorithms.MonoGame;
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
        public DrawableFactory DrawableFactory { get; private set; }

        public Scene Scene { get; set; }

        public MonoGameControl()
        {
            Scene = new Scene();
        }

        protected override void Initialize()
        {
            base.Initialize();

            DrawableFactory = new DrawableFactory(Editor.services);

            var t = new Timer();
            t.Interval = 100;
            t.Tick += (o, e) => Draw();
            t.Start();
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.graphics.Clear(Color.Black);

            //Editor.graphics.RasterizerState = new RasterizerState
            //{
            //    CullMode = CullMode.None
            //};

            Scene.Draw();
        }
    }
}
