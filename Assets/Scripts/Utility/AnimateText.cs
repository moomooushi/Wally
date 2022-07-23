using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Utility
{
    public class AnimateText : MonoBehaviour
    {
        [SerializeField]
        private Dialogues dialogues;
        [SerializeField]
        private TMP_Text textField;

        private void Start()
        {
            textField.text = "";
        }

        private void Update()
        {
            foreach (string s in dialogues.list)
            {
                textField.text = s;
            }
        }
    }
}
