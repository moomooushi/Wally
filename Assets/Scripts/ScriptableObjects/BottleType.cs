using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bottle_", menuName = "Bottles/New Bottle Type", order = 0)]
    public class BottleType : ScriptableObject
    {
        public string bottleName;
        public Sprite sprite;
        public Fluid fluidType;
    }
}