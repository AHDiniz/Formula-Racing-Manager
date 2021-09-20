using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    public class Context : MonoBehaviour
    {
        private List<IGameplayManager> gameplayManagers = new List<IGameplayManager>();
        private GameObject gameController;
        private AppManager app;

        private void OnEnable()
        {
            app = AppManager.Instance;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            foreach (IGameplayManager manager in gameplayManagers)
            {
                manager.Finish();
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Update()
        {
            foreach (IGameplayManager manager in gameplayManagers)
            {
                manager.Tick();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Contains("Grand Prix") || scene.name == "Main Menu")
            {
                Debug.Log(scene.name);
                gameController = GameObject.FindWithTag("GameController");
                IGameplayManager[] managers = gameController.GetComponents<IGameplayManager>();
                foreach (IGameplayManager m in managers)
                {
                    gameplayManagers.Add(m);
                    app.AddGameplayManager(m);
                    m.Initialize();
                }
            }
        }
    }
}