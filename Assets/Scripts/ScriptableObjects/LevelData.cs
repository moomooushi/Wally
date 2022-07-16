using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Levels/New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public new string name;
        public List<Entry> ingredientsList = new();
        public float cashReward;
        [SerializeField]
        private bool _rewardGiven = false;
        [FormerlySerializedAs("_levelComplete")] [SerializeField]
        private bool levelComplete;
        public bool LevelComplete
        {
            get => levelComplete;
            private set
            {
                levelComplete = value;
                if (levelComplete == true)
                {

                    if (_rewardGiven == false)
                    {
                        _rewardGiven = true;
                        GameEvents.OnUpdateWalletEvent?.Invoke(cashReward);
                    } 
                        
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
            ResetValues();
        }

        private void Reset()
        {
            ResetValues();
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

        void ResetValues()
        {
            foreach (Entry entry in ingredientsList)
            {
                entry.ResetEntry();
            }

            LevelComplete = false;
            _rewardGiven = false;
        }

        private void SetLevelCompleted()
        { 
            Debug.Log("We tried to check if the level was complete");
            LevelComplete = ingredientsList.All(s => s.complete);
        }
       
    }
}