using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Game Architecture: https://www.youtube.com/watch?v=JQ0Jdfxo7Cg
Save System: https://www.youtube.com/watch?v=5roZtuqZyuw
Loading Screen: https://www.youtube.com/watch?v=iXWFTgFNRdM
Progress Bars: https://www.youtube.com/watch?v=J1ng1zA3-Pk
*/

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

        private void Awake()
        {
            _instance = this;
            gameModeManager = new GameModeManager(mainMenuSceneName, raceSceneNames.ToArray());
            optionsManager = new OptionsManager();
            Object.DontDestroyOnLoad(gameObject);
            Object.DontDestroyOnLoad(loadingScreen);
            loadingScreen.SetActive(false);
        }

        private void Start()
        {
            gameModeManager.LoadDefaultScene();
        }

        public void LoadGameMode(string name)
        {
            if (name == mainMenuSceneName)
                gameModeManager.LoadSceneByName(name);
            else gameModeManager.LoadSceneByName(name);
        }

        public void Save(string saveName, object saveData) => optionsManager.Save(saveName, saveData);
        public object Load(string saveName) => optionsManager.Load(saveName);
    }
}