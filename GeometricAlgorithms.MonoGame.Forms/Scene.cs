using GeometricAlgorithms.MonoGame.Forms.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public class Scene
    {
        public List<Drawables.IDrawable> Drawables { get; private set; }
        public ICamera Camera { get; set; }

        public Scene()
        {
            Drawables = new List<Drawables.IDrawable>();
        }

        public void Draw()
        {
            foreach (var drawable in Drawables)
            {
                drawable.Draw(Camera);
            }
        }
    }
}
