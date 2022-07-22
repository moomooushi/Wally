using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "IngredientTypesList_", menuName = "Ingredients/New Ingredients List", order = 0)]
    public class IngredientTypes : ScriptableObject
    {
        public IngredientType[] list;
    }
}