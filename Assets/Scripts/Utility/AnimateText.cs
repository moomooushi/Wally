using System.Collections.Generic;
using DG.Tweening;
using TMPro;

namespace Utility
{
    public class AnimateText
    {
        public void AnimateArray(TMP_Text textField, List<string> list, float durationPerCharacter = 0.1f, float delayBetweenStrings = 2f, float initialDelay = 0)
        {
            Sequence textSequence =  DOTween.Sequence().SetDelay(initialDelay);

            textField.text = "";

            foreach (string s in list)
            {
                Tween tween = textField.DOText(s,s.Length * durationPerCharacter).SetEase(Ease.Linear);
                textSequence.Append(tween).AppendInterval(delayBetweenStrings).AppendCallback(() => textField.text = "");
            }
        }
    }
    
}