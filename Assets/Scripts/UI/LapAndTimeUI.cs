using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FormulaManager.Vehicle;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.UI
{
    public class LapAndTimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private WeekendManager manager;

        private void Start()
        {
            manager = GameObject.FindWithTag("GameController").GetComponent<WeekendManager>();
        }

        private void Update()
        {
            WeekendEvent currentEvent = manager.CurrentEvent;
            if (currentEvent != null)
            {
                if (currentEvent.Grid != null && currentEvent.Grid.Length != 0)
                {
                    LapCounter first = currentEvent.Grid[0];
                    if (first != null)
                    {
                        float secondsRemaining = currentEvent.SecondsRemaining;

                        int minutesRemaining = (int)(secondsRemaining / 60);
                        int secondsToShow = minutesRemaining * 60 - (int)secondsRemaining;

                        minutesRemaining = Mathf.Abs(minutesRemaining);
                        secondsToShow = Mathf.Abs(secondsToShow);

                        text.text = "Laps: " + first.LapCount + " Time Remaining: " + minutesRemaining + ":" + secondsToShow;
                    }
                }
            }
        }
    }
}
