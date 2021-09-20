using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Stats;
using FormulaManager.Vehicle;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Racing
{
    public class Qualifying : WeekendEvent
    {
        [Header("Qualifying Properties")]
        [SerializeField] private TireType qualiTire;

        public override
        void Initialize(GameObject carPrefab, PathCreator path, Driver[] drivers, StartingPosition[] startingPositions)
        {
            base.Initialize(carPrefab, path, drivers, startingPositions);
        }

        public override void Tick()
        {
            grid = grid.OrderBy(lapCounter => lapCounter.FastestLap).ToList() as List<LapCounter>;
        }

        public override void Finish()
        {
            
        }

        protected override void ExtraCarInit(GameObject carInstance, VehicleController controller)
        {
            TireManagement tire = carInstance.GetComponent<TireManagement>();
            tire.SwitchTire(qualiTire);

            LapCounter lapCounter = carInstance.GetComponent<LapCounter>();
            grid.Add(lapCounter);

            controller.CurrentPace = VehicleController.Pace.PushingHard;
        }
    }
}