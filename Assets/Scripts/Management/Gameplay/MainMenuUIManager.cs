using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FormulaManager.Stats;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    public class MainMenuUIManager : MonoBehaviour, IGameplayManager
    {
        [Header("Menu Parents References")]
        [SerializeField] private GameObject mainMenuParent;
        [SerializeField] private GameObject optionsParent;
        [SerializeField] private GameObject raceSelectionParent;
        [SerializeField] private GameObject teamSelectionParent;

        [Header("Slider References")]
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider raceDurationSlider;

        private Team playerTeam;
        private string selectedRace;

        private AppManager app;
        private SaveData saveData;

        void IGameplayManager.Initialize()
        {
            app = AppManager.Instance;
            saveData = new SaveData();
        }

        void IGameplayManager.Tick()
        {
            saveData.SFXVolume = sfxVolumeSlider.value;
            saveData.MusicVolume = musicVolumeSlider.value;
            saveData.RaceDuration = raceDurationSlider.value;
        }

        void IGameplayManager.Finish()
        {
            app.Save("player_data", saveData);
        }

        public void GoToOptions(GameObject toDeactivate)
        {
            toDeactivate.SetActive(false);
            optionsParent.SetActive(true);
        }

        public void GoToMainMenu(GameObject toDeactivate)
        {
            toDeactivate.SetActive(false);
            mainMenuParent.SetActive(true);
        }

        public void GoToRaceSelection(GameObject toDeactivate)
        {
            toDeactivate.SetActive(false);
            raceSelectionParent.SetActive(true);
        }

        public void GoToTeamSelection(GameObject toDeactivate)
        {
            toDeactivate.SetActive(false);
            teamSelectionParent.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void SetPlayerTeam(Team playerTeam)
        {
            this.playerTeam = playerTeam;
            saveData.PlayerTeamName = this.playerTeam.Name;
        }

        public void SetRace(string race)
        {
            selectedRace = race;
        }

        public void StartRace()
        {
            // TODO: Call options manager here to set the team that the player chose
            app.LoadGameMode(selectedRace);
        }
    }
}