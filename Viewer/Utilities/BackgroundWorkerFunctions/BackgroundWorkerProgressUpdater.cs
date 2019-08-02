using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    /// <summary>
    /// Updates status to worker so that the worker can inform the main thread about the update with the 
    /// original updater. Also makes a difference between task update and task complete (which are both processed as progress)
    /// </summary>
    class BackgroundWorkerProgressUpdater : IProgressUpdater
    {
        public BackgroundWorker Worker { get; set; }

        /// <summary>
        /// The status updater of the currently executed task
        /// </summary>
        public IProgressUpdater CurrentProgressUpdater { get; set; }

        public BackgroundWorkerProgressUpdater(BackgroundWorker worker)
        {
            Worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public virtual void OnFunctionFinished<T>(T result, Action<T> resultListener)
        {
            Worker.ReportProgress(100, new FunctionResult<T>(result, resultListener));
        }

        public virtual void UpdateStatus(int percentDone, string message = null)
        {
            Worker.ReportProgress(0, new FunctionProgress(CurrentProgressUpdater, percentDone, message));
        }
    }
}
