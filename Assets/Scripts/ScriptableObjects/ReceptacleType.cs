using UnityEngine;

namespace ScriptableObjects
{
    public class ReceptacleType : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
        public IngredientType ingredientType;
    }
}