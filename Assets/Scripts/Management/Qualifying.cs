using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Vehicle;

namespace FormulaManager.Management
{
    public class Qualifying : WeekendEvent
    {
        [SerializeField] private List<StartingPosition> startingPositions = new List<StartingPosition>();
        [SerializeField] private int secondsToStart = 5;

        private WaitForSeconds countdown;

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
                controller.Driver = color.Driver = manager.Drivers[i];
                controller.CurrentPace = VehicleController.Pace.PushingHard;
                controller.Path = path;
                carInstance.SetActive(true);
                StartCoroutine(StartCountDown(controller));
            }
        }

        public override void TickEvent()
        {

        }

        public override void FinishEvent()
        {
            
        }

        private IEnumerator StartCountDown(VehicleController controller)
        {
            yield return countdown;
            controller.Launch();
        }
    }
}