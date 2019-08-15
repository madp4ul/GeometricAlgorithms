using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.NormalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
{
    class NormalOrientationTools
    {
        public readonly Workspace Workspace;
        public NormalOrientationModel NormalOrientation => Workspace.NormalOrientation;

        public bool AutoCalculateOrientation { get; private set; }

        public NormalOrientationTools(Workspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));

            NormalOrientation.Updated += NormalOrientation_Updated;
        }

        public void SetAutoCalculateOrientation(bool enable)
        {
            AutoCalculateOrientation = enable;
        }

        private void NormalOrientation_Updated()
        {
            if (AutoCalculateOrientation && NormalOrientation.CanCalculateOrientation)
            {
                NormalOrientation.CalculateIsNormalOrientationOutwards(areOutwards =>
                {
                    if (!areOutwards)
                    {
                        MirrorNormals();
                    }
                });
            }
        }

        public bool EnableMirrorNormals()
        {
            return !AutoCalculateOrientation && NormalOrientation.CanMirrorNormals;
        }

        public void MirrorNormals()
        {
            Workspace.Update(NormalOrientation.CreateMeshWithMirrorNormals());
        }
    }
}
