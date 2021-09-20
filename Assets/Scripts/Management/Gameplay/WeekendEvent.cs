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
        [SerializeField] private float launchWaitTime = 5f;
        [SerializeField] private Sector startFinishLine;

        private WaitForSeconds launchStall;

        protected WeekendManager manager = null;
        protected float timer = 0f;
        protected List<GameObject> carInstances = new List<GameObject>();
        protected List<LapCounter> grid = new List<LapCounter>();

        public WeekendManager Manager { get => manager; set => manager = value; }
        public LapCounter[] Grid { get => grid.ToArray(); }

        public virtual
        void Initialize(GameObject carPrefab, PathCreator path, Driver[] drivers, StartingPosition[] startingPositions)
        {
            launchStall = new WaitForSeconds(launchWaitTime);
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

            if (timer >= duration)
            {
                startFinishLine.LastLap = true;
                manager.GoToNextEvent();
            }
        }

        public virtual void Finish()
        {

        }

        protected virtual void ExtraCarInit(GameObject carInstance, VehicleController controller)
        {

        }

        private IEnumerator Launch(VehicleController controller)
        {
            yield return launchStall;
            controller.Launch();
        }
    }
}
