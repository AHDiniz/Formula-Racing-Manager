using UnityEngine;
using FormulaManager.Management.Gameplay;

namespace FormulaManager.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameAudio : MonoBehaviour
    {
        [System.Serializable]
        public enum AudioType
        {
            SFX, Music
        }

        [SerializeField] private AudioType type;

        private AudioSource source;
        private AudioManager manager;

        private float volume;

        public float Volume { get => volume; set => volume = value; }

        private void Start()
        {
            source = GetComponent<AudioSource>();
            manager = GameObject.FindWithTag("GameController").GetComponent<AudioManager>();

            if (type == AudioType.SFX)
                manager.AddSFX(this);
            else manager.AddMusic(this);
        }

        private void Update()
        {
            source.volume = volume;
        }
    }
}
