using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public abstract class ATreeNode : ITreeNode
    {
        public int Depth { get; private set; }
        public int VertexCount { get; private set; }
        public BoundingBox BoundingBox { get; private set; }

        public abstract int NodeCount { get; protected set; }
        public abstract int LeafCount { get; protected set; }

        public abstract int ChildCount { get; }

        public readonly ATreeNode Parent;
        public bool HasParent => Parent != null;

        protected ATreeNode(ATreeNode parent, BoundingBox boundingBox, int verticesCount, int depth)
        {
            Parent = parent;
            VertexCount = verticesCount;
            Depth = depth;
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
        }


        internal abstract void FindInRadius(InRadiusQuery query);

        internal abstract void FindNearestVertices(NearestVerticesQuery query);

        internal abstract void AddBranches(List<ATreeBranch> branches);

        internal abstract void AddLeaves(List<TreeLeaf> leaves);
    }
}
