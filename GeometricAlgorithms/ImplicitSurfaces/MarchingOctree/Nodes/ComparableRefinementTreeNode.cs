using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes
{
    class ComparableRefinementTreeNode<TCompare> : IComparable<ComparableRefinementTreeNode<TCompare>> where TCompare : IComparable<TCompare>
    {
        private readonly Func<RefinementTreeNode, TCompare> GetComparationFeature;

        internal readonly RefinementTreeNode Node;
        public TCompare ComparationFeature { get; private set; }

        internal ComparableRefinementTreeNode(RefinementTreeNode node, Func<RefinementTreeNode, TCompare> getComparationFeature)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            GetComparationFeature = getComparationFeature;
            ComparationFeature = getComparationFeature(node);
        }

        public void RecalculatePriority()
        {
            ComparationFeature = GetComparationFeature(Node);
        }

        public int CompareTo(ComparableRefinementTreeNode<TCompare> other)
        {
            return ComparationFeature.CompareTo(other.ComparationFeature);
        }

        public override string ToString()
        {
            return $"{{comparable node: {ComparationFeature.ToString()}, {Node.ToString()}}}";
        }
    }
}
