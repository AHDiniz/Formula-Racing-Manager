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
            LapCounter first = currentEvent.Grid[0];
            float secondsRemaining = currentEvent.SecondsRemaining;

            int minutesRemaining = (int)(secondsRemaining / 60);
            int secondsToShow = minutesRemaining * 60 - (int)secondsRemaining;

            text.text = "Laps: " + first.LapCount + " Time: " + minutesRemaining + ":" + secondsToShow;
        }
    }
}
