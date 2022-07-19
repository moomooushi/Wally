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

        private void Awake()
        {
            if (Instance == null)
            {
                playerData = ScriptableObject.CreateInstance<PlayerSessionData>();
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
        }
        
        private void OnEnable()
        {
            GameEvents.OnWalletUpdatedEvent += UpdatePlayerCash;
        }
        
        private void OnDisable()
        {
            GameEvents.OnWalletUpdatedEvent -= UpdatePlayerCash;
        }
        
        private void UpdatePlayerCash(float value)
        {
            playerCash = 0;
            playerCash = value;
        }
    }
}
