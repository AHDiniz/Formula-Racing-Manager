using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Audio;
using FormulaManager.Management.Global;

namespace FormulaManager.Management.Gameplay
{
    public class AudioManager : MonoBehaviour, IGameplayManager
    {
        private List<GameAudio> sfx = new List<GameAudio>();
        private List<GameAudio> music = new List<GameAudio>();

        private bool isDone = false;
        private AppManager app;
        private SaveData saveData;

        bool IGameplayManager.IsDone { get => isDone; }

        void IGameplayManager.Initialize()
        {
            app = AppManager.Instance;
            saveData = (SaveData)app.Load("player_data");
            isDone = true;
        }

        void IGameplayManager.Tick()
        {
            foreach (GameAudio a in sfx)
            {
                a.Volume = saveData.SFXVolume;
            }

            foreach (GameAudio a in music)
            {
                a.Volume = saveData.MusicVolume;
            }
        }

        void IGameplayManager.Finish()
        {

        }

        public void AddSFX(GameAudio a)
        {
            sfx.Add(a);
        }

        public void AddMusic(GameAudio a)
        {
            music.Add(a);
        }
    }
}