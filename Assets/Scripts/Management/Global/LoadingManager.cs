using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Management.Global
{
    public class LoadingManager
    {
        private List<AsyncOperation> operations;
        private List<IGameplayManager> managers;
        private GameObject loadingScreen;

        public LoadingManager(GameObject loadingScreen)
        {
            operations = new List<AsyncOperation>();
            managers = new List<IGameplayManager>();
            this.loadingScreen = loadingScreen;
        }

        public IEnumerator CheckSceneLoadingProgress()
        {
            foreach (AsyncOperation operation in operations)
            {
                while (!operation.isDone)
                {
                    yield return null;
                }
            }

            yield return CheckInitializationProgress();
        }

        public IEnumerator CheckInitializationProgress()
        {
            foreach(IGameplayManager m in managers)
            {
                while (!m.IsDone)
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

        public void AddGameplayManager(IGameplayManager manager)
        {
            managers.Add(manager);
        }

        public void ClearOperationsList()
        {
            operations.Clear();
        }

        public void ClearManagersList()
        {
            managers.Clear();
        }
    }
}
