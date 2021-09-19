using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Management.Global
{
    public class AppManager : MonoBehaviour
    {
        [Header("Scene Management")]
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private List<string> raceSceneNames = new List<string>();

        [Header("Loading Screen")]
        [SerializeField] private GameObject loadingScreen;

        private static AppManager _instance;
        public static AppManager Instance { get => _instance; }

        private GameModeManager gameModeManager;
        private OptionsManager optionsManager;
        private LoadingManager loadingManager;

        private void Awake()
        {
            _instance = this;
            gameModeManager = new GameModeManager(mainMenuSceneName, raceSceneNames.ToArray());
            optionsManager = new OptionsManager();
            loadingManager = new LoadingManager(loadingScreen);
            Object.DontDestroyOnLoad(gameObject);
            Object.DontDestroyOnLoad(loadingScreen);
        }

        private void Start()
        {
            loadingScreen.SetActive(true);
            gameModeManager.LoadDefaultScene();
            StartCoroutine(loadingManager.CheckSceneLoadingProgress());
        }

        public void LoadGameMode(string name)
        {
            loadingScreen.SetActive(true);
            gameModeManager.LoadSceneByName(name);
            StartCoroutine(loadingManager.CheckSceneLoadingProgress());
        }

        public void Save(string saveName, object saveData) => optionsManager.Save(saveName, saveData);
        public object Load(string saveName) => optionsManager.Load(saveName);
        
        public void AddAsyncOperation(AsyncOperation operation) => loadingManager.AddAsyncOperation(operation);
        public void AddGameplayManager(IGameplayManager manager) => loadingManager.AddGameplayManager(manager);
        public void ClearOperationsList() => loadingManager.ClearOperationsList();
        public void ClearManagersList() => loadingManager.ClearManagersList();
    }
}