using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FormulaManager.Stats;
using FormulaManager.Vehicle;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.UI
{
    public class DriverUI : MonoBehaviour
    {
        [SerializeField] private string carTag = "Car";
        [SerializeField] private List<TextMeshProUGUI> driverName = new List<TextMeshProUGUI>();
        [SerializeField] private List<ProgressBar> tireHealthBar = new List<ProgressBar>();
        [SerializeField] private List<ProgressBar> fuelAmountBar = new List<ProgressBar>();

        private WeekendManager manager;
        private List<FuelManagement> fuel = new List<FuelManagement>(2);
        private List<TireManagement> tire = new List<TireManagement>(2);

        private void Start()
        {
            Driver[] drivers = manager.PlayerDrivers;
            GameObject[] cars = GameObject.FindGameObjectsWithTag(carTag);

            foreach (GameObject car in cars)
            {
                VehicleController controller = car.GetComponent<VehicleController>();
                for (int i = 0; i < drivers.Length; ++i)
                {
                    if (controller.Driver.Number == drivers[i].Number)
                    {
                        fuel[i] = car.GetComponent<FuelManagement>();
                        tire[i] = car.GetComponent<TireManagement>();
                    }
                }
            }
        }

        private void Update()
        {

        }
    }
}
