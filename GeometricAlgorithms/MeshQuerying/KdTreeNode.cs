﻿using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    abstract class KdTreeNode
    {
        public int VertexCount { get; set; }

        public BoundingBox BoundingBox { get; private set; }

        public abstract int NodeCount { get; protected set; }
        public abstract int LeafCount { get; protected set; }

        protected KdTreeNode(BoundingBox boundingBox, int verticesCount)
        {
            VertexCount = verticesCount;
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
        }

        public abstract void FindInRadius(InRadiusQuery query);

        public abstract void FindNearestVertices(NearestVerticesQuery query);

        public abstract void AddBranches(List<KdTreeBranch> branches);

        public abstract void AddLeaves(List<KdTreeLeaf> leaves);
    }
}
