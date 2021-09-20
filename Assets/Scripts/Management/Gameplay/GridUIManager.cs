using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.UI;
using FormulaManager.Vehicle;

namespace FormulaManager.Management.Gameplay
{
    [RequireComponent(typeof(WeekendManager))]
    public class GridUIManager : MonoBehaviour, IGameplayManager
    {
        [SerializeField] private List<GridPosition> gridPositions = new List<GridPosition>();
        
        private WeekendManager manager;
        private bool isDone = false;

        public bool IsDone { get => isDone; }

        void IGameplayManager.Initialize()
        {
            manager = GetComponent<WeekendManager>();
            isDone = true;
        }

        void IGameplayManager.Tick()
        {
            LapCounter[] grid = manager.CurrentEvent.Grid;
            for (int i = 0; i < grid.Length; ++i)
            {
                if (grid[i].FastestLap == Mathf.Infinity)
                    gridPositions[i].SetDataDisplay(i + 1, grid[i].DriverData.Tag, 0);
                else gridPositions[i].SetDataDisplay(i + 1, grid[i].DriverData.Tag, grid[i].FastestLap);
            }
        }

        void IGameplayManager.Finish()
        {

        }
    }
}
