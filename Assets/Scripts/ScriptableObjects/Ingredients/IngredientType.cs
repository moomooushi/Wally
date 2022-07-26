using UnityEngine;

namespace ScriptableObjects.Ingredients
{
    public abstract class IngredientType : ScriptableObject
    {
        public new string name;
        public Color color;
        public GameObject prefab;
        public float ingredientPrice;
    }
}