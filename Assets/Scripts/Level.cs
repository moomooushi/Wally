using ScriptableObjects;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelData level;
    [SerializeField][ReadOnly]
    private bool levelIsComplete;

    private void Update()
    {
        CheckLevelCompleted();
    }
    
    private void CheckLevelCompleted()
    {
        levelIsComplete = level.LevelComplete;
    }
}
