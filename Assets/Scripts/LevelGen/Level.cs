using Events;
using ScriptableObjects;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    public LevelData levelData;
    [SerializeField][ReadOnly]
    private bool levelIsComplete, levelCompleteEventSent;

    private void Start()
    {
        levelIsComplete = false;
        levelCompleteEventSent = false;
    }

    private void OnEnable()
    {
        if(!levelCompleteEventSent)
            GameEvents.OnLevelCompletedEvent += CheckLevelCompleted;
        
        
    }
    
    private void OnDisable()
    {
        GameEvents.OnLevelCompletedEvent -= CheckLevelCompleted;
   
    }

    private void CheckLevelCompleted()
    {
        levelIsComplete = levelData.LevelComplete;
        if(levelIsComplete && !levelCompleteEventSent)
        {
            GameEvents.OnShowLevelEndStateEvent?.Invoke();
            levelCompleteEventSent = true;
        }
    }
}
