using System.Collections.Generic;
using ScriptableObjects.Receptacles;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ReceptacleTypesList_", menuName = "Receptacles/New Receptacle List", order = 0)]
    public class ReceptacleTypes : ScriptableObject
    {
        public ReceptacleType[] list;
    }
}