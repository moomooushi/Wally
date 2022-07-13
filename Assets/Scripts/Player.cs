using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance = null;
    [SerializeField]
    private PlayerSessionData playerData;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        playerData = ScriptableObject.CreateInstance<PlayerSessionData>();
    }
}
