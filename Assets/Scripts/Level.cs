using System;
using System.Collections;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelData level;
    [SerializeField][ReadOnly]
    private bool levelIsComplete;
    [SerializeField]
    private float levelCompleteTimeOut = 5;
    [SerializeField][ReadOnly]
    private bool runCoroutine = true;
    
    // todo: this needs to be refactored so that level progression is dynamic
    [SerializeField] public string sceneToLoad = "LevelCompleteScene";

    private void Start()
    {
        levelIsComplete = false;
    }

    private void OnEnable()
    {
        GameEvents.OnLevelCompletedEvent += CheckLevelCompleted;
    }
    
    private void OnDisable()
    {
        GameEvents.OnLevelCompletedEvent -= CheckLevelCompleted;
    }

    private void CheckLevelCompleted()
    {
        levelIsComplete = level.LevelComplete;
        LoadEndScene();
    }

    private void LoadEndScene()
    {
        if (levelIsComplete && runCoroutine)
        {
            runCoroutine = false;
            StartCoroutine(DelayEndSceneLoad());
        } 
    }
    
    IEnumerator DelayEndSceneLoad()
    {
        yield return new WaitForSeconds(levelCompleteTimeOut);
        if(sceneToLoad != null) {
            StopAllCoroutines();
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }
    }
}
