using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Management.Global
{
    public class GameModeManager
    {
        // Contains a list of the possible game modes
        // Has method to call default mode (main menu)
        // Also has a method to call the mode of the currently edited scene

        private IGameMode mainMenuMode;
        private List<IGameMode> raceModes = new List<IGameMode>();

        public GameModeManager(string mainMenuSceneName, string[] raceSceneNames)
        {
            mainMenuMode = new MainMenuMode(mainMenuSceneName);
            foreach (string sceneName in raceSceneNames)
            {
                raceModes.Add(new RaceMode(sceneName));
            }
        }

        public void LoadDefaultScene(GameObject[] managers)
        {
            // This is where the code to load the main menu scene comes in.
            mainMenuMode.Initialize(managers);

            // TODO: If the game is running on the editor, run the currently edited scene
        }

        public void LoadSceneByName(string name, GameObject[] managers)
        {
            // Loading the mode with a given name.
            if (name == mainMenuMode.SceneName)
                mainMenuMode.Initialize(managers);
            
            foreach (IGameMode race in raceModes)
            {
                if (name == race.SceneName)
                {
                    race.Initialize(managers);
                    break;
                }
            }
        }
    }
}