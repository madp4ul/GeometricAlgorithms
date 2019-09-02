using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Trees
{
    public interface ITreeNode
    {
        BoundingBox BoundingBox { get; }

        int ChildCount { get; }
        bool HasParent { get; }

        string ToString();
    }
}
