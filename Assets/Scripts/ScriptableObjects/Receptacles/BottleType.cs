﻿using UnityEngine;

namespace ScriptableObjects.Receptacles
{
    [CreateAssetMenu(fileName = "Bottle_", menuName = "Bottles/New Bottle Type", order = 0)]
    public class BottleType : ReceptacleType
    {
        [Range(0,200)]
        public int fillCount = 50;
        [Space(20)]
        [SerializeField][ReadOnly]
        private float totalPrice;
        public float TotalPrice
        {
            get => totalPrice;
            private set => totalPrice = value;
        }

        private void OnEnable()
        {
            TotalPrice = GetPrice();
        }

        private void OnValidate()
        {
            TotalPrice = GetPrice();
        }

        float GetPrice()
        {
            return ingredientType.ingredientPrice * fillCount;
        }
    }
}