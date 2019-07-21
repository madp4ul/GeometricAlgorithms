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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static DrawableFactory DrawableFactory => Instance.monoGameControl.DrawableFactory;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Scene Scene { get => monoGameControl.Scene; set => monoGameControl.Scene = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control Display => monoGameControl;

        private static Viewer3D Instance = null;

        public Viewer3D()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Con not use this control twice at the same time");
            }
            Instance = this;

            InitializeComponent();
        }

        ~Viewer3D()
        {
            Instance = null;
        }

        public Keys[] GetPressedKeys()
        {
            var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            return state.GetPressedKeys().Select(k => (Keys)Enum.Parse(typeof(Keys), k.ToString())).ToArray();
        }
    }
}
