using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Vehicle
{
    [RequireComponent(typeof(MeshRenderer))]
    public class VehicleColor : MonoBehaviour
    {
        [SerializeField] private Team team;

        private MeshRenderer meshRenderer;

        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            Material[] materials = {team.MainColor, team.SecondaryColor, team.DetailColor};
            meshRenderer.materials = materials;
        }
    }
}