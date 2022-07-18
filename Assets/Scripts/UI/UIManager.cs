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

        private void Start()
        {
            currentCashText.text = PlayerManager.Instance.playerCash.ToString(CultureInfo.CurrentCulture);
        }

        private void OnEnable()
        {
            GameEvents.OnUpdateWalletEvent += UpdateCash;
        }
        private void OnDisable()
        {
            GameEvents.OnUpdateWalletEvent -= UpdateCash;
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
            currentCashText.GetComponent<TMP_Text>();
        }

      
    }
}