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
using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.OpenTk.Shaders;

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

        Model<Vertex> MyModel;

        public CustomGLControl() : base(new GraphicsMode(new ColorFormat(32), 32, 32))
        {
            InitializeComponent();

            var timer = new Timer();
            timer.Tick += T_Tick;
            timer.Interval = 5;
            timer.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var shader = new PointShader();

            MyModel = new Model<Vertex>(new Vertex[]
                {
                    new Vertex(new Vector3(-0.5f, -0.5f, 0.0f)),
                    new Vertex(new Vector3(0.5f, -0.5f, 0.0f)),
                    new Vertex(new Vector3(0.0f,  0.5f, 0.0f))
                },
                new uint[] { 0, 1, 2 },
                shader,
                PrimitiveType.Triangles,
                PolygonMode.Fill,
                MaterialFace.FrontAndBack);

            // Ensure the Viewport is set up correctly
            OnResize(EventArgs.Empty);

            //Set color to clear buffer to
            GL.ClearColor(ClearColor);
        }

        protected override void OnResize(EventArgs e)
        {
            //base.OnResize(e);

            this.MakeCurrent();
            GL.Viewport(0, 0, Width, Height);

            RedrawScene();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.DesignMode)
            {
                if (!IsDrawing)
                {
                    IsDrawing = true;
                    RedrawScene();
                    IsDrawing = false;
                }
            }
        }

        public override void Refresh()
        {
            base.Refresh();
        }

        private void RedrawScene()
        {
            MakeCurrent();

            var r = new Random();
            GL.ClearColor(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));


            //Clear buffer with color
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //Draw models
            DrawModels();

            SwapBuffers();
        }

        private void DrawModels()
        {
            this.MakeCurrent();
            //TODO draw models
            MyModel.Draw();

        }
    }
}
