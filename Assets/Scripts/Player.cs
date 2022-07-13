using System;
using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerSessionData playerData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
