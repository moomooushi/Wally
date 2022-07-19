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
        List<AudioClip> _clips;

        private void Start()
        {
            AddConstructionType();
            _clips = this.constructionMaterial.audioClips;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            AudioClip randomClip = GetRandomClip(_clips);
            GameEvents.OnAudioCollisionEvent?.Invoke(randomClip);
        }

        AudioClip GetRandomClip(List<AudioClip> listOfClips)
        {
            AudioClip clip = listOfClips[Random.Range(0, listOfClips.Count)];
            Debug.Log("We got a random clip to play " + clip);
            return clip;
        }

        void AddConstructionType()
        {
            if (constructionMaterial == null && GetComponent<Receptacle>())
            {
                constructionMaterial = GetComponent<Receptacle>().receptacleType.constructionMaterial;
            }
        }
}
}