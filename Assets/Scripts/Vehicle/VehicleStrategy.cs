using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(LapCounter))]
    [RequireComponent(typeof(VehicleController))]
    [RequireComponent(typeof(TireManagement))]
    public class VehicleStrategy : MonoBehaviour
    {
        private TireType nextTire;
        private int pitStopLap = -1;
        private bool pitThisLap = false;
        private bool onPits = false;

        private LapCounter lapCounter;
        private VehicleController controller;
        private TireManagement tire;

        public bool PitThisLap { get => pitThisLap; }
        public bool OnPits { get => onPits; }

        private void Start()
        {
            lapCounter = GetComponent<LapCounter>();
            controller = GetComponent<VehicleController>();
            tire = GetComponent<TireManagement>();
        }

        private void Update()
        {
            if (lapCounter.LapCount == pitStopLap)
                pitThisLap = true;
        }

        public void SetStrategy(int pitStopLap, TireType nextTire)
        {
            if (!onPits)
            {
                this.pitStopLap = pitStopLap;
                this.nextTire = nextTire;
            }
        }

        public void StartPitStop()
        {
            onPits = true;
            controller.StartPitStop();
            tire.SwitchTire(nextTire);
        }

        public void EndPitStop()
        {
            pitThisLap = onPits = false;
            controller.EndPitStop();
        }
    }
}
