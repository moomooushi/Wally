using System.Collections.Generic;
using Events;
using ScriptableObjects.Receptacles;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSessionData_", menuName = "Session Data/New Player Session Data", order = 0)]
    public class PlayerSessionData : ScriptableObject
    {
        [SerializeField][ReadOnly]
        private float currentCash;
        public List<BottleType> inventory = new();
        [SerializeField]
        private float _cashBonusModifier;
        public float CashBonusModifier
        {
            get => _cashBonusModifier;
            set => _cashBonusModifier = value;
        }

        public float CurrentCash
        { 
            get => currentCash;
            private set => currentCash = value;
        }

        private void OnEnable()
        {
            GameEvents.OnUpdateWalletEvent += UpdateCurrentCash;
        }

        private void OnDisable()
        {
            GameEvents.OnUpdateWalletEvent -= UpdateCurrentCash;
        }

        private void UpdateCurrentCash(float valueToAdd)
        {
            CurrentCash += valueToAdd * CashBonusModifier;
            GameEvents.OnWalletUpdatedEvent?.Invoke(CurrentCash);
        }

        void UpdateInventory(BottleType bottleType)
        {
            inventory.Add(bottleType);
        }
        
    }
}