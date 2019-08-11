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
    class ApproximationConfigurator : MenuConfigurator
    {
        private readonly MainWindow MainWindow;

        public ApproximationConfigurator(ModelData model, MainWindow mainWindow) : base(model)
        {
            MainWindow = mainWindow;
        }

        public override void Configure(MenuStrip menuStrip)
        {
            var approximationTools = new ApproximationTools(Model.Workspace.PointData);

            var approximationMenu = GetMenu(menuStrip.Items, "approximationToolStripMenuItem");
            var normalMenu = GetMenu(approximationMenu.DropDownItems, "normalApproximationToolStripMenuItem");
            ConfigureNormalMenu(normalMenu, approximationTools);

            var facesMenu = GetMenu(approximationMenu.DropDownItems, "approximateFacesToolStripMenuItem");
            ConfigureFacesMenu(facesMenu, approximationTools);
        }

        private void ConfigureNormalMenu(ToolStripMenuItem normalMenu, ApproximationTools approximationTools)
        {
            var approximateNormalsFromFaces = GetMenu(normalMenu.DropDownItems, "approximateFromFacesToolStripMenuItem");

            normalMenu.DropDownOpening += (o, e) =>
            {
                approximateNormalsFromFaces.Enabled = approximationTools.EnableApproximateNormalsFromFaces();
            };

            MakeClickAction(approximateNormalsFromFaces, approximationTools.ApproximateNormalsFromFaces);
        }

        private void ConfigureFacesMenu(ToolStripMenuItem facesMenu, ApproximationTools approximationTools)
        {
            var approximateFaces = GetMenu(facesMenu.DropDownItems, "approximateFacesClickToolStripMenuItem");

            facesMenu.DropDownOpening += (o, e) =>
            {
                approximateFaces.Enabled = approximationTools.EnableApproximateFaces();
            };

            MakeClickAction(approximateFaces, () => approximationTools.ApproximateFaces(MainWindow));
        }
    }
}
