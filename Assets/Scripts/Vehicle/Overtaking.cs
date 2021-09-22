using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(VehicleController))]
    public class Overtaking : MonoBehaviour
    {
        private int currentPosition = 0;
        private VehicleController controller;

        public int CurrentPosition { get => currentPosition; }
        public VehicleController Controller { get => controller; }

        private void Start()
        {
            controller = GetComponent<VehicleController>();
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == gameObject.tag)
            {
                Overtaking overtaking = col.gameObject.GetComponent<Overtaking>();
                if (overtaking.Controller.DistanceTravelled < controller.DistanceTravelled)
                    ++currentPosition;
                else
                    --currentPosition;
            }
        }
    }
}
