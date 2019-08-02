using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTreeProgressUpdater
    {
        private readonly IProgressUpdater Updater;

        private readonly string OperationDescription;

        private readonly int TotalOperations;
        private int OperationsCompleted;

        public KdTreeProgressUpdater(IProgressUpdater updater, int numberOfOperations, string operationDescription)
        {
            Updater = updater;
            OperationDescription = operationDescription;
            TotalOperations = numberOfOperations;
            OperationsCompleted = 0;
        }

        public void UpdateAddOperation(int operationCount = 1)
        {
            if (Updater != null)
            {
                OperationsCompleted += operationCount;

                //because TotalOperations isnt necessarily accurate we dont rely on it to be the max value
                int maxOperationCount = Math.Max(OperationsCompleted, TotalOperations);

                int percentDone = (int)(100 * (OperationsCompleted / (float)maxOperationCount));

                string message = $"{OperationsCompleted}/{maxOperationCount} {OperationDescription}";

                Updater.UpdateStatus(percentDone, message);
            }
        }

        public void IsCompleted()
        {
            Updater?.UpdateStatus(100, $"{OperationDescription} complete");
        }
    }
}
