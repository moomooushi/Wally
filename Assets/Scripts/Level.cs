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
    private bool _runCoroutine;

    private void Update()
    {
        CheckLevelCompleted();
    }

    private void CheckLevelCompleted()
    {
        levelIsComplete = level.LevelComplete;
        if (levelIsComplete && _runCoroutine)
        {
            _runCoroutine = false;
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(levelCompleteTimeOut);
        Debug.Log("Scene is gonna load");
        SceneManager.LoadScene("LevelCompleteScene");
    }
}
