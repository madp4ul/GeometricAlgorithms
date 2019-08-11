using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Drawables
{
    public class CameraChangedEventDrawable : IDrawable
    {
        public readonly IDrawable Drawable;

        public Transformation Transformation { get => Drawable.Transformation; set => Drawable.Transformation = value; }

        public event Action<ACamera> CameraChanged;

        public CameraChangedEventDrawable()
        {
            Drawable = new EmptyDrawable();
        }

        public CameraChangedEventDrawable(IDrawable drawable)
        {
            Drawable = drawable ?? throw new ArgumentNullException(nameof(drawable));
        }

        public void Dispose()
        {
            Drawable.Dispose();
        }

        private Vector3 LastPosition { get; set; }
        private Vector3 LastForward { get; set; }
        private Vector3 LastUp { get; set; }
        public void Draw(ACamera camera)
        {
            if (camera.Position != LastPosition
                || camera.Forward != LastForward
                || camera.Up != LastUp)
            {
                CameraChanged?.Invoke(camera);
            }

            Drawable.Draw(camera);

            LastPosition = camera.Position;
            LastForward = camera.Forward;
            LastUp = camera.Up;
        }
    }
}
