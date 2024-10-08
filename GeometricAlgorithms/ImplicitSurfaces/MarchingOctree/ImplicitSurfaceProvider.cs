﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.FunctionValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    /// <summary>
    /// Used to get function values and track which values were computed
    /// </summary>
    public class ImplicitSurfaceProvider
    {
        private readonly IImplicitSurface ImplicitSurface;

        private readonly List<FunctionValue> FunctionValues = new List<FunctionValue>();

        public int FunctionValueCount => FunctionValues.Count;

        public ImplicitSurfaceProvider(IImplicitSurface implicitSurface)
        {
            ImplicitSurface = implicitSurface ?? throw new ArgumentNullException(nameof(implicitSurface));
        }

        public FunctionValue CreateFunctionValue(Vector3 position)
        {
            float distance = ImplicitSurface.GetApproximateSurfaceDistance(position);

            FunctionValue value = new FunctionValue(position, distance);
            lock (FunctionValues)
            {
                FunctionValues.Add(value);
            }
            return value;
        }

        public FunctionValue[] GetFunctionValues()
        {
            lock (FunctionValues)
            {
                return FunctionValues.ToArray();
            }
        }
    }
}
