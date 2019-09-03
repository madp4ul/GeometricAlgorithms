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
            return Workspace.Normals.HasNormals;
        }

        public bool EnableOptionSetApproximatedNormalVisiblity()
        {
            return Workspace.ApproximatedNormals.Normals.HasNormals;
        }

        public bool EnableOptionSetOriginalFacesVisiblity()
        {
            return Workspace.Faces.HasFaces;
        }

        public bool EnableOptionSetApproximatedFacesVisibility()
        {
            return Workspace.ApproximatedFaces.HasFaces;
        }

        public void SetApproximatedFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.ApproximatedFaces.DrawFaces = showOriginalFaces;
        }

        public void SetDrawApproximatedFacesAsWireframe(bool showWireframe)
        {
            Workspace.ApproximatedFaces.DrawAsWireframe = showWireframe;
        }

        public void SetOriginalFacesVisiblity(bool showOriginalFaces)
        {
            Workspace.Faces.DrawFaces = showOriginalFaces;
        }

        public void SetDrawOriginalFacesAsWireframe(bool showWireframe)
        {
            Workspace.Faces.DrawAsWireframe = showWireframe;
        }

        public void SetPointCloudVisibility(bool showPointCloud)
        {
            Workspace.Positions.DrawMeshPositions = showPointCloud;
        }

        public void SetNormalVisiblity(bool showNormals)
        {
            Workspace.Normals.DrawNormals = showNormals;
        }

        public void SetApproximatedNormalVisiblity(bool showNormals)
        {
            Workspace.ApproximatedNormals.Normals.DrawNormals = showNormals;
        }
    }
}
