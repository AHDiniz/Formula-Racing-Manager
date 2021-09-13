using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FormulaManager.Stats;

namespace FormulaManager.Management
{
    public class WeekendManager : MonoBehaviour
    {
        [SerializeField] private List<WeekendEvent> weekendEvents = new List<WeekendEvent>();
        [SerializeField, Range(0, 1)] private float timeScale = .25f;
        [SerializeField] private UnityEvent OnFinishWeekend;
        [SerializeField] private List<Driver> drivers = new List<Driver>();
        [SerializeField] private GameObject carPrefab;

        private int weekendEvent = 0;
        private List<Driver> startingGrid = new List<Driver>();

        public float TimeScale { get => timeScale; }
        public Driver[] Drivers { get => drivers.ToArray(); }
        public GameObject CarPrefab { get => carPrefab; }

        private void Start()
        {
            weekendEvents[weekendEvent].InitEvent();
        }

        private void Update()
        {
            weekendEvents[weekendEvent].TickEvent();
        }

        public void NextStage()
        {
            weekendEvents[weekendEvent].FinishEvent();
            ++weekendEvent;
            if (weekendEvent < weekendEvents.Count)
            {
                weekendEvents[weekendEvent].InitEvent();
            }
            else
            {
                OnFinishWeekend.Invoke();
            }
        }

        public void SetStartingGrid(Driver[] grid)
        {
            for (int i = 0; i < grid.Length; ++i)
            {
                startingGrid.Add(grid[i]);
            }
        }
    }
}