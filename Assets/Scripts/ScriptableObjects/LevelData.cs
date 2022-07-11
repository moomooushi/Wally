using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Levels/New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public new string name;
        public List<Entry> ingredientsList = new();
        [SerializeField]
        private bool _levelComplete;
        public bool LevelComplete
        {
            get => _levelComplete;
            private set => _levelComplete = value;
        }
        private void OnEnable()
        {
            GameEvents.OnFluidEnterGlassEvent += IncreaseCount;
            GameEvents.OnFluidExitGlassEvent += ReduceCount;
            GameEvents.OnFluidEnterGlassEvent += SetLevelCompleted;
        }
        
        private void OnDisable()
        {
            GameEvents.OnFluidEnterGlassEvent -= IncreaseCount; 
            GameEvents.OnFluidExitGlassEvent -= ReduceCount;
            GameEvents.OnFluidEnterGlassEvent -= SetLevelCompleted;
            ResetCounts();
        }

        private void Reset()
        {
            ResetCounts();
        }

        private void IncreaseCount(Ingredient ingredient)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredient == ingredient);
            
            if(i != null )
                i.IncreaseCount();
        }
        
        private void ReduceCount(Ingredient ingredient)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredient == ingredient);
           
            if(i != null )
                i.ReduceCount();
        }

        void ResetCounts()
        {
            foreach (Entry entry in ingredientsList)
            {
                entry.ResetEntry();
            }

            LevelComplete = false;
        }

        private void SetLevelCompleted(Ingredient ingredient)
        {
            Debug.Log("We tried to check if the level was complete");
           LevelComplete = ingredientsList.All(s => s.complete);
        }
       
    }
}