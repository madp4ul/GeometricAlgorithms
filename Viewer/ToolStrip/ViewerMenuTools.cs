using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip
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
            return Workspace.PointData.ApproximatedNormalData.HasNormals;
        }

        public void SetPointCloudVisibility(bool showPointCloud)
        {
            Workspace.PointData.EnableDraw = showPointCloud;
        }

        public void SetNormalVisiblity(bool showNormals)
        {
            Workspace.PointData.NormalData.EnableDraw = showNormals;
        }

        public void SetApproximatedNormalVisiblity(bool showNormals)
        {
            Workspace.PointData.ApproximatedNormalData.EnableDraw = showNormals;
        }
    }
}
