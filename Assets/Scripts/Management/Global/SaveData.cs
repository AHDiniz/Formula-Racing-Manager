using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;

namespace FormulaManager.Management.Global
{
    [System.Serializable]
    public class SaveData
    {
        private float sfxVolume;
        private float musicVolume;
        private float raceDuration;
        private string playerTeamName;

        public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public float RaceDuration { get => raceDuration; set => raceDuration = value; }
        public string PlayerTeamName { get => playerTeamName; set => playerTeamName = value; }
    }
}
