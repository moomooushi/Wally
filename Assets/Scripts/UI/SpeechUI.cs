using System.Collections.Generic;
using Events;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class SpeechUI : MonoBehaviour
    {
        [SerializeField]
        private Dialogues dialogues;
        [SerializeField]
        private TMP_Text textField;
        [SerializeField]
        private float durationPerCharacter = 0.1f;

        [SerializeField] private float delayBetweenStrings = 2f;
        private readonly AnimateText _textAnimator = new();
        
        private void OnEnable()
        {
            GameEvents.OnNewLevelCreatedEvent += NewOrderDialogue;
        }
        private void OnDisable()
        {
            GameEvents.OnNewLevelCreatedEvent -= NewOrderDialogue;
        }

        void NewOrderDialogue(Level level)
        {
            // Todo: This is on track, but needs a LOT of work
            string drink = "drinkName";
            string requirement = "requirementName";
            List<string> temp = new();
            foreach (Entry entry in level.levelData.ingredientsList)
            {
                drink = entry.ingredientType.name;
                requirement = entry.receptacleRequirement.name;
            }
            foreach (string s in dialogues.list)
            {
                temp.Add(s.Replace("{drink}", drink).Replace("{glass}", requirement));
            }
            _textAnimator.AnimateArray(textField, temp, durationPerCharacter, delayBetweenStrings);
        }
        
    }
}
