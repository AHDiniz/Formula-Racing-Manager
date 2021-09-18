using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FormulaManager.Vehicle;
using FormulaManager.Stats;

namespace FormulaManager.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gridMenuGameObj;
        [SerializeField] private GameObject stratMenuGameObj;
        [SerializeField] private RectTransform gridParent;
        [SerializeField] private GameObject gridPositionPrefab;

        private bool gridActive = false, stratActive = false;

        private List<GridPosition> qualiGridUI = new List<GridPosition>();
        private List<GridPosition> raceGridUI = new List<GridPosition>();
        
        private Dictionary<Driver, Lap> qualifyingGrid = new Dictionary<Driver, Lap>();
        private Dictionary<Driver, Lap> raceGrid = new Dictionary<Driver, Lap>();

        private Team playerTeam;
        private Driver[] playerDrivers = new Driver[2];

        public Dictionary<Driver, Lap> QualifyingGrid
        { get => qualifyingGrid; set => qualifyingGrid = value; }

        public Dictionary<Driver, Lap> RaceGrid
        { get => raceGrid; set => raceGrid = value; }

        private void Start()
        {
            gridMenuGameObj.SetActive(gridActive);
            stratMenuGameObj.SetActive(stratActive);
        }

        public void SetPlayerTeamAndDrivers(Team team, Driver driver1, Driver driver2)
        {
            playerTeam = team;
            playerDrivers[0] = driver1;
            playerDrivers[1] = driver2;
        }
        
        public void SwitchGridOnOff()
        {
            gridActive = !gridActive;
            gridMenuGameObj.SetActive(gridActive);
        }

        public void SwitchStratOnOff()
        {
            stratActive = !stratActive;
            stratMenuGameObj.SetActive(stratActive);
        }

        public void UpdateQualiGridPosition()
        {
            foreach (RectTransform child in gridParent)
                Destroy(child.gameObject);

            int i = 1;
            foreach (KeyValuePair<Driver, Lap> pair in qualifyingGrid)
            {
                GameObject gridPositionInstance = Instantiate(gridPositionPrefab) as GameObject;
                GridPosition gridPosition = gridPositionInstance.GetComponent<GridPosition>();
                gridPosition.SetDataDisplay(i, pair.Key.Tag, pair.Value.FastestLapTimeSeconds);
                gridPositionInstance.transform.SetParent(gridParent);
                gridPositionInstance.SetActive(true);
                ++i;
            }
        }
    }
}