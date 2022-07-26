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
            if (LevelManager.Instance.gameMode == GameMode.Random || LevelManager.Instance.gameMode == GameMode.Progressive)
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
        
        void AssignCurrentCashText()
        {
            currentCashText = GameObject.Find("CurrentCashText").GetComponent<TMP_Text>();
        }

      
    }
}