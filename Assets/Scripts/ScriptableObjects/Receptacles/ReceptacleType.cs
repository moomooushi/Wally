using ScriptableObjects.MaterialInteractions;
using UnityEngine;

namespace ScriptableObjects.Receptacles
{
    public class ReceptacleType : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
        public IngredientType ingredientType;
        public ConstructionMaterialType constructionMaterial;
    }
}