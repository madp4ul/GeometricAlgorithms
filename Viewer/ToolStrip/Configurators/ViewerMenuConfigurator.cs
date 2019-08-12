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
            var showNormals = GetMenu(menu.DropDownItems, "showNormalsToolStripMenuItem");
            var showFaces = GetMenu(menu.DropDownItems, "showFacesToolStripMenuItem");

            var showFaceApproximatedNormals = GetMenu(menu.DropDownItems, "showNormalApproximationToolStripMenuItem");

            var showFacesAsWireframe = GetMenu(showFaces.DropDownItems, "showFacesAsWireframeToolStripMenuItem");

            var showApproximatedFaces = GetMenu(menu.DropDownItems, "showFaceApproximationToolStripMenuItem");
            var showApproximatedFacesAsWireframe = GetMenu(showApproximatedFaces.DropDownItems, "showFaceApproximationAsWireframeToolStripMenuItem");

            //Set enabled on open menu
            menu.DropDownOpening += (o, e) =>
            {
                showNormals.Enabled = viewerMenuTools.EnableOptionSetNormalVisiblity();
                showFaceApproximatedNormals.Enabled = viewerMenuTools.EnableOptionSetApproximatedNormalVisiblity();
                showFaces.Enabled = viewerMenuTools.EnableOptionSetOriginalFacesVisiblity();
                showApproximatedFaces.Enabled = viewerMenuTools.EnableOptionSetApproximatedFacesVisibility();
            };

            //Configure action on click
            MakeClickToggle(showPositions, viewerMenuTools.SetPointCloudVisibility);

            MakeClickToggle(showNormals, viewerMenuTools.SetNormalVisiblity);
            MakeClickToggle(showFaceApproximatedNormals, viewerMenuTools.SetApproximatedNormalVisiblity);

            MakeClickToggle(showFaces, viewerMenuTools.SetOriginalFacesVisiblity);
            MakeClickToggle(showFacesAsWireframe, viewerMenuTools.SetDrawOriginalFacesAsWireframe);

            MakeClickToggle(showApproximatedFaces, viewerMenuTools.SetApproximatedFacesVisiblity);
            MakeClickToggle(showApproximatedFacesAsWireframe, viewerMenuTools.SetDrawApproximatedFacesAsWireframe);
        }
    }
}
