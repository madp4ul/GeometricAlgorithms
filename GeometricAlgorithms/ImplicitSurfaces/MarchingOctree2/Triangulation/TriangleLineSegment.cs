using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation
{
    class TriangleLineSegment
    {
        public readonly TriangleLineSegmentNode First;
        public readonly TriangleLineSegmentNode Last;

        public TriangleLineSegment(TriangleLineSegmentNode first, TriangleLineSegmentNode last)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Last = last ?? throw new ArgumentNullException(nameof(last));
        }

        public bool IsCircle => First.VertexIndex == Last.VertexIndex;

        public static List<TriangleLineSegment> Merge(List<TriangleLineSegment> segments)
        {
            var merged = new List<TriangleLineSegment>();

            //Add circles
            for (int i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                if (segment.IsCircle)
                {
                    merged.Add(segment);
                    segments.RemoveAt(i);
                    i--;
                }
            }

            //Merge non-circles segments
            for (int i = 0; i < segments.Count; i++)
            {
                var firstSegment = segments[i];

                for (int j = i + 1; j < segments.Count; j++)
                {
                    var secondSegment = segments[j];

                    void addCombinedSegment(TriangleLineSegment combinedSegment)
                    {
                        if (combinedSegment.IsCircle)
                        {
                            merged.Add(combinedSegment);
                        }
                        else
                        {
                            segments.Add(combinedSegment);
                        }

                        segments.RemoveAt(j);
                        segments.RemoveAt(i);
                        i--;
                    }

                    if (firstSegment.Last.VertexIndex == secondSegment.First.VertexIndex)
                    {
                        firstSegment.Last.Next = secondSegment.First.Next;

                        //Replace segments with combination of them and break from current iteration
                        addCombinedSegment(new TriangleLineSegment(firstSegment.First, secondSegment.Last));
                        break;
                    }

                    if (secondSegment.Last.VertexIndex == firstSegment.First.VertexIndex)
                    {
                        secondSegment.Last.Next = firstSegment.First.Next;
                        addCombinedSegment(new TriangleLineSegment(secondSegment.First, firstSegment.Last));
                        break;
                    }
                }
            }

            merged.AddRange(segments);

            return merged;
        }
    }
}
