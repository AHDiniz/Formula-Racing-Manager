using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Management.Global
{
    public class RaceMode : IGameMode
    {
        private string sceneName;
        private GameObject context;
        private Context c;
        private AppManager app;

        public string SceneName { get => sceneName; }

        public RaceMode(string sceneName)
        {
            this.sceneName = sceneName;
            app = AppManager.Instance;
        }

        void IGameMode.Initialize()
        {
            app.AddAsyncOperation(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            context = new GameObject("Context");
            c = context.AddComponent<Context>() as Context;
        }

        void IGameMode.Finish()
        {
            c.Finish();
            Object.Destroy(context);
            app.AddAsyncOperation(SceneManager.UnloadSceneAsync(sceneName));
        }
    }
}