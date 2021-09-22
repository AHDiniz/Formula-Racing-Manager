using System;
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
        [SerializeField] private float duration = 3600f;
        [SerializeField] private float launchWaitTime = 15f;
        [SerializeField] private Sector startFinishLine;

        protected WeekendManager manager = null;
        protected float timer = 0f;
        protected List<GameObject> carInstances = new List<GameObject>();
        protected List<LapCounter> grid = new List<LapCounter>();

        public float SecondsRemaining { get => (duration * manager.RaceDuration) - timer; }
        public WeekendManager Manager { get => manager; set => manager = value; }
        public LapCounter[] Grid { get => grid.ToArray(); }

        public virtual
        void Initialize(GameObject carPrefab, PathCreator path, Driver[] drivers, StartingPosition[] startingPositions)
        {
            for (int i = 0; i < drivers.Length; ++i)
            {
                float t = startingPositions[i].GetTForThisPosition();
                Vector3 position = path.path.GetPointAtTime(t);
                Quaternion rotation = path.path.GetRotation(t);

                GameObject carInstance = Instantiate(carPrefab) as GameObject;
                carInstance.transform.SetPositionAndRotation(position, rotation);
                carInstances.Add(carInstance);

                VehicleController controller = carInstance.GetComponent<VehicleController>();
                VehicleColor color = carInstance.GetComponent<VehicleColor>();
                controller.Driver = color.Driver = drivers[i];
                controller.Path = path;

                ExtraCarInit(carInstance, controller);

                carInstance.SetActive(true);

                StartCoroutine(Launch(controller));
            }
        }

        public virtual void Tick()
        {
            timer += Time.deltaTime;

            if (timer >= duration * manager.RaceDuration)
            {
                startFinishLine.LastLap = true;
                manager.GoToNextEvent();
            }
        }

        public virtual void Finish()
        {
            StartCoroutine(WaitForNextEvent());
        }

        protected virtual void ExtraCarInit(GameObject carInstance, VehicleController controller)
        {

        }

        public VehicleStrategy[] GetPlayerStrategies(Driver[] drivers)
        {
            List<VehicleStrategy> strategies = new List<VehicleStrategy>();
            foreach (LapCounter lapCounter in grid)
            {
                if (Array.IndexOf(drivers, lapCounter.DriverData) != -1)
                {
                    VehicleStrategy strategy = lapCounter.gameObject.GetComponent<VehicleStrategy>();
                    strategies.Add(strategy);
                }
            }
            return strategies.ToArray();
        }

        private IEnumerator Launch(VehicleController controller)
        {
            yield return new WaitForSeconds(launchWaitTime);
            controller.Launch();
        }

        private IEnumerator WaitForNextEvent()
        {
            yield return new WaitForSeconds(manager.SecondsForNextEvent);
            for (int i = 0; i < carInstances.Count; ++i)
                Destroy(carInstances[i]);
        }
    }
}
