using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    /// <summary>
    /// Contains definitions of lines that seperate the corner values of a side into positive and negative areas.
    /// The direction of the lines is always so that the negative area is to their left and the positive area is to their right.
    /// </summary>
    static class SideTriangulationTable
    {
        private static readonly LineSegmentDefinition[][] ValuesByFunctionValueIndex = new LineSegmentDefinition[16][]
        {
  /*0  */          new LineSegmentDefinition[0],

  /*1  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,0)) },
  /*2  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,0)) },
  /*3  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(0,0)) },
  /*4  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,1),new SideEdgeIndex(0,1)) },
  /*5  */          new LineSegmentDefinition[2]
                   {
                       new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,0)),
                       new LineSegmentDefinition(new SideEdgeIndex(1,1),new SideEdgeIndex(0,1))
                   },
  /*6  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,1),new SideEdgeIndex(1,0)) },
  /*7  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,1),new SideEdgeIndex(0,0)) },
  /*8  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,1)) },

  /*9 */           new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(1,1)) },
  /*10 */          new LineSegmentDefinition[2]
                   {
                       new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,1)),
                       new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,0))
                   },
  /*11 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,1)) },
  /*12 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(0,1)) },
  /*13 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,1)) },
  /*14 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,0)) },
  /*15 */          new LineSegmentDefinition[0],
        };

        public static LineSegmentDefinition[] GetDefinitionByFunctionValue(bool is00Inside, bool is10Inside, bool is01Inside, bool is11Inside)
        {
            int index = 0;
            index |= is00Inside ? 1 : 0;
            index |= is10Inside ? 2 : 0;
            index |= is11Inside ? 4 : 0;
            index |= is01Inside ? 8 : 0;

            return ValuesByFunctionValueIndex[index];
        }
    }

    struct SideEdgeIndex
    {
        public readonly int DimensionIndex;
        public readonly int DirectionIndex;

        public SideEdgeIndex(int dimension0Index, int dimension1Index)
        {
            DimensionIndex = dimension0Index;
            DirectionIndex = dimension1Index;
        }
    }

    struct LineSegmentDefinition
    {
        public readonly SideEdgeIndex LineStart;
        public readonly SideEdgeIndex LineEnd;

        public LineSegmentDefinition(SideEdgeIndex lineStart, SideEdgeIndex lineEnd)
        {
            LineStart = lineStart;
            LineEnd = lineEnd;
        }
    }
}
