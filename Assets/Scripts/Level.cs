using System.Collections;
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
    private void Update()
    {
        CheckLevelCompleted();
    }

    private void CheckLevelCompleted()
    {
        levelIsComplete = level.LevelComplete;
        if (levelIsComplete) StartCoroutine(EndLevel());
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(levelCompleteTimeOut);
        SceneManager.LoadScene("LevelCompleteScene", LoadSceneMode.Additive);
    }

    
}
