using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Receptacles;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Levels/New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public new string name;
        public List<Entry> ingredientsList = new();
        [SerializeField]
        private float cashReward;

        public float CashReward
        {
            get => cashReward;
            private set => cashReward = value;
        }

        [SerializeField]
        private bool rewardGiven = false;
        [SerializeField]
        private bool levelComplete;
        public bool LevelComplete
        {
            get => levelComplete;
            private set
            {
                levelComplete = value;
                if (levelComplete == true)
                {

                    if (rewardGiven == false)
                    {
                        rewardGiven = true;
                        GameEvents.OnUpdateWalletEvent?.Invoke(CashReward);
                    } 
                        
                    GameEvents.OnLevelCompletedEvent?.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            GameEvents.OnIngredientUpdatedEvent += SetLevelCompleted;
            GameEvents.OnIngredientEnterGlassEvent += IncreaseCount;
            GameEvents.OnIngredientExitGlassEvent += ReduceCount;
            SceneManager.sceneLoaded += ResetValues;
            ResetValues();
        }
        
        private void OnDisable()
        {
            GameEvents.OnIngredientUpdatedEvent -= SetLevelCompleted;
            GameEvents.OnIngredientEnterGlassEvent -= IncreaseCount; 
            GameEvents.OnIngredientExitGlassEvent -= ReduceCount;
            SceneManager.sceneLoaded -= ResetValues;
            ResetValues();
        }

        public void IncreaseCount(IngredientType ingredientType, ReceptacleType receptacleRequirement)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredientType == ingredientType && s.receptacleRequirement == receptacleRequirement);
            
            if(i != null)
                i.IncreaseCount();
            
            GameEvents.OnIngredientUpdatedEvent?.Invoke();
        }
        
        public void ReduceCount(IngredientType ingredientType, ReceptacleType receptacleRequirement)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredientType == ingredientType && s.receptacleRequirement == receptacleRequirement);
           
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
            rewardGiven = false;
        }
        
        private void ResetValues(Scene arg0, LoadSceneMode arg1)
        {
            ResetValues();
        }

        private void SetLevelCompleted()
        { 
            bool testComplete = ingredientsList.All(s => s.complete);
            LevelComplete = testComplete;
            Debug.Log("All level Reqs complete: " + testComplete);
        }

        public void SetReward()
        {
            float value = 0;
            foreach (Entry entry in ingredientsList)
            {
                value += entry.ingredientType.ingredientPrice * entry.requirement;
            }
            CashReward = value;
        }
       
    }
}