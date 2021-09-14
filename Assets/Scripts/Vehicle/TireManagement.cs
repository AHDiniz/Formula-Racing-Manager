using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    public class TireManagement : MonoBehaviour
    {
        private TireType currentTireType;
        private float tireHealth = 1f;

        public float SpeedMultiplier { get => currentTireType.SpeedMultiplier; }
        public float TireHealth { get => tireHealth; }

        public void Degrade()
        {
            tireHealth -= currentTireType.DegradationPerLap;
        }

        public void SwitchTire(TireType newTire)
        {
            tireHealth = 1f;
            currentTireType = newTire;
        }
    }
}