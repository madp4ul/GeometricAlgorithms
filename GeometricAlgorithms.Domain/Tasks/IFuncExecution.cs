using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Tasks
{
    /// <summary>
    /// Information about the execution of a function by a FuncExecutor
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IFuncExecution<TResult>
    {
        bool IsStarted { get; }
        bool IsDone { get; }

        /// <summary>
        /// Gets called with result value when func is done
        /// </summary>
        event Action<TResult> FuncDone;

        /// <summary>
        /// Contains the result value if execution is done
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Guarantees that the arrival of a value will not be missed
        /// </summary>
        /// <param name="action"></param>
        void CallWithResult(Action<TResult> action);
    }


}
