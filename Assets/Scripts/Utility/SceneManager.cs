using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
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
}