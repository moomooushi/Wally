using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    [FormerlySerializedAs("level")] [SerializeField]
    public LevelData levelData;
    [SerializeField][ReadOnly]
    private bool levelIsComplete;

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
        levelIsComplete = levelData.LevelComplete;
        if(levelIsComplete) 
            GameEvents.OnShowLevelEndStateEvent?.Invoke();
    }
}
