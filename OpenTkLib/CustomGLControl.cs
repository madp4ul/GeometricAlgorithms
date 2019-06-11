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
        private Color _ClearColor = Color.CornflowerBlue;
        public Color ClearColor
        {
            get { return _ClearColor; }
            set
            {
                _ClearColor = value;
                GL.ClearColor(value);
            }
        }

        private bool IsDrawing = false;

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

            // Ensure the Viewport is set up correctly
            OnResize(EventArgs.Empty);

            //Set color to clear buffer to
            GL.ClearColor(ClearColor);
        }

        protected override void OnResize(EventArgs e)
        {
            this.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            this.SwapBuffers();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.DesignMode)
            {
                MakeCurrent();

                //var r = new Random();
                //GL.ClearColor(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));


                //Clear buffer with color
                GL.Clear(ClearBufferMask.ColorBufferBit);
                //Draw models
                Draw();

                SwapBuffers();
            }
        }

        public override void Refresh()
        {
            if (!IsDrawing)
            {
                IsDrawing = true;
                base.Refresh();
                IsDrawing = false;
            }
        }

        private void Draw()
        {
            //TODO draw models

         
        }
    }
}
