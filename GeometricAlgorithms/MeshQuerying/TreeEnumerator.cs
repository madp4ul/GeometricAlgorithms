using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class TreeEnumerator : ITreeEnumerator
    {
        private readonly ATree Tree;

        private ATreeNode CurrentNode;
        private ATreeBranch CurrentBranch => CurrentNode as ATreeBranch;

        private bool CurrentNodeExists => CurrentNode != null;

        public TreeEnumerator(ATree tree)
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

            CurrentNode = branch.GetChildren().Skip(childIndex).First();

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
