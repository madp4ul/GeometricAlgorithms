using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    /// <summary>
    /// Contains a function result to be delegated to the main thread
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class FunctionResult<T> : IWorkerProgressUpdate
    {
        public T Result;
        public Action<T> ResultListener;

        public FunctionResult(T result, Action<T> resultListener)
        {
            Result = result;
            ResultListener = resultListener ?? throw new ArgumentNullException(nameof(resultListener));
        }

        public void ExecuteUpdate()
        {
            ResultListener(Result);
        }
    }
}
