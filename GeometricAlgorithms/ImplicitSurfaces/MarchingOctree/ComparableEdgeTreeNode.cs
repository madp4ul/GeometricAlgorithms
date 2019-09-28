using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class ComparableEdgeTreeNode<TCompare> : IComparable<ComparableEdgeTreeNode<TCompare>> where TCompare : IComparable<TCompare>
    {
        private readonly Func<EdgeTreeNode, TCompare> GetComparationFeature;

        internal readonly EdgeTreeNode Node;
        public TCompare ComparationFeature { get; private set; }

        internal ComparableEdgeTreeNode(EdgeTreeNode node, Func<EdgeTreeNode, TCompare> getComparationFeature)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            GetComparationFeature = getComparationFeature;
            ComparationFeature = getComparationFeature(node);
        }

        public void RecalculatePriority()
        {
            ComparationFeature = GetComparationFeature(Node);
        }

        public int CompareTo(ComparableEdgeTreeNode<TCompare> other)
        {
            return ComparationFeature.CompareTo(other.ComparationFeature);
        }

        public override string ToString()
        {
            return $"{{comparable node: {ComparationFeature.ToString()}, {Node.ToString()}}}";
        }
    }
}
