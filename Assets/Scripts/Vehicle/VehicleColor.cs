using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(MeshRenderer))]
    public class VehicleColor : MonoBehaviour
    {
        [SerializeField] private Driver driver;

        public Driver Driver { get => driver; set => driver = value; }

        private MeshRenderer meshRenderer;

        // Start is called before the first frame update
        private void OnEnable()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            Team team = driver.CurrentTeam;
            Material[] materials = {team.MainColor, team.SecondaryColor, team.DetailColor};
            meshRenderer.materials = materials;
        }
    }
}