using Events;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance = null;
        [SerializeField]
        private PlayerSessionData playerData;
        public float playerCash;

        private void OnEnable()
        {
            GameEvents.OnWalletUpdatedEvent += UpdatePlayerCash;
        }
        
        private void OnDisable()
        {
            GameEvents.OnWalletUpdatedEvent -= UpdatePlayerCash;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            playerData = ScriptableObject.CreateInstance<PlayerSessionData>();
        }
        
        private void UpdatePlayerCash(float value)
        {
            playerCash += value;
        }
    }
}
