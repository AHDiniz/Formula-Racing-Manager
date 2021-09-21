using UnityEngine;
using FormulaManager.Vehicle;

namespace FormulaManager.Racing
{
    public class PitStopExit : MonoBehaviour
    {
        [SerializeField] private string carTag = "Car";

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == carTag)
            {
                VehicleStrategy strategy = col.gameObject.GetComponent<VehicleStrategy>();
                if (strategy.OnPits)
                    strategy.EndPitStop();
            }
        }
    }
}