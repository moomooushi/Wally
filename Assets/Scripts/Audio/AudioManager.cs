using Events;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance = null;
        private AudioSource audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void OnEnable()
        {
            GameEvents.OnAudioCollisionEvent += PlayClip;
        }
        private void OnDisable()
        {
            GameEvents.OnAudioCollisionEvent -= PlayClip;
        }

        private void PlayClip(AudioClip clip)
        {
            
        }

        void GetAudioSource()
        {
            
        }
    }
}