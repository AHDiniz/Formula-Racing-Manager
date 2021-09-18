using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Management.Global
{
    public class MainMenuMode : IGameMode
    {
        private string sceneName;

        public string SceneName { get => sceneName; }

        public MainMenuMode(string sceneName)
        {
            this.sceneName = sceneName;
        }

        void IGameMode.Initialize(GameObject[] managers)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); // TODO: Change this to async operation later
            GameObject context = new GameObject("Context");
            Context c = context.AddComponent<Context>() as Context;
            c.InstantiateManagersPrefabs(managers);
        }

        void IGameMode.Finish()
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}