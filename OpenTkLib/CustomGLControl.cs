using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;

namespace GeometricAlgorithms.OpenTk
{
    public partial class CustomGLControl : GLControl
    {
        public CustomGLControl() : base()
        {
            InitializeComponent();

            var t = new Timer();
            t.Interval = 100;

            t.Tick += T_Tick;

            t.Start();
        }



        private void T_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.MidnightBlue);

            OnResize(e);
        }

        protected override void OnResize(EventArgs e)
        {
            //base.OnResize(e);

            if (ClientSize.Height == 0)
                ClientSize = new System.Drawing.Size(ClientSize.Width, 1);

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.DesignMode)
            {
                MakeCurrent();

                var r = new Random();
                GL.ClearColor(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));

                //MakeCurrent();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                SwapBuffers();
            }
        }
    }
}
