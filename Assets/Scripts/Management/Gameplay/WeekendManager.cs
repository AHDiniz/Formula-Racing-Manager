using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Racing;
using FormulaManager.Stats;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    public class WeekendManager : MonoBehaviour, IGameplayManager
    {
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private PathCreator path;
        [SerializeField] private List<WeekendEvent> events = new List<WeekendEvent>();
        [SerializeField] private List<Driver> drivers = new List<Driver>();
        [SerializeField] private List<StartingPosition> startingPositions = new List<StartingPosition>();

        private bool isDone = false;
        private int currentEvent = 0;
        private float raceDuration;
        private string playerTeamName;
        private Driver[] playerDrivers = new Driver[2];
        private SaveData saveData;
        private AppManager app;

        public Driver[] PlayerDrivers { get => playerDrivers; }

        bool IGameplayManager.IsDone { get => isDone; }

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
            
            events[currentEvent].Manager = this;
            events[currentEvent].Initialize(carPrefab, path, drivers.ToArray(), startingPositions.ToArray());

            StartCoroutine(WaitForInitialization());
        }

        void IGameplayManager.Tick()
        {
            events[currentEvent].Tick();
        }

        void IGameplayManager.Finish()
        {
            events[currentEvent].Finish();
        }

        public void GoToNextEvent()
        {
            events[currentEvent].Finish();
            currentEvent = (currentEvent + 1) % events.Count;
            if (events[currentEvent].Manager == null)
                events[currentEvent].Manager = this;
            events[currentEvent].Initialize(carPrefab, path, drivers.ToArray(), startingPositions.ToArray());
        }

        private IEnumerator WaitForInitialization()
        {
            yield return new WaitForSeconds(.5f);
            isDone = true;
        }
    }
}
