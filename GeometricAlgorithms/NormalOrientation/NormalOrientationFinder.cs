using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.NormalOrientation
{
    public class NormalOrientationFinder
    {
        public readonly ATree Tree;
        public Mesh Mesh => Tree.Mesh;


        public NormalOrientationFinder(ATree treeOfMeshWithNormals)
        {
            Tree = treeOfMeshWithNormals ?? throw new ArgumentNullException(nameof(treeOfMeshWithNormals));

            if (!Tree.Mesh.HasNormals)
            {
                throw new ArgumentException("Mesh needs normals");
            }
        }

        public bool NormalsAreOrientedOutwards(IProgressUpdater progressUpdater = null)
        {
            var progress = new OperationProgressUpdater(progressUpdater, Mesh.VertexCount, "calculating normal orientation");

            int positiveMinuesNegativeValues = 0;

            var compareIndices = new RandomEnumerable(Mesh.VertexCount);

            for (int currentPointIndex = 0; currentPointIndex < Mesh.VertexCount; currentPointIndex++)
            {

                foreach (var compareIndex in compareIndices.Take(5))
                {
                    float value = GetMinimalDistancePositionOfNormals(currentPointIndex, compareIndex);

                    //Simply counting the cases turned out best for now
                    //Other (commented out) criteria might be helpful later
                    positiveMinuesNegativeValues += value > 0 ? 1 : (value < 0 ? -1 : 0);
                }

                progress.UpdateAddOperation();
            }

            progress.IsCompleted();
            Console.WriteLine(positiveMinuesNegativeValues);

            //if the minima are behind the positions in normal direction, 
            //they must point "away" from each other going forward.
            //Thus they must be pointing to the outside of a curve and be oriented outwards.
            return positiveMinuesNegativeValues <= 0;
        }

        /// <summary>
        /// Compute the point of minimal distance of two lines from indices.
        /// It inly compares points at same function value on both lines
        /// </summary>
        /// <param name="indexOfFirst"></param>
        /// <param name="indexOfSecond"></param>
        /// <returns></returns>
        private float GetMinimalDistancePositionOfNormals(int indexOfFirst, int indexOfSecond)
        {
            //first is g in docs, second is h

            Line3 firstLine = new Line3(Mesh.Positions[indexOfFirst], Mesh.UnitNormals[indexOfFirst]);
            Line3 secondLine = new Line3(Mesh.Positions[indexOfSecond], Mesh.UnitNormals[indexOfSecond]);

            //if they are parallel, every parameter has the same distance. 
            //then we select the "best" value as closest
            if (firstLine.Direction.IsLinearDependantOf(secondLine.Direction).HasValue)
            {
                return 0;
            }

            Vector3 positionDiff = secondLine.Position - firstLine.Position;
            Vector3 directionDiff = secondLine.Direction - firstLine.Direction;

            // try to set derivative to zero
            float derivativeNstDenominator = Vector3.Dot(directionDiff, directionDiff);

            if (derivativeNstDenominator != 0)
            {

                float derivativeNstNominator = -Vector3.Dot(positionDiff, directionDiff);

                return derivativeNstNominator / derivativeNstDenominator;
            }
            else
            {
                //cant divide as derivate is not defined for x.
                //instead there is a point where the minimal distance is zero

                float nstNominator = -(positionDiff.X + positionDiff.Y + positionDiff.Z);
                float nstDenominator = directionDiff.X + directionDiff.Y + directionDiff.Z;

                return nstNominator / nstDenominator;
            }
        }
    }
}
