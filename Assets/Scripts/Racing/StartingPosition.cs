using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace FormulaManager.Racing
{
    public class StartingPosition : MonoBehaviour
    {
        [SerializeField] private PathCreator path;

        public PathCreator Path { get => path; }

        public float GetTForThisPosition()
        {
            return path.path.GetClosestTimeOnPath(this.transform.position);
        }
    }
}