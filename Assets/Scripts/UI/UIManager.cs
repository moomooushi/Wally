using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currentCashText;
        private void OnEnable()
        {
           
            GameEvents.OnWalletUpdatedEvent += UpdateCash;
        }
        private void OnDisable()
        {
            GameEvents.OnWalletUpdatedEvent -= UpdateCash;
        }
        private void UpdateCash(float value)
        {
            currentCashText.text = value.ToString();
        }

      
    }
}