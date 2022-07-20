using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Levels/New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public new string name;
        public List<Entry> ingredientsList = new();
        public float cashReward;
        [FormerlySerializedAs("_rewardGiven")] [SerializeField]
        private bool rewardGiven = false;
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

                    if (rewardGiven == false)
                    {
                        rewardGiven = true;
                        GameEvents.OnUpdateWalletEvent?.Invoke(cashReward);
                    } 
                        
                    GameEvents.OnLevelCompletedEvent?.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            GameEvents.OnIngredientUpdatedEvent += SetLevelCompleted;
            SceneManager.sceneLoaded += ResetValues;
            ResetValues();
        }

       
        private void OnDisable()
        {
            GameEvents.OnIngredientUpdatedEvent -= SetLevelCompleted;
            SceneManager.sceneLoaded -= ResetValues;
            ResetValues();
        }

        public void IncreaseCount(IngredientType ingredientType)
        {
            var i = ingredientsList.FirstOrDefault(s => s.ingredientType == ingredientType);
            
            if(i != null )
                i.IncreaseCount();
            
            GameEvents.OnIngredientUpdatedEvent?.Invoke();
        }
        
        public void ReduceCount(IngredientType ingredientType)
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
       
    }
}