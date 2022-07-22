using System.Collections.Generic;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGen
{
    public class LevelDataGenerator : MonoBehaviour
    {
        [SerializeField]
        private IngredientTypes ingredientsTypes;
        [SerializeField]
        private ReceptacleTypes receptaclesTypes;
        [SerializeField]
        private int2 ingredients;
        [SerializeField]
        private int2 requirements;

        public LevelData GenerateNewLevelData()
        {
           LevelData newLevelData = ScriptableObject.CreateInstance<LevelData>();
           newLevelData.name = "Random Level";
           int numberOfEntries = Random.Range(ingredients.x, ingredients.y);
           newLevelData.ingredientsList = CreateIngredientList(numberOfEntries);
           return newLevelData; 
        }

        List<Entry> CreateIngredientList(int numberOfEntries)
        {
            List<Entry> ingredientList = new();
            for (int i = 0; i < numberOfEntries; i++)
            {
                Entry entry = new Entry();
                entry.ingredientType = ingredientsTypes.list[Random.Range(0, ingredientsTypes.list.Length)];
                entry.receptacleRequirement = receptaclesTypes.list[Random.Range(0, receptaclesTypes.list.Length)];
                entry.requirement = Random.Range(requirements.x,requirements.y);
                ingredientList.Add(entry);
            }
            return ingredientList;
        }
    }
}