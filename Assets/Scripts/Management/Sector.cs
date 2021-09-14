using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Vehicle;

namespace FormulaManager.Management
{
    public class Sector : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float expectedSpeedScale = 1;
        [SerializeField] private bool startFinishLine = false;
        [SerializeField] private string carTag = "Car";

        private float timer = 0f;

        public float ExpectedSpeedScale { get => expectedSpeedScale; }
        public bool StartFinishLine { get => startFinishLine; }

        private void Update()
        {
            timer += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == carTag)
            {
                VehicleController controller = col.gameObject.GetComponent<VehicleController>();
                controller.SpeedScale = expectedSpeedScale;

                if (startFinishLine)
                {
                    Lap lap = col.gameObject.GetComponent<Lap>();
                    lap.OnStartFinishLine();
                }
            }
        }
    }
}