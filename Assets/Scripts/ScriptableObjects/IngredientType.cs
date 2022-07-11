using UnityEngine;

namespace ScriptableObjects
{
    public abstract class IngredientType : ScriptableObject
    {
        public Color color;
        public GameObject prefab;
    }
}