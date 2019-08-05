using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public class Scene
    {
        public List<IDrawable> Drawables { get; private set; }
        public ACamera Camera { get; set; }

        public Scene()
        {
            Drawables = new List<IDrawable>();
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
