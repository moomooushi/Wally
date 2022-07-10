using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Fluid_", menuName = "Fluids/New Fluid", order = 0)]
    public class FluidType : ScriptableObject
    {
        public Color fluidColor;
        public GameObject prefab;
    }
}