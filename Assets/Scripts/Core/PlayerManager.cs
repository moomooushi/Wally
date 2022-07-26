using Events;
using ScriptableObjects;
using UnityEngine;

public enum DataManagement
{
    AlwaysDelete,
    CreateOnce,
}
namespace Core
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance = null;
        [SerializeField]
        private PlayerSessionData playerData;
        public float playerCash;
        public float cashBonusModifier = 1.5f;
        [SerializeField]
        private DataManagement dataManagement;
        private void Awake()
        {
            if (Instance == null)
            {
                RenewSessionData();
                playerData.CashBonusModifier = cashBonusModifier;
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
            if(dataManagement == DataManagement.AlwaysDelete)
                GameEvents.OnSessionEndedEvent += RenewSessionData;
        }

       
        private void OnDisable()
        {
            GameEvents.OnWalletUpdatedEvent -= UpdatePlayerCash;
            GameEvents.OnSessionEndedEvent -= RenewSessionData;
        }
        
        private void UpdatePlayerCash(float value)
        {
            playerCash = 0;
            playerCash = value;
        }
        
        private void RenewSessionData()
        {
            Debug.Log("Trying to destroy playerData");
            
            if(playerData != null)
                DestroyImmediate(playerData);
            
            playerData = ScriptableObject.CreateInstance<PlayerSessionData>();
            playerData.CashBonusModifier = cashBonusModifier;
        }

    }
}
