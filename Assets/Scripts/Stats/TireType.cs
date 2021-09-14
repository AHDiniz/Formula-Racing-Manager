using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Stats
{
    [CreateAssetMenu(fileName = "TireType", menuName = "Racing Manager Stats/TireType", order = 0)]
    public class TireType : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private string tag;
        [SerializeField, Range(0.001f, .01f)] private float degradationPerLap = .005f;
        [SerializeField, Range(.9f, 1f)] private float speedMultiplier = .95f;

        public string Name { get => name; }
        public string Tag { get => tag; }
        public float DegradationPerLap { get => degradationPerLap; }
        public float SpeedMultiplier { get => speedMultiplier; }
    }
}