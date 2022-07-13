using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance = null;
        [ReadOnly]
        public string previousLevel, currentLevel, nextLevel;
        public int currentSceneBuildID;
        [SerializeField]
        private LevelData currentLevelData;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            currentSceneBuildID = SceneManager.GetActiveScene().buildIndex;
            
//            if (!GameObject.Find("LevelData")) return;
            
            currentLevelData = GameObject.Find("LevelData").GetComponent<Level>().level;
            currentLevel = currentLevelData.name;
            _ = currentLevel == "Level 1" ? previousLevel = null : previousLevel = UpdateLevelString(currentLevel, -1);
            nextLevel = UpdateLevelString(currentLevel, 1);
        }

        static string UpdateLevelString(string id, int increment)
        {
            string[] arr = id.Split(" ");
            return arr[0] + " " + (int.Parse(arr[1]) + increment);
        }
    }
}