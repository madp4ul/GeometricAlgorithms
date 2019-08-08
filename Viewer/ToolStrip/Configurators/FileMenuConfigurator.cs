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
    class FileMenuConfigurator : MenuConfigurator
    {
        public FileMenuConfigurator(ModelData model) : base(model)
        {
        }

        public override void Configure(MenuStrip menuStrip)
        {
            var fileMenuTools = new FileMenuTools(Model);

            var menu = GetMenu(menuStrip.Items, "fileToolStripMenuItem");

            MakeClickAction(GetMenu(menu.DropDownItems, "openFileToolStripMenuItem"), fileMenuTools.OpenFile);
            MakeClickAction(GetMenu(menu.DropDownItems, "saveFileToolStripMenuItem"), fileMenuTools.SaveToFile);
        }
    }
}
