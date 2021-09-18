using UnityEngine;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(VehicleController))]
    public class FuelManagement : MonoBehaviour
    {
        [SerializeField] private float maxFuelUsage = .001f;

        private VehicleController controller;
        private float fuelLoad = 1f;

        public float FuelLoad { get => fuelLoad; }

        private void Start()
        {
            controller = GetComponent<VehicleController>();
        }

        public void UseFuel()
        {
            fuelLoad -= maxFuelUsage * controller.PaceMultiplier;
        }
    }
}