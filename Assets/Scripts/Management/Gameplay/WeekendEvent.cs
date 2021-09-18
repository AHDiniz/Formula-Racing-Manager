using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using FormulaManager.Racing;
using FormulaManager.Stats;
using FormulaManager.Vehicle;

namespace FormulaManager.Management.Gameplay
{
    public class WeekendEvent : MonoBehaviour
    {
        protected GameObject carPrefab;
        protected PathCreator path;
        protected List<Driver> drivers = new List<Driver>();
        protected List<StartingPosition> startingPositions = new List<StartingPosition>();

        public virtual
        void Initialize(GameObject carPrefab, PathCreator path, Driver[] drivers, StartingPosition[] startingPositions)
        {
            this.carPrefab = carPrefab;
            this.path = path;
            
            foreach (Driver d in drivers)
                this.drivers.Add(d);
            foreach (StartingPostion p in startingPositions)
                this.startingPositions.Add(p);
            
            
        }

        public virtual void Tick()
        {

        }

        public virtual void Finish()
        {

        }
    }
}
