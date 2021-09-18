using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Management.Gameplay
{
    public class Context : MonoBehaviour
    {
        private List<IGameplayManager> gameplayManagers = new List<IGameplayManager>();

        private void Update()
        {
            foreach (IGameplayManager manager in gameplayManagers)
            {
                manager.Tick();
            }
        }

        private void OnDestroy()
        {
            foreach (IGameplayManager manager in gameplayManagers)
            {
                manager.Finish();
            }
        }

        public void InstantiateManagersPrefabs(GameObject[] prefabs)
        {
            foreach (GameObject prefab in prefabs)
            {
                GameObject manager = Instantiate(prefab) as GameObject;
                IGameplayManager managerComponent = manager.GetComponent<IGameplayManager>();
                gameplayManagers.Add(managerComponent);
                managerComponent.Initialize();
            }
        }
    }
}