using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public readonly PointData PointData;
        private readonly ContainerDrawable ReferenceFrame;

        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            ReferenceFrame = new ContainerDrawable();
            PointData = new PointData(drawableFactoryProvider, funcExecutor);
        }

        public void LoadReferenceFrame()
        {
            var drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(
                new[] { new BoundingBox(-Vector3.One, Vector3.One) }, (box) => Color.Blue.ToVector3());

            ReferenceFrame.SwapDrawable(drawable);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return ReferenceFrame;
            foreach (var drawable in PointData.GetDrawables())
            {
                yield return drawable;
            }
        }
    }
}