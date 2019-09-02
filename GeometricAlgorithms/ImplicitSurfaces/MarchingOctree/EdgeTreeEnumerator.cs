using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeEnumerator : ITreeEnumerator
    {
        private readonly EdgeTree Tree;

        private EdgeTreeNode CurrentNode;
        private EdgeTreeBranch CurrentBranch => CurrentNode as EdgeTreeBranch;

        private bool CurrentNodeExists => CurrentNode != null;

        public EdgeTreeEnumerator(EdgeTree tree)
        {
            Tree = tree ?? throw new ArgumentNullException(nameof(tree));
            MoveToRoot();
        }

        public ITreeNode Current => CurrentNode;

        public bool MoveToChild(int childIndex)
        {
            var branch = CurrentBranch;

            if (branch == null)
            {
                throw new InvalidOperationException();
            }

            CurrentNode = branch.Skip(childIndex).First();

            return CurrentNodeExists;
        }

        public bool MoveToParent()
        {
            if (!CurrentNodeExists)
            {
                throw new InvalidOperationException();
            }

            CurrentNode = CurrentNode.Parent;

            return CurrentNodeExists;
        }

        public bool MoveToRoot()
        {
            CurrentNode = Tree.Root;
            return CurrentNodeExists;
        }
    }
}
