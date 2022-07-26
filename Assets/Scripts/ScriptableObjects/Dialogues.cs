using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "Dialogues/New Dialogue", order = 0)]
    public class Dialogues : ScriptableObject
    {
        public List<string> list = new();
    }
}