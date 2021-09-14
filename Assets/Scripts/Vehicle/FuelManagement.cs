using UnityEngine;

namespace FormulaManager.Vehicle
{
    public class FuelManagement : MonoBehaviour
    {
        [SerializeField] private float fuelUsageOnNeutralPace = .001f;

        private float fuelLoad = 1f;

        public float FuelLoad { get => fuelLoad; }

        public void UseFuel()
        {
            fuelLoad -= fuelUsageOnNeutralPace;
        }
    }
}