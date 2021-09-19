using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Management.Global
{
    public class LoadingManager
    {
        private List<AsyncOperation> operations;
        private GameObject loadingScreen;

        public LoadingManager(GameObject loadingScreen)
        {
            operations = new List<AsyncOperation>();
            this.loadingScreen = loadingScreen;
        }

        public IEnumerator CheckProgress()
        {
            foreach (AsyncOperation operation in operations)
            {
                while (!operation.isDone)
                {
                    yield return null;
                }
            }

            loadingScreen.SetActive(false);
        }

        public void AddAsyncOperation(AsyncOperation operation)
        {
            operations.Add(operation);
        }

        public void ClearOperationsList()
        {
            operations.Clear();
        }
    }
}
