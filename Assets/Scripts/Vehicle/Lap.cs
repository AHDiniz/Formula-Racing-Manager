using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(FuelManagement))]
    [RequireComponent(typeof(TireManagement))]
    [RequireComponent(typeof(VehicleController))]
    public class Lap : MonoBehaviour
    {
        private int lapCount = 0;
        private int completedLaps = -1;

        private FuelManagement fuel;
        private TireManagement tire;
        private VehicleController controller;

        private float previousLapTimeSeconds = 0f;
        private float currentLapTimeSeconds = 0f;
        private float fastestLapTimeSeconds = Mathf.Infinity;

        public int CurrentLap { get => lapCount; }
        public int CompletedLaps { get => completedLaps; }
        public float PreviousLapTimeSeconds { get => previousLapTimeSeconds; }
        public float CurrentLapTimeSeconds { get => currentLapTimeSeconds; }
        public float FastestLapTimeSeconds { get => fastestLapTimeSeconds; }

        private void Start()
        {
            fuel = GetComponent<FuelManagement>();
            tire = GetComponent<TireManagement>();
            controller = GetComponent<VehicleController>();
        }

        private void Update()
        {
            if (completedLaps >= 0)
                currentLapTimeSeconds += Time.deltaTime;
        }

        public void OnStartFinishLine()
        {
            if (currentLapTimeSeconds <= fastestLapTimeSeconds && completedLaps >= 0)
                fastestLapTimeSeconds = currentLapTimeSeconds;
            previousLapTimeSeconds = currentLapTimeSeconds;
            ++lapCount;
            ++completedLaps;
            currentLapTimeSeconds = 0f;
            fuel.UseFuel();
            tire.Degrade();
        }
    }
}