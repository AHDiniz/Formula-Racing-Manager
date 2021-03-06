using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FormulaManager.UI
{
    public class GridPosition : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI position;
        [SerializeField] private TextMeshProUGUI driverTag;
        [SerializeField] private TextMeshProUGUI lapTime;

        public void SetDataDisplay(int position, string driverTag, float lapTime)
        {
            this.position.text = "" + position;
            this.driverTag.text = driverTag;

            int seconds = (int)lapTime;
            int miliseconds = (int)((lapTime - (float)seconds) * 1000f);
            int minutes = seconds / 60;

            if (miliseconds >= 100)
                this.lapTime.text = minutes + ":" + seconds + "." + miliseconds;
            else if (miliseconds >= 10)
                this.lapTime.text = minutes + ":" + seconds + ".0" + miliseconds;
            else this.lapTime.text = minutes + ":" + seconds + ".00" + miliseconds;
        }
    }
}