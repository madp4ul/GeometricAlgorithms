﻿using System;
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
            ClearPreviousMerges(segments);
            MoveCircles(from: segments, to: merged);

            //Merge non-circles segments
            for (int i = 0; i < segments.Count; i++)
            {
                var segment1 = segments[i];

                for (int j = i + 1; j < segments.Count; j++)
                {
                    var segment2 = segments[j];

                    if (segment1.Last.VertexIndex == segment2.First.VertexIndex)
                    {
                        combineSegments(segment1, segment2);
                        break;
                    }

                    if (segment2.Last.VertexIndex == segment1.First.VertexIndex)
                    {
                        combineSegments(segment2, segment1);
                        break;
                    }

                    void combineSegments(TriangleLineSegment first, TriangleLineSegment second)
                    {
                        //Replace segments with combination of them and break from current iteration
                        MergedTriangleLineSegment combinedSegment = Connect(first, second);
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
                        i--; //TODO check: does it change the outside captured variable? (it has to)
                    }
                }
            }

            merged.AddRange(segments);

            return merged;
        }

        private static MergedTriangleLineSegment Connect(TriangleLineSegment first, TriangleLineSegment second)
        {
            //skip one of duplicate nodes
            TriangleLineSegmentNode.Connect(first.Last, second.First.Next);

            var mergedNodes = new List<TriangleLineSegmentNode>();

            if (first is MergedTriangleLineSegment mergedFirst)
            {
                mergedNodes.AddRange(mergedFirst.MergedNodes);
            }
            if (second is MergedTriangleLineSegment mergedSecond)
            {
                mergedNodes.AddRange(mergedSecond.MergedNodes);
            }
            mergedNodes.Add(first.Last);

            //Replace segments with combination of them and break from current iteration
            return new MergedTriangleLineSegment(first.First, second.Last, mergedNodes);
        }

        private static void ClearPreviousMerges(List<TriangleLineSegment> segments)
        {
            foreach (var segmentWidthpreviousMerges in segments.OfType<MergedTriangleLineSegment>())
            {
                segmentWidthpreviousMerges.MergedNodes.Clear();
            }
        }

        private static void MoveCircles(List<TriangleLineSegment> from, List<TriangleLineSegment> to)
        {
            //Add circles
            for (int i = 0; i < from.Count; i++)
            {
                var segment = from[i];
                if (segment.IsCircle)
                {
                    to.Add(segment);
                    from.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
