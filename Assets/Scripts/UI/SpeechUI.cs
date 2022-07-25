using System;
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
        private CanvasGroup _canvasGroup;
        private RectTransform _speechBubbleRect;
        private Vector3 _rectPos;
        private Vector3 _originalPos;
        

        private void Awake()
        {
            _speechBubbleRect = this.gameObject.GetComponent<RectTransform>();
            _rectPos = _speechBubbleRect.localPosition;
            _originalPos = _rectPos;
            _rectPos.y += 10;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            GameEvents.OnNewLevelCreatedEvent += NewOrderDialogue;
            AnimateText.AnimationComplete += AnimateOut;
        }
        

        private void OnDisable()
        {
            DOTween.RewindAll();
            GameEvents.OnNewLevelCreatedEvent -= NewOrderDialogue;
            AnimateText.AnimationComplete -= AnimateOut;
        }

        void NewOrderDialogue(Level level)
        {
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
            AnimateIn();
        }
        
        void AnimateIn()
        {
            _canvasGroup.alpha = 0;
            _speechBubbleRect.transform.DOLocalMoveY(_rectPos.y + 20, .2f);
            _canvasGroup.DOFade(1,.2f);
        }
        
        void AnimateOut()
        {
            _speechBubbleRect.transform.DOLocalMoveY(_originalPos.y, .2f);
            _canvasGroup.DOFade(0,.2f);
        }
    }

    
}
