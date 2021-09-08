using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Stats
{
    [CreateAssetMenu(fileName = "Team", menuName = "Racing Manager Stats/Team", order = 1)]
    public class Team : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private string tag;
        [SerializeField] private Material mainColor;
        [SerializeField] private Material secondaryColor;
        [SerializeField] private Material detailColor;
        [SerializeField, Range(100000000, 140000000)] private int yearlyRevenue;
        [SerializeField, Range(1, 20)] private int reliability;
        [SerializeField, Range(1, 20)] private int speed;
        [SerializeField, Range(1, 20)] private int acceleration;
        [SerializeField, Range(1, 20)] private int fuelConsumption;
        [SerializeField, Range(1, 20)] private int tireDegradation;
        [SerializeField, Range(1, 20)] private int energyConsumption;
        [SerializeField, Range(1, 20)] private int aeroSensibility;
        [SerializeField, Range(1, 20)] private int downforce;

        public string Name { get => name; }
        public string Tag { get => tag; }
        public Material MainColor { get => mainColor; }
        public Material SecondaryColor { get => secondaryColor; }
        public Material DetailColor { get => detailColor; }
        public int YearlyRevenue { get => yearlyRevenue; }
        public int Reliability { get => reliability; }
        public int Speed { get => speed; }
        public int Acceleration { get => acceleration; }
        public int FuelConsumption { get => fuelConsumption; }
        public int TireDegradation { get => tireDegradation; }
        public int EnergyConsumption { get => energyConsumption; }
        public int AeroSensibility { get => aeroSensibility; }
        public int Downforce { get => downforce; }
    }
}