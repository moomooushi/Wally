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
            private set
            {
                _levelComplete = value;
                if (_levelComplete == true)
                {
                    GameEvents.OnLevelCompletedEvent?.Invoke();
                }
            }
        }
        private void OnEnable()
        {
            GameEvents.OnIngredientEnterGlassEvent += IncreaseCount;
            GameEvents.OnIngredientExitGlassEvent += ReduceCount;
            GameEvents.OnIngredientUpdatedEvent += SetLevelCompleted;
        }
        
        private void OnDisable()
        {
            GameEvents.OnIngredientEnterGlassEvent -= IncreaseCount; 
            GameEvents.OnIngredientExitGlassEvent -= ReduceCount;
            GameEvents.OnIngredientUpdatedEvent -= SetLevelCompleted;
            ResetCounts();
        }

        private void Reset()
        {
            ResetCounts();
        }

        private void IncreaseCount(IngredientType ingredientType)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredientType == ingredientType);
            
            if(i != null )
                i.IncreaseCount();
            
            GameEvents.OnIngredientUpdatedEvent?.Invoke();
        }
        
        private void ReduceCount(IngredientType ingredientType)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredientType == ingredientType);
           
            if(i != null )
                i.ReduceCount();
            
            GameEvents.OnIngredientUpdatedEvent?.Invoke();
        }

        void ResetCounts()
        {
            foreach (Entry entry in ingredientsList)
            {
                entry.ResetEntry();
            }

            LevelComplete = false;
        }

        private void SetLevelCompleted()
        { 
            Debug.Log("We tried to check if the level was complete");
            LevelComplete = ingredientsList.All(s => s.complete);
        }
       
    }
}