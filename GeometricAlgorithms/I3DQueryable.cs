﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms
{
    public interface I3DQueryable<TVertex> where TVertex : IVertex
    {
        SortedList<float, TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount);
        List<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius);
    }

    //Default generic param
    public interface I3DQueryable : I3DQueryable<Vertex>
    {
    }
}
