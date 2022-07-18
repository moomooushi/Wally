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

        private void OnCollisionEnter(Collision collision)
        {
            GameEvents.OnAudioCollisionEvent?.Invoke(GetRandomClip(constructionMaterial.audioClips));
        }

        AudioClip GetRandomClip(List<AudioClip> clips)
        {
            AudioClip clip = clips[Random.Range(0, clips.Count)];
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