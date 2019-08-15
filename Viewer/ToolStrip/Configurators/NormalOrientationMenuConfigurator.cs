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
    class NormalOrientationMenuConfigurator : MenuConfigurator
    {
        public NormalOrientationMenuConfigurator(ModelData model) : base(model)
        {
        }

        public override void Configure(MenuStrip menuStrip)
        {
            var tools = new NormalOrientationTools(Model.Workspace);

            var menu = GetMenu(menuStrip.Items, "normalOrientationToolStripMenuItem");

            var autoOrientation = GetMenu(menu.DropDownItems, "automaticNormalOrientationToolStripMenuItem");
            var mirrorNormals = GetMenu(menu.DropDownItems, "mirrorNormalsToolStripMenuItem");

            menu.DropDownOpening += (o, e) =>
            {
                mirrorNormals.Enabled = tools.EnableMirrorNormals();
            };

            MakeClickToggle(autoOrientation, tools.SetAutoCalculateOrientation);
            MakeClickAction(mirrorNormals, tools.MirrorNormals);
        }
    }
}
