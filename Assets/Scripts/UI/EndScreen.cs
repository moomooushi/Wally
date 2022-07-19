using System.ComponentModel;
using Core;
using Events;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText, descriptionText, buttonText;
    [SerializeField]
    private Button _button;
    private LevelData levelData;
    [Header("Button Values")]
    [Tooltip("What do you want the button to stay when a level is detected as the next scene?")]
    [SerializeField] private string incrementLevel;
    [FormerlySerializedAs("mainMenu")]
    [Tooltip("What do you want the button to stay when there is no next scene?")]
    [SerializeField] private string fallBackButtonText;
    
    private void Awake()
    {
        titleText = FindTextObject("Title");
        descriptionText = FindTextObject("Description");
        _button = GetComponentInChildren<Button>();
        buttonText = FindTextObject("ButtonText");
        // todo: this level data thingy is not good here. I don't like. Should be decoupled.
        if (GameObject.Find("LevelData")) {
            levelData = GameObject.Find("LevelData").GetComponent<Level>().levelData;
            UpdateDescription(levelData.cashReward);
        }
        else
        {
            levelData = null;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += DestroyEndScreen;
    }
    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= DestroyEndScreen;
    }
    
    private void Start()
    {
        if(levelData != null)
            UpdateDescription(levelData.cashReward);

        if (!LevelManager.Instance.nextLevel.Contains("Level "))
        {
            buttonText.text = fallBackButtonText;
            _button.onClick.AddListener(SessionEnd);
        }
        else
        {
            buttonText.text = incrementLevel;
        }
    }

    private void SessionEnd()
    {
        GameEvents.OnSessionEndedEvent?.Invoke();
    }

    private void UpdateDescription(float valueToAdd)
    {
        descriptionText.text = $"You gained ${valueToAdd}";
    }

    TMP_Text FindTextObject(string nameOfObject)
    {
        return GameObject.Find(nameOfObject).GetComponentInChildren<TMP_Text>();
    }
    
    private void DestroyEndScreen(Scene arg0)
    {
        Destroy(this.gameObject);
    }
    
}
