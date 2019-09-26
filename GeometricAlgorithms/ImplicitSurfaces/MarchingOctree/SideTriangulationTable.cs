using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    static class SideTriangulationTable
    {
        private static readonly LineSegmentDefinition[][] ValuesByFunctionValueIndex = new LineSegmentDefinition[16][]
        {
  /*0  */          new LineSegmentDefinition[0],

  /*1  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,0)) },
  /*2  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,1)) },
  /*3  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(0,1)) },
  /*4  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,1)) },
  /*5  */          new LineSegmentDefinition[2]
                   {
                       new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,0)),
                       new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,1))
                   },
  /*6  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(1,1)) },
  /*7  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,1)) },
  /*8  */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,1)) },

  /*9 */           new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(1,1)) },
  /*10 */          new LineSegmentDefinition[2]
                   {
                       new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,1)),
                       new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,1))
                   },
  /*11 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,1),new SideEdgeIndex(1,1)) },
  /*12 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(0,1)) },
  /*13 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(1,0),new SideEdgeIndex(0,1)) },
  /*14 */          new LineSegmentDefinition[1]{ new LineSegmentDefinition(new SideEdgeIndex(0,0),new SideEdgeIndex(1,0)) },
  /*15 */          new LineSegmentDefinition[0],
        };

        private static readonly LineSegmentDefinition[][] ValuesByEdgeValueIndex = new LineSegmentDefinition[16][]
{
  /*0  */          ValuesByFunctionValueIndex[0],                  
  /*1  */          null,
  /*2  */          null,
  /*3  */          ValuesByFunctionValueIndex[2],
  /*4  */          null,
  /*5  */          ValuesByFunctionValueIndex[6],
  /*6  */          ValuesByFunctionValueIndex[4],
  /*7  */          null,
  /*8  */          null,
  /*9 */           ValuesByFunctionValueIndex[1],
  /*10 */          ValuesByFunctionValueIndex[3],
  /*11 */          null,
  /*12 */          ValuesByFunctionValueIndex[8],
  /*13 */          null,
  /*14 */          null,
  /*15 */          ValuesByFunctionValueIndex[5]
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

        public static LineSegmentDefinition[] GetDefinitionByEdge(
            bool hasIntersection00,
            bool hasIntersection10,
            bool hasIntersection01,
            bool hasIntersection11)
        {
            int index = 0;
            index |= hasIntersection10 ? 1 : 0;
            index |= hasIntersection01 ? 2 : 0;
            index |= hasIntersection11 ? 4 : 0;
            index |= hasIntersection00 ? 8 : 0;

            return ValuesByEdgeValueIndex[index];
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
