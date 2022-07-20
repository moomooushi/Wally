using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    [FormerlySerializedAs("level")] [SerializeField]
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
        
        GameEvents.OnIngredientEnterGlassEvent += levelData.IncreaseCount;
        GameEvents.OnIngredientExitGlassEvent += levelData.ReduceCount;
    }
    
    private void OnDisable()
    {
        GameEvents.OnLevelCompletedEvent -= CheckLevelCompleted;
        GameEvents.OnIngredientEnterGlassEvent -= levelData.IncreaseCount; 
        GameEvents.OnIngredientExitGlassEvent -= levelData.ReduceCount;
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
