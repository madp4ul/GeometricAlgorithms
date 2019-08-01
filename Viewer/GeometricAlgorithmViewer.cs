﻿using System;
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
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;
using GeometricAlgorithms.Domain.Providers;
using GeometricAlgorithms.Domain.Drawables;

namespace GeometricAlgorithms.Viewer
{
    public partial class GeometricAlgorithmViewer : UserControl, IDrawableFactoryProvider
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ViewerConfiguration Configuration { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Workspace Workspace { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDrawableFactory DrawableFactory => Viewer3D.DrawableFactory;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private readonly MouseDragger ViewerDragger;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private FirstPersonCamera Camera;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control Display => viewer.Display;

        private readonly System.Timers.Timer KeyEventTimer;

        public GeometricAlgorithmViewer()
        {
            InitializeComponent();

            ViewerDragger = new MouseDragger(viewer.Display);
            ViewerDragger.OnMouseDrag += ViewerDragger_OnMouseDrag;

            KeyEventTimer = new System.Timers.Timer();
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Camera = new FirstPersonCamera();

                Camera.SetPosition(new Vector3(0.5f, 0.5f, 1.3f));
                Camera.SetRotation(0, 0);
                Camera.SetProjection((float)Math.PI / 3, 1, 0.0001f, 1000f);

                viewer.Scene.Camera = Camera;
                viewer.Scene.Drawables.Add(Workspace);

                KeyEventTimer.Elapsed += KeyEventTimer_Elapsed;
                KeyEventTimer.Enabled = true;
                KeyEventTimer.Interval = 16;
                KeyEventTimer.Start();
            }
        }

        private void ViewerDragger_OnMouseDrag(Size size)
        {
            float Sensitivity = 0.015f * Configuration.MouseSensitivity;

            Camera.SetRotation(Camera.RotationX + size.Height * Sensitivity, Camera.RotationY + size.Width * Sensitivity);
            viewer.Invalidate();
        }

        private void KeyEventTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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

            movement *= 0.0003f * (float)KeyEventTimer.Interval;

            Camera.SetPosition(Camera.Position + movement);

            viewer.Invalidate();
        }

        private void GeometricAlgorithmViewer_Resize(object sender, EventArgs e)
        {
            if (Camera != null)
            {
                Camera.SetProjection(
                    Camera.FieldOfView,
                    (float)viewer.Display.Width / (float)viewer.Display.Height,
                    Camera.NearPlane,
                    Camera.FarPlane);
                viewer.Invalidate();
            }
        }
    }
}
