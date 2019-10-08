using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.FaceModels
{
    public class ImplicitSurfaceModel : IUpdatable<ATree>
    {
        public IFiniteImplicitSurface ImplicitSurface { get; private set; }

        public bool CanApproximate { get; private set; }


        public ImplicitSurfaceModel()
        {
            _UsedNearestPointCount = 10;
        }

        private int _UsedNearestPointCount;
        public int UsedNearestPointCount
        {
            get => _UsedNearestPointCount;
            set
            {
                _UsedNearestPointCount = value;
                if (ImplicitSurface != null
                    && ImplicitSurface is ScalarProductSurface scalarProductSurface)
                {
                    scalarProductSurface.UsedNearestPointCount = value;
                }
            }
        }

        public event Action Updated;

        public void Update(ATree tree)
        {
            CanApproximate = tree.Mesh.HasNormals;

            ImplicitSurface = CanApproximate
                ? new ScalarProductSurface(tree, UsedNearestPointCount)
                : null;

            if (ImplicitSurface != null)
            {
                ImplicitSurface = new DebugTimeSurface(ImplicitSurface);
            }


            Updated?.Invoke();
        }

        private class DebugTimeSurface : IFiniteImplicitSurface
        {
            private readonly IFiniteImplicitSurface Surface;
            private readonly System.Diagnostics.Stopwatch StopWatch;

            public DebugTimeSurface(IFiniteImplicitSurface surface)
            {
                Surface = surface ?? throw new ArgumentNullException(nameof(surface));
                StopWatch = new System.Diagnostics.Stopwatch();
            }

            public int CalculationCount { get; private set; }
            public TimeSpan CalculationTime => StopWatch.Elapsed;


            public BoundingBox DefinedArea => Surface.DefinedArea;

            public float GetApproximateSurfaceDistance(Vector3 position)
            {
                StopWatch.Start();

                var pos = Surface.GetApproximateSurfaceDistance(position);

                StopWatch.Stop();

                CalculationCount++;

                if (CalculationCount % 1000 == 0)
                {
                    double average = CalculationTime.TotalSeconds / CalculationCount;

                    Console.WriteLine($"{CalculationCount} calculations took {CalculationTime.TotalSeconds:N3} seconds. The average calculation takes {average:N7} seconds");
                }

                return pos;
            }
        }
    }
}
