using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.Forms
{
    public partial class TreeNavigationForm : Form
    {
        private readonly TreeEnumerationModel TreeEnumeration;

        private Button[] ChildButtons;

        public TreeNavigationForm(TreeEnumerationModel treeEnumeration)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                if (!treeEnumeration.HasEnumerator)
                {
                    throw new InvalidOperationException();
                }

                TreeEnumeration = treeEnumeration;
                TreeEnumeration.Updated += TreeEnumeration_Update;

                TreeEnumeration_Update();
            }
        }

        private void TreeEnumeration_Update()
        {
            TreeEnumeration.SelectRoot();
            RefreshControls();
        }

        private void TreeNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TreeEnumeration.Updated -= TreeEnumeration_Update;
        }

        private void RefreshControls()
        {
            SetParentEnabled();
            SetCurrentText();
            RegenerateChildren();
        }

        private void SetCurrentText()
        {
            lbCurrent.Text = TreeEnumeration.CurrentName;
        }

        private void SetParentEnabled()
        {
            btnToParent.Enabled = TreeEnumeration.CanSelectParent;
        }

        private void RegenerateChildren()
        {
            if (ChildButtons != null)
            {
                pnChildren.Controls.Clear();

                foreach (var childButton in ChildButtons)
                {
                    childButton.Dispose();
                }
            }

            ChildButtons = new Button[TreeEnumeration.ChildCount];

            const int btnHeight = 25;
            const int btnMargin = 10;

            for (int i = 0; i < ChildButtons.Length; i++)
            {
                var btn = new Button
                {
                    Text = $"Select child {i}",
                    Location = new Point(0, (btnMargin + btnHeight) * i),
                    Height = btnHeight,
                    Width = pnChildren.Width,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                };

                int childId = i;

                btn.Click += (o, e) =>
                {
                    TreeEnumeration.SelectChild(childId);
                    RefreshControls();
                };

                ChildButtons[i] = btn;
                pnChildren.Controls.Add(btn);
            }

        }

        private void BtnToRoot_Click(object sender, EventArgs e)
        {
            TreeEnumeration.SelectRoot();

            RefreshControls();
        }

        private void BtnToParent_Click(object sender, EventArgs e)
        {
            TreeEnumeration.SelectParent();

            RefreshControls();
        }
    }
}
