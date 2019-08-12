using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.Viewer.Model;
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
            return Workspace.NormalData.HasNormals;
        }

        public bool EnableOptionSetApproximatedNormalVisiblity()
        {
            return Workspace.ApproximatedNormalData.NormalData.HasNormals;
        }

        public bool EnableOptionSetOriginalFacesVisiblity()
        {
            return Workspace.FaceData.HasFaces;
        }

        public bool EnableOptionSetApproximatedFacesVisibility()
        {
            return Workspace.ApproximatedFaceData.FaceData.HasFaces;
        }

        public void SetApproximatedFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.ApproximatedFaceData.FaceData.DrawFaces = showOriginalFaces;
        }

        public void SetDrawApproximatedFacesAsWireframe(bool showWireframe)
        {
            Workspace.ApproximatedFaceData.FaceData.DrawAsWireframe = showWireframe;
        }

        public void SetOriginalFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.FaceData.DrawFaces = showOriginalFaces;
        }

        public void SetDrawOriginalFacesAsWireframe(bool showWireframe)
        {
            Workspace.FaceData.DrawAsWireframe = showWireframe;
        }

        public void SetPointCloudVisibility(bool showPointCloud)
        {
            Workspace.PointData.DrawMeshPositions = showPointCloud;
        }

        public void SetNormalVisiblity(bool showNormals)
        {
            Workspace.NormalData.DrawNormals = showNormals;
        }

        public void SetApproximatedNormalVisiblity(bool showNormals)
        {
            Workspace.ApproximatedNormalData.NormalData.DrawNormals = showNormals;
        }
    }
}
