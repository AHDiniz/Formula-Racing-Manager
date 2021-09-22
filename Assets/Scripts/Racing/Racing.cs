using System;
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
    public class Racing : WeekendEvent
    {
        private List<Overtaking> overtakingComponents = new List<Overtaking>();

        public override
        void Initialize(GameObject carPrefab, PathCreator path, Driver[] drivers, StartingPosition[] startingPositions)
        {
            base.Initialize(carPrefab, path, drivers, startingPositions);
            Debug.Log("Racing Event");
        }

        public override void Tick()
        {
            base.Tick();
            grid = grid.OrderBy(lapCounter => {
                int index = Array.IndexOf(grid.ToArray(), lapCounter);
                return OrderGrid(index);
            }).ToList() as List<LapCounter>;
        }

        public override void Finish()
        {
            base.Finish();
        }

        protected override void ExtraCarInit(GameObject carInstance, VehicleController controller)
        {
            Overtaking o = carInstance.GetComponent<Overtaking>();
            overtakingComponents.Add(o);
        }

        private int OrderGrid(int index)
        {
            return overtakingComponents[index].CurrentPosition;
        }
    }
}
