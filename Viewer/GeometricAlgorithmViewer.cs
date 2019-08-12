using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Utilities;
using GeometricAlgorithms.BusinessLogic;

namespace GeometricAlgorithms.Viewer
{
    public partial class GeometricAlgorithmViewer : UserControl, IDrawableFactoryProvider
    {
        public const int AutoInvalidationMilliseconds = 500;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ViewerConfiguration Configuration { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IHasDrawables Model { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDrawableFactory DrawableFactory => Viewer3D.DrawableFactory;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private readonly MouseDragger ViewerDragger;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private FirstPersonCamera Camera;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control Display => viewer.Display;

        public GeometricAlgorithmViewer()
        {
            InitializeComponent();

            ViewerDragger = new MouseDragger(viewer.Display);
            ViewerDragger.OnMouseDrag += ViewerDragger_OnMouseDrag;
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Camera = new FirstPersonCamera();

                Camera.SetPosition(new Vector3(0f, 0f, 0.8f));
                Camera.SetRotation(0, 0);
                Camera.SetProjection((float)Math.PI / 3, 1, 0.0001f, 1000f);
                SetAspectRatio();

                viewer.Scene.Camera = Camera;

                foreach (var drawable in Model.GetDrawables())
                {
                    viewer.Scene.Drawables.Add(drawable);
                }
            }
        }

        private bool DraggedMouseSinceLastUpdate = false;
        private void ViewerDragger_OnMouseDrag(Size size)
        {
            float Sensitivity = 0.015f * Configuration.MouseSensitivity;

            if (!size.IsEmpty)
            {
                Camera.SetRotation(Camera.RotationX + size.Height * Sensitivity, Camera.RotationY + size.Width * Sensitivity);
                DraggedMouseSinceLastUpdate = true;
            }
        }

        private DateTime LastInvalidationTime;
        private void InputProcessingTimer_Tick(object sender, EventArgs e)
        {
            if (ProcessKeyboardInput()
                || ProcessedMouseInput()
                || ProcessAutoInvalidation())
            {
                DraggedMouseSinceLastUpdate = false;
                LastInvalidationTime = DateTime.Now;
                Display.Invalidate();
            }
        }

        private bool ProcessedMouseInput()
        {
            return DraggedMouseSinceLastUpdate;
        }

        private bool ProcessAutoInvalidation()
        {
            return (DateTime.Now - LastInvalidationTime).TotalMilliseconds > AutoInvalidationMilliseconds;
        }

        private bool ProcessKeyboardInput()
        {
            Keys[] keys = viewer.GetPressedKeys();

            Vector3 movement = Vector3.Zero;

            if (keys.Contains(Keys.W))
                movement += Camera.Forward.Normalized();
            if (keys.Contains(Keys.A))
                movement -= Vector3.Cross(Camera.Forward, Camera.Up).Normalized();
            if (keys.Contains(Keys.D))
                movement += Vector3.Cross(Camera.Forward, Camera.Up).Normalized();
            if (keys.Contains(Keys.S))
                movement -= Camera.Forward.Normalized();
            if (keys.Contains(Keys.Space))
                movement += Camera.Up.Normalized();
            if (keys.Contains(Keys.LControlKey))
                movement -= Camera.Up.Normalized();

            movement *= 0.0003f * (float)inputProcessingTimer.Interval;

            if (!movement.Equals(Vector3.Zero))
            {
                Camera.SetPosition(Camera.Position + movement);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GeometricAlgorithmViewer_Resize(object sender, EventArgs e)
        {
            if (Camera != null)
            {
                SetAspectRatio();
                Display.Invalidate();
            }
        }

        private void SetAspectRatio()
        {
            Camera.SetProjection(
                    Camera.FieldOfView,
                    (float)Display.Width / (float)Display.Height,
                    Camera.NearPlane,
                    Camera.FarPlane);
        }
    }
}
