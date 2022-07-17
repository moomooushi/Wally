using System;
using System.Collections;
using Events;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText, descriptionText, buttonText;
    [SerializeField]
    private Button _button;
    private LevelData levelData;
    private void Awake()
    {
        titleText = FindTextObject("Title");
        descriptionText = FindTextObject("Description");
        _button = GetComponentInChildren<Button>();
        buttonText = FindTextObject("ButtonText");
        // todo: this level data thingy is not good here. I don't like. Should be decoupled.
        if (GameObject.Find("LevelData")) {
            levelData = GameObject.Find("LevelData").GetComponent<Level>().levelData;
        }
        else
        {
            levelData = null;
        }
    }

    private void Start()
    {
        if(levelData != null)
            UpdateDescription(levelData.cashReward);
    }

    private void UpdateDescription(float valueToAdd)
    {
        descriptionText.text = $"You gained ${valueToAdd}";
    }

    TMP_Text FindTextObject(string nameOfObject)
    {
        return GameObject.Find(nameOfObject).GetComponentInChildren<TMP_Text>();
    }
    
}
