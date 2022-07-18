using System;
using System.Collections.Generic;
using Events;
using ScriptableObjects.MaterialInteractions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private ConstructionMaterialType constructionMaterial;

        private void OnValidate()
        {
            AddConstructionType();
        }

        private void Awake()
        {
            AddConstructionType();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            AudioClip clip = GetRandomClip(constructionMaterial.audioClips);
            GameEvents.OnAudioCollisionEvent?.Invoke(clip);
        }

        AudioClip GetRandomClip(List<AudioClip> clips)
        {
            AudioClip clip = clips[Random.Range(0, clips.Count)];
            Debug.Log("We got a random clip to play" + clip);
            return clip;
        }

        void AddConstructionType()
        {
            if (constructionMaterial != null)
            {
                if(GetComponent<Receptacle>())
                    constructionMaterial = GetComponent<Receptacle>().receptacleType.constructionMaterial;
            }
        }
}
}