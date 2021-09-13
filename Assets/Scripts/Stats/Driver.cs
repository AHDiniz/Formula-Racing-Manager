using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Stats
{
    [CreateAssetMenu(fileName = "Driver", menuName = "Racing Manager Stats/Driver", order = 1)]
    public class Driver : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private string tag;
        [SerializeField] private int age;
        [SerializeField] private int number;
        [SerializeField, Range(1, 20)] private int experience;
        [SerializeField, Range(1, 20)] private int talent;
        [SerializeField, Range(1, 20)] private int progression;
        [SerializeField, Range(1, 20)] private int awareness;
        [SerializeField, Range(1, 20)] private int aggressiveness;
        [SerializeField, Range(1, 20)] private int defensiveAbilities;
        [SerializeField, Range(1, 20)] private int tireManagement;
        [SerializeField, Range(1, 20)] private int fuelManagement;
        [SerializeField, Range(1, 20)] private int energyManagement;
        [SerializeField] private Team team;

        public string Name { get => name; }
        public string Tag { get => tag; }
        public int Age { get => age; }
        public int Number { get => number; }
        public int Experience { get => experience; }
        public int Talent { get => talent; }
        public int Progression { get => progression; }
        public int Awareness { get => awareness; }
        public int Aggressiveness { get => aggressiveness; }
        public int DefensiveAbilities { get => defensiveAbilities; }
        public int TireManagement { get => tireManagement; }
        public int FuelManagement { get => fuelManagement; }
        public int EnergyManagement { get => energyManagement; }
        public Team CurrentTeam { get => team; }
    }
}
