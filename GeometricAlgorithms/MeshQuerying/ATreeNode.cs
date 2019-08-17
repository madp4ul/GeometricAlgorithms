using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public abstract class ATreeNode
    {
        public int VertexCount { get; set; }
        public BoundingBox BoundingBox { get; private set; }

        public abstract int NodeCount { get; protected set; }
        public abstract int LeafCount { get; protected set; }

        protected ATreeNode(BoundingBox boundingBox, int verticesCount)
        {
            VertexCount = verticesCount;
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
        }


        internal abstract void FindInRadius(InRadiusQuery query);

        internal abstract void FindNearestVertices(NearestVerticesQuery query);

        internal abstract void AddBranches(List<ATreeBranch> branches);

        internal abstract void AddLeaves(List<TreeLeaf> leaves);
    }
}
