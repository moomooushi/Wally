using DG.Tweening;
using Events;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField]
        GameObject orderUIPrefab;
        private CanvasGroup _canvasAlpha;

        private void Awake()
        {
            _canvasAlpha = this.GetComponent<CanvasGroup>();
            _canvasAlpha.alpha = 0;
            
        }
        private void Start()
        {
            _canvasAlpha.DOFade(1, 1);
        }

        private void OnEnable()
        {
            GameEvents.OnNewLevelCreatedEvent += UpdateUI;
        }

        private void OnDisable()
        {
            GameEvents.OnNewLevelCreatedEvent -= UpdateUI;
        }

        void UpdateUI(Level level)
        {
            if (level == null)
                return;

            if (GetComponentInChildren<OrderEntryUI>())
            {
                Destroy(GetComponentInChildren<OrderEntryUI>().gameObject);
            }
            


            LevelData levelData = level.levelData;
            foreach (Entry entry in levelData.ingredientsList)
            {
                GameObject orderEntry = Instantiate(orderUIPrefab, transform);
                orderEntry.SetActive(true);
                var orderUI = orderEntry.GetComponent<OrderEntryUI>();
                if (entry.ingredientType.preferredReceptacle.sprite != null)
                    orderUI.receptacleGiver.sprite = entry.ingredientType.preferredReceptacle.sprite;
                else
                    orderUI.receptacleGiver.sprite = null;

                if (entry.receptacleRequirement.sprite != null)
                    orderUI.receptacleReceiver.sprite = entry.receptacleRequirement.sprite;
                else
                    orderUI.receptacleReceiver.sprite = null;
            }
        }
    }
}
