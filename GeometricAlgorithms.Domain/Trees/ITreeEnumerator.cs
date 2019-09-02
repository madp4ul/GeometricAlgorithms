using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Trees
{
    public interface ITreeEnumerator
    {
        ITreeNode Current { get; }

        bool MoveToRoot();

        bool MoveToChild(int childIndex);
        bool MoveToParent();
    }
}
