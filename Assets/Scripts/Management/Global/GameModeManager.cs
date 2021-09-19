using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Management.Global
{
    public class GameModeManager
    {
        private IGameMode mainMenuMode;
        private List<IGameMode> raceModes = new List<IGameMode>();
        private IGameMode currentMode = null;

        public GameModeManager(string mainMenuSceneName, string[] raceSceneNames)
        {
            mainMenuMode = new MainMenuMode(mainMenuSceneName);
            foreach (string sceneName in raceSceneNames)
            {
                raceModes.Add(new RaceMode(sceneName));
            }
        }

        public void LoadDefaultScene()
        {
            if (currentMode != null) currentMode.Finish();
            currentMode = mainMenuMode;
            mainMenuMode.Initialize();

            // TODO: If the game is running on the editor, run the currently edited scene
        }

        public void LoadSceneByName(string name)
        {
            if (currentMode != null) currentMode.Finish();

            if (name == mainMenuMode.SceneName)
            {
                currentMode = mainMenuMode;
                mainMenuMode.Initialize();
            }
            else
            {
                foreach (IGameMode race in raceModes)
                {
                    if (name == race.SceneName)
                    {
                        currentMode = race;
                        race.Initialize();
                        break;
                    }
                }
            }
        }
    }
}