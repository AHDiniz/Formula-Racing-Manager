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

        [Header("Managers Prefabs")]
        [SerializeField] private List<GameObject> mainMenuManagers = new List<GameObject>();
        [SerializeField] private List<GameObject> raceManagers = new List<GameObject>();

        private static AppManager _instance;
        public static AppManager Instance { get => _instance; }

        private GameModeManager gameModeManager;

        private void Awake()
        {
            _instance = this;
            gameModeManager = new GameModeManager(mainMenuSceneName, raceSceneNames.ToArray());
            Object.DontDestroyOnLoad(gameObject);
            Object.DontDestroyOnLoad(loadingScreen);
            loadingScreen.SetActive(false);
        }

        private void Start()
        {
            gameModeManager.LoadDefaultScene(mainMenuManagers.ToArray());
        }

        public void LoadGameMode(string name)
        {
            if (name == mainMenuSceneName)
                gameModeManager.LoadSceneByName(name, mainMenuManagers.ToArray());
            else gameModeManager.LoadSceneByName(name, raceManagers.ToArray());
        }
    }
}