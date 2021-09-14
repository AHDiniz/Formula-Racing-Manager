using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(FuelManagement))]
    [RequireComponent(typeof(TireManagement))]
    public class Lap : MonoBehaviour
    {
        private int lapCount = 0;

        private FuelManagement fuel;
        private TireManagement tire;

        private float previousLapTimeSeconds = 0f;
        private float currentLapTimeSeconds = 0f;

        private void Start()
        {
            fuel = GetComponent<FuelManagement>();
            tire = GetComponent<TireManagement>();
        }

        private void Update()
        {
            currentLapTimeSeconds += Time.deltaTime;
        }

        public void OnStartFinishLine()
        {
            previousLapTimeSeconds = currentLapTimeSeconds;
            currentLapTimeSeconds = 0f;
            fuel.UseFuel();
            tire.Degrade();
        }
    }
}