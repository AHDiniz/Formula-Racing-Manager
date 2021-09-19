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
        private GameObject context;
        private AppManager app;

        public string SceneName { get => sceneName; }

        public MainMenuMode(string sceneName)
        {
            this.sceneName = sceneName;
            app = AppManager.Instance;
        }

        void IGameMode.Initialize()
        {
            app.AddAsyncOperation(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            context = new GameObject("Context");
            Context c = context.AddComponent<Context>() as Context;
        }

        void IGameMode.Finish()
        {
            Object.Destroy(context);
            app.AddAsyncOperation(SceneManager.UnloadSceneAsync(sceneName));
        }
    }
}