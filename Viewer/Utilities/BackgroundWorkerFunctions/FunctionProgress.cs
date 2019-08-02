using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    /// <summary>
    /// Contains a function progress update to be delegated to the main thread
    /// </summary>
    class FunctionProgress : IWorkerProgressUpdate
    {
        public IProgressUpdater StatusUpdater;
        public int PercentDone;
        public string Message;

        public FunctionProgress(IProgressUpdater statusUpdater, int percentDone, string message)
        {
            StatusUpdater = statusUpdater ?? throw new ArgumentNullException(nameof(statusUpdater));
            PercentDone = percentDone;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public void ExecuteUpdate()
        {
            StatusUpdater?.UpdateStatus(PercentDone, Message);
        }
    }
}
