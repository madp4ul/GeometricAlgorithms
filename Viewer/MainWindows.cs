using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer
{
    public partial class MainWindows : Form
    {
        private MouseDragger ViewerDragger;

        FirstPersonCamera Camera;

        public MainWindows()
        {
            InitializeComponent();

            ViewerDragger = new MouseDragger(this);
            ViewerDragger.OnMouseDrag += ViewerDragger_OnMouseDrag;
        }



        private void Viewer3D_Load(object sender, EventArgs e)
        {
            var scene = new Scene();
            Camera = new FirstPersonCamera();

            Camera.SetPosition(new Vector3(0, 0f, 3f));
            Camera.SetRotation(0, 0);
            Camera.SetProjection((float)Math.PI / 3, 1, 0.0001f, 1000f);


            scene.Camera = Camera;

            var rand = new Random();
            Vector3[] points = new Vector3[4000];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Domain.Vector3(
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble());
            }

            scene.Drawables.Add(viewer.DrawableFactory.CreatePointCloud(points, 2));

            viewer.Scene = scene;
        }

        private void ViewerDragger_OnMouseDrag(Size size)
        {
            const float Sensitivity = 0.01f;

            Camera.SetRotation(size.Width * Sensitivity, size.Height * Sensitivity);
            viewer.Invalidate();
        }
    }
}
