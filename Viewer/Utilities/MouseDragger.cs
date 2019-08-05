using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.Utilities
{
    class MouseDragger
    {
        public Control Control { get; set; }

        public bool IsMouseDown { get; private set; }
        public Point MouseDownPosition { get; set; }

        public event Action<Size> OnMouseDrag;

        public MouseDragger(Control control)
        {
            Control = control ?? throw new ArgumentNullException(nameof(control));

            Control.MouseDown += Control_MouseDown;
            Control.MouseMove += Control_MouseMove;
            Control.MouseUp += Control_MouseUp;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseDownPosition = e.Location;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsMouseDown)
            {
                Size mouseMovedDistance = new Size(e.Location.X - MouseDownPosition.X, e.Location.Y - MouseDownPosition.Y);
                MouseDownPosition = e.Location;

                OnMouseDrag?.Invoke(mouseMovedDistance);
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }
    }
}
