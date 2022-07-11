using System;
using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Levels/New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public FluidType fluid;
            public int requirement;
            public int count;
            public bool complete;
            
            public void SetLevelComplete()
            {
                _ = count >= requirement ? complete : !complete;
            }

            public void ReduceCount()
            {
                count--;
            }

            public void IncreaseCount()
            {
                count++;
            }
        }
        public new string name;
        public List<Entry> ingredientsList = new();
       
    }
}