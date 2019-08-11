﻿using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
{
    class ViewerMenuTools
    {
        public Workspace Workspace { get; set; }

        public ViewerMenuTools(Workspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public bool EnableOptionSetNormalVisiblity()
        {
            return Workspace.PointData.NormalData.HasNormals;
        }

        public bool EnableOptionSetApproximatedNormalVisiblity()
        {
            return Workspace.PointData.FaceApproximatedNormalData.NormalData.HasNormals;
        }

        public bool EnableOptionSetOriginalFacesVisiblity()
        {
            return Workspace.PointData.FaceData.HasFaces;
        }

        public bool EnableOptionSetApproximatedFacesVisibility()
        {
            return Workspace.PointData.KdTreeData.ApproximatedFaceData.FaceData.HasFaces;
        }

        public void SetApproximatedFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.PointData.KdTreeData.ApproximatedFaceData.FaceData.DrawFaces = showOriginalFaces;
        }

        public void SetDrawApproximatedFacesAsWireframe(bool showWireframe)
        {
            Workspace.PointData.KdTreeData.ApproximatedFaceData.FaceData.DrawAsWireframe = showWireframe;
        }

        public void SetOriginalFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.PointData.FaceData.DrawFaces = showOriginalFaces;
        }

        public void SetDrawOriginalFacesAsWireframe(bool showWireframe)
        {
            Workspace.PointData.FaceData.DrawAsWireframe = showWireframe;
        }

        public void SetPointCloudVisibility(bool showPointCloud)
        {
            Workspace.PointData.DrawMeshPositions = showPointCloud;
        }

        public void SetNormalVisiblity(bool showNormals)
        {
            Workspace.PointData.NormalData.DrawNormals = showNormals;
        }

        public void SetApproximatedNormalVisiblity(bool showNormals)
        {
            Workspace.PointData.FaceApproximatedNormalData.NormalData.DrawNormals = showNormals;
        }
    }
}
