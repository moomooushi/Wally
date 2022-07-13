using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance = null;
    private PlayerSessionData playerData;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
