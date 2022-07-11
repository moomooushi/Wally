using UnityEngine;

namespace ScriptableObjects
{
    public abstract class Ingredient : ScriptableObject
    {
        public Color color;
        public GameObject prefab;
    }
}