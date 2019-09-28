using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Tasks
{
    /// <summary>
    /// Can execute functions
    /// </summary>
    public interface IFuncExecutor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <param name="statusUpdater"></param>
        /// <returns></returns>
        IFuncExecution<T> Execute<T>(Func<IProgressUpdater, T> function);

        bool IsWorking { get; }
        int QueuedExecutionCount { get; }
    }
}
