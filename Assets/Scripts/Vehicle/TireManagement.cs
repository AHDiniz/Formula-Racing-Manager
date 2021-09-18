using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(VehicleController))]
    public class TireManagement : MonoBehaviour
    {
        [SerializeField] private AnimationCurve speedByTireHealthCurve;

        private TireType currentTireType;
        private VehicleController controller;
        private float tireHealth = 1f;

        public float SpeedMultiplier { get => currentTireType.SpeedMultiplier; }
        public float TireHealth { get => tireHealth; }
        public float TireSpeedMultiplier { get => speedByTireHealthCurve.Evaluate(tireHealth); }

        private void Start()
        {
            controller = GetComponent<VehicleController>();
        }

        public void Degrade()
        {
            tireHealth -= currentTireType.MaxDegPerLap * controller.PaceMultiplier;
        }

        public void SwitchTire(TireType newTire)
        {
            tireHealth = 1f;
            currentTireType = newTire;
        }
    }
}