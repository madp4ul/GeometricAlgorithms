using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.ConfigurationModel;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.Viewer
{
    public partial class GeometricAlgorithmViewer : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ViewerConfiguration Configuration { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private readonly MouseDragger ViewerDragger;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private FirstPersonCamera Camera;

        public GeometricAlgorithmViewer()
        {
            InitializeComponent();

            ViewerDragger = new MouseDragger(viewer.Display);
            ViewerDragger.OnMouseDrag += ViewerDragger_OnMouseDrag;

            viewer.Display.KeyDown += Display_KeyDown;
            
        }



        private void Viewer_Load(object sender, EventArgs e)
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
            float Sensitivity = 0.015f * Configuration.MouseSensitivity;

            Camera.SetRotation(Camera.RotationX + size.Height * Sensitivity, Camera.RotationY + size.Width * Sensitivity);
            viewer.Invalidate();
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {

            Vector3 movement;

            switch (e.KeyCode)
            {
                case Keys.W:
                    movement = Camera.Forward;
                    break;
                case Keys.A:
                    movement = -Vector3.Cross(Camera.Forward, Camera.Up);
                    break;
                case Keys.D:
                    movement = Vector3.Cross(Camera.Forward, Camera.Up);
                    break;
                case Keys.S:
                    movement = -Camera.Forward;
                    break;
                default:
                    movement = Vector3.Zero;
                    break;
            }

            movement *= 0.1f;

            Camera.SetPosition(Camera.Position + movement);



            viewer.Invalidate();
        }
    }
}
