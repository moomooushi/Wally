using System.Collections.Generic;
using Events;
using ScriptableObjects.Receptacles;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSessionData_", menuName = "Session Data/New Player Session Data", order = 0)]
    public class PlayerSessionData : ScriptableObject
    {
        [SerializeField][ReadOnly]
        private float currentCash;
        public List<BottleType> inventory = new();
        [FormerlySerializedAs("_cashBonusModifier")] [SerializeField]
        private float cashBonusModifier;
        public float CashBonusModifier
        {
            get => cashBonusModifier;
            set => cashBonusModifier = value;
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
            float valueTimesCashBonus = valueToAdd * CashBonusModifier;
            CurrentCash += valueTimesCashBonus;
            GameEvents.OnWalletUpdatedEvent?.Invoke(CurrentCash);
        }

        void UpdateInventory(BottleType bottleType)
        {
            inventory.Add(bottleType);
        }
        
    }
}