using DG.Tweening;
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
        DOTweenTMPAnimator _animator;
        private Sequence _textSequence;
        private void Start()
        {
            if(dialogues != null)
                SetUpTextAnimations();
        }

//        private void Update()
//        {
////            foreach (string s in dialogues.list)
////            {
////                textField.text = s;
////            }
//        }
        void SetUpTextAnimations()
        {
            _textSequence =  DOTween.Sequence();
            _animator = new DOTweenTMPAnimator(textField);
            textField.text = "";

            foreach (string s in dialogues.list)
            {
                _textSequence.Append(textField.DOText(s,3)).AppendCallback(() => textField.text = "");
            }
        }
    }
}
