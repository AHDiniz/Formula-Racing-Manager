using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Racing;
using FormulaManager.Stats;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    [RequireComponent(typeof(StrategyUIManager))]
    public class WeekendManager : MonoBehaviour, IGameplayManager
    {
        [SerializeField] private float secondsForNextEvent = 30f;
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private PathCreator path;
        [SerializeField] private List<WeekendEvent> events = new List<WeekendEvent>();
        [SerializeField] private List<Driver> drivers = new List<Driver>();
        [SerializeField] private List<StartingPosition> startingPositions = new List<StartingPosition>();

        private bool isDone = false;
        private bool waitingNextEvent = false;
        private int currentEvent = 0;
        private float raceDuration;
        private string playerTeamName;
        private Driver[] playerDrivers = new Driver[2];
        private SaveData saveData;
        private AppManager app;
        private WaitForSeconds waitForNextEvent;

        private StrategyUIManager strategyUIManager;

        public bool WaitingNextEvent { get => waitingNextEvent; }
        public Driver[] PlayerDrivers { get => playerDrivers; }
        public WeekendEvent CurrentEvent { get => events[currentEvent]; }

        bool IGameplayManager.IsDone { get => isDone; }

        void IGameplayManager.Initialize()
        {
            strategyUIManager = GetComponent<StrategyUIManager>();
            waitForNextEvent = new WaitForSeconds(secondsForNextEvent);

            app = AppManager.Instance;
            saveData = app.Load("player_data") as SaveData;

            if (saveData != null)
            {
                raceDuration = saveData.RaceDuration;
                playerTeamName = saveData.PlayerTeamName;
            }

            int playerDriverIndex = 0;
            foreach (Driver d in drivers)
            {                
                if (d.CurrentTeam.Name == playerTeamName)
                {
                    playerDrivers[playerDriverIndex] = d;
                    ++playerDriverIndex;
                }
            }

            strategyUIManager.Drivers = playerDrivers;
            
            events[currentEvent].Manager = this;
            events[currentEvent].Initialize(carPrefab, path, drivers.ToArray(), startingPositions.ToArray());
            
            strategyUIManager.CurrentEvent = events[currentEvent];
            strategyUIManager.Strategies = events[currentEvent].GetPlayerStrategies(playerDrivers);
            strategyUIManager.PopulatePaceDropdown();

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
            StartCoroutine(WaitForNextEvent());
        }

        private IEnumerator WaitForInitialization()
        {
            yield return new WaitForSeconds(.5f);
            isDone = true;
        }

        private IEnumerator WaitForNextEvent()
        {
            waitingNextEvent = true;
            yield return waitForNextEvent;
            events[currentEvent].Finish();
            currentEvent = (currentEvent + 1) % events.Count;
            if (events[currentEvent].Manager == null)
                events[currentEvent].Manager = this;
            events[currentEvent].Initialize(carPrefab, path, drivers.ToArray(), startingPositions.ToArray());
            strategyUIManager.CurrentEvent = events[currentEvent];
            strategyUIManager.Strategies = events[currentEvent].GetPlayerStrategies(playerDrivers);
            waitingNextEvent = false;
        }
    }
}
