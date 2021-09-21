using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(VehicleController))]
    public class LapCounter : MonoBehaviour
    {
        private int lapCount = 0;
        private float fastestLap = Mathf.Infinity;
        private float previousLap = 0f;
        private float currentLap = 0f;
        private Driver driver;
        private VehicleController controller;

        public int LapCount { get => lapCount; }
        public float FastestLap { get => fastestLap; }
        public float PreviousLap { get => previousLap; }
        public float CurrentLap { get => currentLap; }
        public Driver DriverData { get => driver; }

        private void Start()
        {
            controller = GetComponent<VehicleController>();
            driver = controller.Driver;
        }

        private void Update()
        {
            currentLap += Time.deltaTime;
        }

        public void StartLap()
        {
            ++lapCount;
            previousLap = currentLap;
            currentLap = 0f;
            if (previousLap <= fastestLap && lapCount > 1)
                fastestLap = previousLap;
        }

        public void Finish()
        {
            if (currentLap >= fastestLap)
                fastestLap = currentLap;
            controller.Stop();
        }
    }
}
