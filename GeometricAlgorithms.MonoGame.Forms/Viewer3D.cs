using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.MonoGame.Forms.Drawables;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public partial class Viewer3D : UserControl
    {
        public DrawableFactory DrawableFactory => monoGameControl.DrawableFactory;

        public Scene Scene { get => monoGameControl.Scene; set => monoGameControl.Scene = value; }

        public Viewer3D()
        {
            InitializeComponent();
        }
    }
}
