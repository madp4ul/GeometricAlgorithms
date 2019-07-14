using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public class MonoGameControl : InvalidationControl
    {
        public readonly Game Game;

        public MonoGameControl()
        {
            Game = new Game();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Game.Init();
        }

        protected override void Draw()
        {
            base.Draw();

            Game.Draw();
        }
    }
}
