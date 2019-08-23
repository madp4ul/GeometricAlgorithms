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

        /// <summary>
        /// Index of Vertex on this edge. only has a value
        /// if function values cross zero (surface) on this edge
        /// </summary>
        public readonly int? VertexIndex;

        private readonly Edge[] Children;

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
                VertexIndex = result.AddPosition(vertex);
            }
        }

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

        private Edge(FunctionValue start, FunctionValue end, float? interpolationValue, int? vertexIndex)
            : this(start, end)
        {
            Interpolation = interpolationValue;
            VertexIndex = vertexIndex;
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
                        Children[1] = new Edge(middle, Maximum, 2 * (Interpolation.Value - 0.5f), VertexIndex.Value);
                    }
                    else
                    {
                        Children[0] = new Edge(Minimum, middle, 2 * (Interpolation.Value), VertexIndex.Value);
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

        public static Edge Merge(IEnumerable<Edge> edges)
        {
            var edge = edges.FirstOrDefault(e => e.IsComplete);

            if (edge != null)
            {
                return edge;
            }

            Edge child0 = Merge(edges.Select(e => e.GetChild(0)).ToList());
            Edge child1 = Merge(edges.Select(e => e.GetChild(1)).ToList());

            return new Edge(child0, child1);
        }
    }
}
