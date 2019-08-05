namespace GeometricAlgorithms.MonoGame.Forms.Cameras
{
    using GeometricAlgorithms.Domain;
    using GeometricAlgorithms.Domain.Cameras;
    using GeometricAlgorithms.MonoGame.Forms.Extensions;
    using System;

    public class FirstPersonCamera : ACamera
    {
        private readonly FirstPerson.InternalFirstPersonCamera Camera;

        public override Vector3 Position { get; set; }

        public override Vector3 Forward => new Vector3(Camera.Direction.X, Camera.Direction.Y, Camera.Direction.Z);
        public override Vector3 Up => new Vector3(Camera.Up.X, Camera.Up.Y, Camera.Up.Z);

        public float RotationY { get; private set; }

        public float RotationX { get; private set; }

        public float FieldOfView { get; private set; }

        public float AspectRatio { get; private set; }

        public float NearPlane { get; private set; }

        public float FarPlane { get; private set; }

        public override Matrix4x4 ViewProjection
        {
            get
            {
                return Camera.ViewProjectionMatrix.ToDomain();
            }
        }

        public FirstPersonCamera()
        {
            Camera = new FirstPerson.InternalFirstPersonCamera();

            //set defaults
            Position = new Vector3(0, 0, 0);
            RotationX = 0;
            RotationY = 0;
            FieldOfView = Microsoft.Xna.Framework.MathHelper.ToRadians(60);
            AspectRatio = 1;
            NearPlane = 0.001f;
            FarPlane = 100f;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;

            UpdateMatrix();
        }

        public void SetRotation(float x, float y)
        {

            const float maxVerticalRotation = (float)Math.PI / 2f - 0.01f;

            RotationX = Microsoft.Xna.Framework.MathHelper.Clamp(x, -maxVerticalRotation, maxVerticalRotation);

            RotationY = y;

            UpdateMatrix();
        }

        public void SetProjection(float fov, float aspect, float near, float far)
        {
            FieldOfView = fov;
            AspectRatio = aspect;
            NearPlane = near;
            FarPlane = far;

            UpdateMatrix();
        }

        private void UpdateMatrix()
        {
            Camera.UpdateMatrix(
                new Microsoft.Xna.Framework.Vector3(Position.X, Position.Y, Position.Z),
                RotationX, RotationY, FieldOfView, AspectRatio, NearPlane, FarPlane);
        }
    }
}

namespace GeometricAlgorithms.MonoGame.Forms.Cameras.FirstPerson
{
    using Microsoft.Xna.Framework;

    class InternalFirstPersonCamera
    {
        public Matrix ViewProjectionMatrix { get; private set; }

        public Vector3 Direction { get; private set; }
        public readonly Vector3 Up = Vector3.Up;

        public void UpdateMatrix(Vector3 position, float rotX, float rotY, float fov, float aspect, float near, float far)
        {
            Direction = Vector3.Transform(-Vector3.UnitZ, Matrix.CreateRotationX(rotX) * Matrix.CreateRotationY(rotY));

            Matrix view = Matrix.CreateLookAt(position, position + Direction, Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect, near, far);

            ViewProjectionMatrix = view * projection;
        }
    }
}