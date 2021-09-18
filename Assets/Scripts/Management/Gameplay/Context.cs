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

        public void GetManagers()
        {
            GameObject gameController = GameObject.FindWithTag("GameController");
            if (gameController != null)
            {
                IGameplayManager[] managers = gameController.GetComponents<IGameplayManager>();
                if (managers.Length > 0)
                {
                    foreach (IGameplayManager m in managers)
                    {
                        m.Initialize();
                        gameplayManagers.Add(m);
                    }
                }
            }
        }
    }
}