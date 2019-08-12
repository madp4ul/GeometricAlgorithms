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
            var facesMenu = GetMenu(approximationMenu.DropDownItems, "approximateFacesToolStripMenuItem");

            approximationMenu.DropDownOpening += (e, o) =>
            {
                normalMenu.Enabled = approximationTools.EnableApproximateNormalsFromFaces();
                facesMenu.Enabled = approximationTools.EnableApproximateFaces();
            };

            MakeClickAction(normalMenu, () => approximationTools.OpenNormalApproximationForm(MainWindow));
            MakeClickAction(facesMenu, () => approximationTools.OpenFaceApproximationForm(MainWindow));
        }
    }
}
