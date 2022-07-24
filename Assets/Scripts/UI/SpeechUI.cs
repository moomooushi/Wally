using System.Collections.Generic;
using System.Linq;
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
        [SerializeField]
        List<string> temp = new();
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
            // Todo: This is on track, but needs a LOT of work. When there is more than one entry we needed to add another list item before the last entry
            string drink = "drinkName";
            string requirement = "requirementName";
            var drinkOrder = dialogues.list.Where(s => s.Contains("{drink}") || s.Contains("{glass}"));

            foreach (Entry entry in level.levelData.ingredientsList)
            {
                drink = entry.ingredientType.name;
                requirement = entry.receptacleRequirement.name;
                foreach (string s in dialogues.list)
                {
                    temp.Add(s.Replace("{drink}", drink).Replace("{glass}", requirement));
                }
            }
            
            _textAnimator.AnimateArray(textField, temp, durationPerCharacter, delayBetweenStrings);
        }
        
    }
}
