using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    /// <summary>
    /// Contains a function to be executed on progress changes
    /// </summary>
    interface IWorkerProgressUpdate
    {
        void ExecuteUpdate();
    }
}
