using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Tasks
{
    /// <summary>
    /// Can be informed about progress of a task
    /// </summary>
    public interface IProgressUpdater
    {
        /// <summary>
        /// Send Updates about the status of the execution
        /// </summary>
        /// <param name="percentDone">Value from 0 for not done to 100 for done</param>
        /// <param name="message">A text message</param>
        void UpdateStatus(int percentDone, string message = null);
    }
}
