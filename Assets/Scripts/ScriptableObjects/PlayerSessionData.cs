using System.Collections.Generic;
using Events;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSessionData_", menuName = "Session Data/New Player Session Data", order = 0)]
    public class PlayerSessionData : ScriptableObject
    {
        [SerializeField][ReadOnly]
        private float _currentCash;
        public List<BottleType> inventory = new();

        public float CurrentCash
        { 
            get => _currentCash;
            private set => _currentCash = value;
        }

        private void OnEnable()
        {
            GameEvents.OnUpdateWalletEvent += UpdateCurrentCash;
        }

        private void OnDisable()
        {
            GameEvents.OnUpdateWalletEvent -= UpdateCurrentCash;
        }

        public void UpdateCurrentCash(float valueToAdd)
        {
            CurrentCash += valueToAdd;
            GameEvents.OnWalletUpdatedEvent?.Invoke(CurrentCash);
        }

        void UpdateInventory(BottleType bottleType)
        {
            inventory.Add(bottleType);
        }
    }
}