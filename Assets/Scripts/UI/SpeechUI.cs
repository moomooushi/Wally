using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        [Header("Animation Settings")]
        [SerializeField]
        private float durationPerCharacter = 0.1f;
        [SerializeField] private float delayBetweenStrings = 2f;
        private readonly AnimateText _textAnimator = new();
        [SerializeField] private float initialDelay;
        [Header("Replacement Variables")]
        [SerializeField]
        private string receptacleString = "{receptacle}";
        [SerializeField]
        private string ingredientString = "{ingredient}";
        List<string> temp = new();

        private void OnEnable()
        {
            DOTween.PlayAll();
            GameEvents.OnNewLevelCreatedEvent += NewOrderDialogue;
        }
        private void OnDisable()
        {
            DOTween.RewindAll();
            GameEvents.OnNewLevelCreatedEvent -= NewOrderDialogue;
        }

        void NewOrderDialogue(Level level)
        {
            // Todo: This is on track, but needs a LOT of work. When there is more than one entry we needed to add another list item before the last entry
            var drinkOrder = dialogues.list.Where(s => s.Contains(ingredientString) && s.Contains(receptacleString)).ToList();
            foreach (Entry entry in level.levelData.ingredientsList)
            {
                var ingredientName = entry.ingredientType.name;
                var receptacleName = entry.receptacleRequirement.name;
                
                foreach (string order in drinkOrder)
                {
                  temp.Add(order.Replace(ingredientString, ingredientName).Replace(receptacleString, receptacleName));
                }
            }

            List<string> combinedList = new();
            dialogues.list.ForEach(item=> combinedList.Add(item));
            var tobeRemoved = combinedList.FindIndex(f=>f.Contains(ingredientString) && f.Contains(receptacleString));
            combinedList.RemoveAt(tobeRemoved);
            combinedList.InsertRange(tobeRemoved, temp);
            temp.Clear();
            
            _textAnimator.AnimateArray(textField, combinedList, durationPerCharacter, delayBetweenStrings, initialDelay);
        }
    }

    
}
