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
        public readonly int NearPointCount;
        public readonly int SampleCount;

        public readonly KdTree KdTree;
        public Mesh Mesh => KdTree.Mesh;


        public NormalOrientationFinder(KdTree kdTreeOfMeshWithNormals, int sampleCount = 1000, int nearPointCount = 5)
        {
            KdTree = kdTreeOfMeshWithNormals ?? throw new ArgumentNullException(nameof(kdTreeOfMeshWithNormals));
            NearPointCount = nearPointCount;
            SampleCount = sampleCount;

            if (!KdTree.Mesh.HasNormals)
            {
                throw new ArgumentException("Mesh needs normals");
            }
        }

        public bool NormalsAreOrientedOutwards(IProgressUpdater progressUpdater = null)
        {
            int sampleOffset = Mesh.VertexCount / SampleCount;
            if (sampleOffset < 1)
            {
                sampleOffset = 1;
            }

            var progress = new OperationProgressUpdater(progressUpdater, SampleCount, "calculating normal orientation");

            //leave for now
            //float minimumPositionSum = 0;
            //float minimumPositionSumInverted = 0;
            int positiveMinuesNegativeValues = 0;


            for (int currentPointIndex = 0; currentPointIndex < Mesh.VertexCount; currentPointIndex += sampleOffset)
            {
                var comparePositions = KdTree.FindNearestVertices(Mesh.Positions[currentPointIndex], NearPointCount)
                    .Where(neighbour => neighbour.Value.OriginalIndex != currentPointIndex);

                foreach (var comparePosition in comparePositions)
                {
                    float value = GetMinimalDistancePositionOfNormals(currentPointIndex, comparePosition.Value.OriginalIndex);

                    //Simply counting the cases turned out best for now
                    //Other (commented out) criteria might be helpful later
                    positiveMinuesNegativeValues += value > 0 ? 1 : (value < 0 ? -1 : 0);

                    //leave for now
                    //minimumPositionSum += value;
                    //if (value != 0)
                    //{
                    //    minimumPositionSumInverted += 1 / value;
                    //}
                }

                progress.UpdateAddOperation();
            }

            progress.IsCompleted();

            //if the minima are behind the positions in normal direction, 
            //they must point "away" from each other going forward.
            //Thus they must be pointing to the outside of a curve and be oriented outwards.
            return positiveMinuesNegativeValues <= 0;

            //int negativeCriteria =
            //    +(positiveMinuesNegativeValues < 0 ? 1 : 0)
            //    + (minimumPositionSum < 0 ? 1 : 0)
            //    + (minimumPositionSumInverted < 0 ? 1 : 0);

            //return negativeCriteria > 1;
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

            //TODO check if float inaccuracy could be or cause a problem
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
