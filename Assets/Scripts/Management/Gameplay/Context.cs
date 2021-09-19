using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FormulaManager.Management.Gameplay
{
    public class Context : MonoBehaviour
    {
        private List<IGameplayManager> gameplayManagers = new List<IGameplayManager>();
        private GameObject gameController;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Contains("Grand Prix") || scene.name == "Main Menu")
            {
                gameController = GameObject.FindWithTag("GameController");
                IGameplayManager[] managers = gameController.GetComponents<IGameplayManager>();
                foreach (IGameplayManager m in managers)
                {
                    m.Initialize();
                    gameplayManagers.Add(m);
                }
            }
        }
    }
}