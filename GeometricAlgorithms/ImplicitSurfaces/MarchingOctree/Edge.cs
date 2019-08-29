using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class Edge
    {
        public FunctionValue Minimum;
        public FunctionValue Maximum;
        public Vector3 Middle => Minimum.Position + (Maximum.Position - Minimum.Position) / 2;
        public FunctionValue MiddleValueApproximation => new FunctionValue(Middle, (Minimum.Value + Maximum.Value) / 2);

        /// <summary>
        /// if the edge is not completed no valid function values can be retrieved from it.
        /// however the child edges still may be complete and can be used
        /// </summary>
        public bool IsComplete => Minimum != null && Maximum != null;

        /// <summary>
        /// How far away the vertex is from the start. only has a value
        /// if function values cross zero (surface) on this edge
        /// </summary>
        public readonly float? Interpolation;
        public bool HasVertex => Interpolation.HasValue;
        public Vector3 VertexPosition => Minimum.Position + (Maximum.Position - Minimum.Position) * Interpolation.Value;

        /// <summary>
        /// Index of Vertex on this edge. only has a value
        /// if function values cross zero (surface) on this edge
        /// </summary>
        private readonly Lazy<int> VertexIndex;

        private readonly Edge[] Children;

        //Build Edge from function values
        public Edge(FunctionValue start, FunctionValue end, SurfaceResult result)
            : this(start, end)
        {
            if (IsComplete && Minimum.Value * Maximum.Value <= 0)
            {
                float absMinValue = Math.Abs(Minimum.Value);
                float absMaxValue = Math.Abs(Maximum.Value);

                float valueSum = absMinValue + absMaxValue;
                Interpolation = absMinValue / valueSum;

                Vector3 vertex = Minimum.Position + (Maximum.Position - Minimum.Position) * Interpolation.Value;
                VertexIndex = new Lazy<int>(() => result.AddPosition(vertex));
            }
        }

        //Build edge from 2 children
        public Edge(Edge child0, Edge child1)
            : this(child0?.Minimum, child1?.Maximum)
        {
            Children[0] = child0;
            Children[1] = child1;

            if (child0 != null && child1 != null)
            {
                if (child0.Interpolation.HasValue && !child1.Interpolation.HasValue)
                {
                    Interpolation = child0.Interpolation.Value / 2;
                    VertexIndex = child0.VertexIndex;
                }
                else if (!child0.Interpolation.HasValue && child1.Interpolation.HasValue)
                {
                    Interpolation = 0.5f + child1.Interpolation.Value / 2;
                    VertexIndex = child1.VertexIndex;
                }
            }
        }

        //Build child edge from parent
        public Edge(FunctionValue start, FunctionValue end, float? interpolationValue, Lazy<int> vertexIndex)
            : this(start, end)
        {
            Interpolation = interpolationValue;
            VertexIndex = vertexIndex;
        }

        /// <summary>
        /// Only use from other constructor because it doesnt compute vertex
        /// </summary>
        /// <param name="start">min value or null</param>
        /// <param name="end">max value or null</param>
        private Edge(FunctionValue start, FunctionValue end)
        {
            Minimum = start;
            Maximum = end;

            Children = new Edge[2];
        }

        public int GetVertexIndex()
        {
            if (!HasVertex)
            {
                throw new InvalidOperationException();
            }
            return VertexIndex.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childIndex">0 or 1</param>
        /// <returns></returns>
        public Edge GetChild(int childIndex)
        {
            if (Children[childIndex] == null && IsComplete)
            {
                Vector3 middlePosition = Minimum.Position + (Maximum.Position - Minimum.Position) / 2;
                float middleValue = Minimum.Value + (Maximum.Value - Minimum.Value) / 2;
                FunctionValue middle = new FunctionValue(middlePosition, middleValue);

                //Set both child edges because the same data needs to be calculated to create both of them
                if (Interpolation.HasValue)
                {
                    if (Interpolation.Value > 0.5)
                    {
                        Children[0] = new Edge(Minimum, middle, null, null);
                        Children[1] = new Edge(middle, Maximum, 2 * (Interpolation.Value - 0.5f), VertexIndex);
                    }
                    else
                    {
                        Children[0] = new Edge(Minimum, middle, 2 * (Interpolation.Value), VertexIndex);
                        Children[1] = new Edge(middle, Maximum, null, null);
                    }
                }
                else
                {
                    Children[0] = new Edge(Minimum, middle, null, null);
                    Children[1] = new Edge(middle, Maximum, null, null);
                }
            }

            return Children[childIndex];
        }

        public IEnumerable<OrientedFunctionValue> GetFunctionValues(EdgeOrientation orientation)
        {
            yield return new OrientedFunctionValue(Minimum, orientation.GetValueOrientation(0));
            yield return new OrientedFunctionValue(Maximum, orientation.GetValueOrientation(1));
        }

        public override string ToString()
        {
            return $"{{edge: {Minimum?.ToString()}-{Maximum?.ToString()}}}";
        }

        /// <summary>
        /// Select the most complete data from edge and its children
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static Edge Merge(IEnumerable<Edge> edges)
        {
            return MergeNonNullEdges(edges.Where(e => e != null));
        }

        private static Edge MergeNonNullEdges(IEnumerable<Edge> edges)
        {
            var edge = edges.FirstOrDefault(e => e.IsComplete);

            if (edge != null)
            {
                return edge;
            }
            else if (edges.Any())
            {
                List<Edge> getChildren(int index) => edges.Select(e => e.GetChild(index)).ToList();

                Edge child0 = Merge(getChildren(0));
                Edge child1 = Merge(getChildren(1));

                //Only create an edge if there is any information that needs to be wrapped
                if (child0 != null || child1 != null)
                {
                    return new Edge(child0, child1);
                }
            }

            return null;
        }
    }
}
