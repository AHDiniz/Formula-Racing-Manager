using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Vehicle;
using FormulaManager.Stats;
using FormulaManager.UI;

namespace FormulaManager.Management
{
    public class Qualifying : WeekendEvent
    {
        [SerializeField] private List<StartingPosition> startingPositions = new List<StartingPosition>();
        [SerializeField] private int secondsToStart = 5;
        [SerializeField] private float secondsBetweenGridUpdates = 1f;
        [SerializeField] private TireType qualiTireType;
        [SerializeField] private UIManager uiManager;

        private bool canUpdateGrid = false;
        private float gridUpdateTimer = 0f;
        private WaitForSeconds countdown;

        private Dictionary<Driver, Lap> fastestLapTimes = new Dictionary<Driver, Lap>();

        public Dictionary<Driver, Lap> FastestLapTimer { get => fastestLapTimes; }

        public override void InitEvent()
        {
            path = startingPositions[0].Path;
            countdown = new WaitForSeconds(secondsToStart);

            for (int i = 0; i < manager.Drivers.Length; ++i)
            {
                float t = startingPositions[i].GetTForThisPosition();
                Vector3 position = path.path.GetPointAtTime(t);
                Quaternion rotation = path.path.GetRotation(t);
                GameObject carInstance = Instantiate(manager.CarPrefab) as GameObject;
                carInstance.transform.SetPositionAndRotation(position, rotation);
                VehicleController controller = carInstance.GetComponent<VehicleController>();
                VehicleColor color = carInstance.GetComponent<VehicleColor>();
                Lap lap = carInstance.GetComponent<Lap>();
                TireManagement tire = carInstance.GetComponent<TireManagement>();
                tire.SwitchTire(qualiTireType);
                fastestLapTimes[manager.Drivers[i]] = lap;
                controller.Driver = color.Driver = manager.Drivers[i];
                controller.CurrentPace = VehicleController.Pace.PushingHard;
                controller.Path = path;
                carInstance.SetActive(true);
                StartCoroutine(StartCountDown(controller));
            }
        }

        public override void TickEvent()
        {
            if (canUpdateGrid)
            {
                gridUpdateTimer += Time.deltaTime;
                if (gridUpdateTimer >= secondsBetweenGridUpdates)
                {
                    OrderGridLapTimes();
                    uiManager.QualifyingGrid = fastestLapTimes;
                    uiManager.UpdateQualiGridPosition();
                }
            }
        }

        public override void FinishEvent()
        {
            OrderGridLapTimes();
        }

        public void OrderGridLapTimes()
        {
            fastestLapTimes.OrderBy(pair => pair.Value.FastestLapTimeSeconds);
        }

        private IEnumerator StartCountDown(VehicleController controller)
        {
            yield return countdown;
            controller.Launch();
            if (!canUpdateGrid) canUpdateGrid = true;
        }
    }
}