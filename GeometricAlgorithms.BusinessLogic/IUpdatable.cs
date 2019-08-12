using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic
{
    public interface IUpdatable<T>
    {
        /// <summary>
        /// Is called after update completed
        /// </summary>
        event Action Updated;

        /// <summary>
        /// Updates the updatable with new data
        /// </summary>
        /// <param name="data"></param>
        void Update(T data);
    }

    public interface IUpdatable
    {
        /// <summary>
        /// Is called after update completed
        /// </summary>
        event Action Updated;

        /// <summary>
        /// Updates the updatable with new data
        /// </summary>
        /// <param name="data"></param>
        void Update();
    }
}
