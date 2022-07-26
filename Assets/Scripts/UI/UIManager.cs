using System.Globalization;
using Core;
using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currentCashText;
        [SerializeField]
        private SpeechUI newOrderPopup;
        private void Start()
        {
            if (LevelManager.Instance.currentLevel.Contains("Level"))
            {
                AssignCurrentCashText();
                currentCashText.text = PlayerManager.Instance.playerCash.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                currentCashText = null;
            }
        }

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
            if (currentCashText == null)
                return;
            
            currentCashText.text = value.ToString(CultureInfo.CurrentCulture);
            Debug.Log("we are updating the currentCash in the register");
        }
        
        // Todo: need to complete this method so that when a new scene is loaded we reassign the currentCashText
        void AssignCurrentCashText()
        {
            currentCashText = GameObject.Find("CurrentCashText").GetComponent<TMP_Text>();
        }

      
    }
}