using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Stats;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    public class WeekendManager : MonoBehaviour, IGameplayManager
    {
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private PathCreator path;
        [SerializeField] private List<Driver> drivers = new List<Driver>();

        private float raceDuration;
        private string playerTeamName;
        private Driver[] playerDrivers = new Driver[2];
        private SaveData saveData;
        private AppManager app;

        public Driver[] PlayerDrivers { get => playerDrivers; }

        void IGameplayManager.Initialize()
        {
            app = AppManager.Instance;
            saveData = app.Load("player_data") as SaveData;
            raceDuration = saveData.RaceDuration;
            playerTeamName = saveData.PlayerTeamName;

            int playerDriverIndex = 0;
            foreach (Driver d in drivers)
            {                
                if (d.CurrentTeam.Name == playerTeamName)
                {
                    playerDrivers[playerDriverIndex] = d;
                    ++playerDriverIndex;
                }
            }
            
            // Setup weekend events
        }

        void IGameplayManager.Tick()
        {

        }

        void IGameplayManager.Finish()
        {

        }
    }
}
