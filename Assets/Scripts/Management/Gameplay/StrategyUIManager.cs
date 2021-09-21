using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FormulaManager.Stats;
using FormulaManager.Vehicle;

namespace FormulaManager.Management.Gameplay
{
    public class StrategyUIManager : MonoBehaviour, IGameplayManager
    {
        [System.Serializable]
        public struct PaceNameRelation
        {
            public string name;
            public VehicleController.Pace pace;
        }

        [Header("Options Definitions")]
        [SerializeField] private List<TireType> tireTypes = new List<TireType>();
        [SerializeField] private List<PaceNameRelation> paceNames = new List<PaceNameRelation>();
        
        [Header("UI Objects References")]
        [SerializeField] private GameObject strategyParent;
        [SerializeField] private List<TextMeshProUGUI> driverNames = new List<TextMeshProUGUI>();
        [SerializeField] private List<TMP_Dropdown> tireSelectors = new List<TMP_Dropdown>();
        [SerializeField] private List<TMP_Dropdown> paceSelectors = new List<TMP_Dropdown>();
        [SerializeField] private List<TMP_InputField> pitLapInputs = new List<TMP_InputField>();

        private bool isDone = false;
        private bool strategyActive = false;
        private WeekendEvent currentEvent = null;
        private Driver[] drivers = null;
        private VehicleStrategy[] strategies = null;

        public bool IsDone { get => isDone; }

        public Driver[] Drivers { get => drivers; set => drivers = value; }
        public VehicleStrategy[] Strategies { get => strategies; set => strategies = value; }
        public WeekendEvent CurrentEvent { get => currentEvent; set => currentEvent = value; }

        void IGameplayManager.Initialize()
        {
            strategyParent.SetActive(strategyActive);

            List<string> tireNames = new List<string>();

            foreach (TireType tire in tireTypes)
                tireNames.Add(tire.Name);
            
            foreach (TMP_Dropdown tireSelector in tireSelectors)
                tireSelector.AddOptions(tireNames);

            isDone = true;
        }

        void IGameplayManager.Tick()
        {

        }

        void IGameplayManager.Finish()
        {
            
        }

        public void SwitchStrategyOnOff()
        {
            strategyActive = !strategyActive;
            strategyParent.SetActive(strategyActive);
        }

        public void PopulatePaceDropdown()
        {
            for (int i = 0; i < drivers.Length; ++i)
            {
                List<string> names = new List<string>();
                foreach(PaceNameRelation rel in paceNames)
                    names.Add(rel.name);
                paceSelectors[i].AddOptions(names);
            }
        }

        public void SetStrategy(int driverID)
        {
            TireType selectedTire = null;

            int tireOptionID = tireSelectors[driverID].value;
            string tireName = tireSelectors[driverID].options[tireOptionID].text;
            foreach (TireType tire in tireTypes)
            {
                if (tire.Name == tireName)
                {
                    selectedTire = tire;
                    break;
                }
            }

            int selectedPitLap = Convert.ToInt32(pitLapInputs[driverID].text);

            strategies[driverID].SetStrategy(selectedPitLap, selectedTire);
        }
    }
}