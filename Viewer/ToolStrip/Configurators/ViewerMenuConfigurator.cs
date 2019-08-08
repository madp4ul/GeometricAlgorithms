using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.ToolStrip.Tools;

namespace GeometricAlgorithms.Viewer.ToolStrip.Configurators
{
    class ViewerMenuConfigurator : MenuConfigurator
    {
        public ViewerMenuConfigurator(ModelData model) : base(model)
        {
        }

        public override void Configure(MenuStrip menuStrip)
        {
            var viewerMenuTools = new ViewerMenuTools(Model.Workspace);

            var menu = GetMenu(menuStrip.Items, "viewerToolStripMenuItem");

            //Get Menu Items
            var showPositions = GetMenu(menu.DropDownItems, "showPointCloudToolStripMenuItem");
            var showNormals = GetMenu(menu.DropDownItems, "showOriginalNormalsToolStripMenuItem");
            var showFaceApproximatedNormals = GetMenu(menu.DropDownItems, "showFaceApproximatedNormalsToolStripMenuItem");

            var showFaces = GetMenu(menu.DropDownItems, "showOriginalMeshToolStripMenuItem");
            var showFacesAsWireframe = GetMenu(showFaces.DropDownItems, "showOriginalFacesAsWireframeToolStripMenuItem");

            //Set enabled on open menu
            menu.DropDownOpening += (o, e) =>
            {
                showNormals.Enabled = viewerMenuTools.EnableOptionSetNormalVisiblity();
                showFaceApproximatedNormals.Enabled = viewerMenuTools.EnableOptionSetApproximatedNormalVisiblity();
                showFaces.Enabled = viewerMenuTools.EnableOptionSetOriginalFacesVisiblity();
            };

            //Configure action on click
            MakeClickToggle(showPositions, viewerMenuTools.SetPointCloudVisibility);
            MakeClickToggle(showNormals, viewerMenuTools.SetNormalVisiblity);
            MakeClickToggle(showFaceApproximatedNormals, viewerMenuTools.SetApproximatedNormalVisiblity);
            MakeClickToggle(showFaces, viewerMenuTools.SetOriginalFacesVisiblity);
            MakeClickToggle(showFacesAsWireframe, viewerMenuTools.SetDrawOriginalFacesAsWireframe);
        }
    }
}
