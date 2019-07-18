namespace GeometricAlgorithms.MonoGame.Forms.Cameras
{
    using GeometricAlgorithms.Domain;

    public class FirstPersonCamera : ICamera
    {
        private readonly FirstPerson.InternalFirstPersonCamera Camera;
        internal override ICameraData Data => Camera;

        public Vector3 Position { get; set; }

        public float RotationY { get; private set; }

        public float RotationX { get; private set; }

        public float FieldOfView { get; private set; }

        public float AspectRatio { get; private set; }

        public float NearPlane { get; private set; }

        public float FarPlane { get; private set; }

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
            RotationX = x;
            RotationY = y;

            UpdateMatrix();
        }

        public void SetProjection(float fov, float aspect, float near, float far)
        {
            FieldOfView = fov;
            AspectRatio = aspect;
            NearPlane = near;
            FarPlane = far;
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

    class InternalFirstPersonCamera : ICameraData
    {
        public Matrix ViewProjectionMatrix { get; private set; }
        public IViewProjectionEffect Effect { get; set; }

        public void UpdateMatrix(Vector3 position, float rotX, float rotY, float fov, float aspect, float near, float far)
        {
            Vector3 lookAt = Vector3.Transform(-Vector3.UnitZ, Matrix.CreateRotationX(rotX) * Matrix.CreateRotationY(rotY));

            Matrix view = Matrix.CreateLookAt(position, position + lookAt, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect, near, far);

            ViewProjectionMatrix = view * projection;
        }
    }
}