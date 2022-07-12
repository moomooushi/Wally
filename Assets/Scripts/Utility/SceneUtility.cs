using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtility : MonoBehaviour
{
    public static void LoadScene(int sceneToLoad)
    {
        Debug.Log("Load scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void LoadScene(string sceneToLoad)
    {
        Debug.Log("Load scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}