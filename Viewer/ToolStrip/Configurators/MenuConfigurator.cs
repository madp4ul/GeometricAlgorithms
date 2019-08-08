using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.ToolStrip.Configurators
{
    abstract class MenuConfigurator
    {
        protected readonly ModelData Model;

        public MenuConfigurator(ModelData model)
        {
            Model = model;
        }

        public abstract void Configure(MenuStrip menuStrip);

        protected void MakeClickToggle(ToolStripMenuItem menuItem, Action<bool> toToggle)
        {
            toToggle(menuItem.Checked);

            menuItem.Click += (o, e) =>
            {
                ToolStripMenuItem sender = (ToolStripMenuItem)o;
                sender.Checked = !sender.Checked;

                toToggle(sender.Checked);
            };
        }

        protected void MakeClickAction(ToolStripMenuItem menuItem, Action action)
        {
            menuItem.Click += (o, e) => action();
        }

        protected ToolStripItem GetItem(ToolStripItemCollection collection, string name)
        {
            return collection[name] ?? throw new NotImplementedException(
                "Menustrip item not found. Name probably changed in designer.");
        }

        protected ToolStripMenuItem GetMenu(ToolStripItemCollection collection, string name)
        {
            var item = GetItem(collection, name);
            return item as ToolStripMenuItem ?? throw new NotImplementedException(
                "Item was found but it was no MenuItem. Something probably changed in designer.");
        }
    }
}
