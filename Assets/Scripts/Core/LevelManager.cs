using System;
using System.Collections;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance = null;
        [ReadOnly]
        public string previousLevel, currentLevel, nextLevel;
        public int currentSceneBuildID;
        private Level _currentLevelInstance;
        [SerializeField]
        private LevelData currentLevelData;
        private float levelCompleteTimeOut = 5;
        public GameObject levelCompleteUI;
        [SerializeField][ReadOnly]
        private bool runCoroutine = true;

        
        private void OnEnable()
        {
            GameEvents.OnLoadNextLevelEvent += LoadEndScene;
        }

        private void OnDisable()
        {
            GameEvents.OnLoadNextLevelEvent -= LoadEndScene;
        }

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
            
            _currentLevelInstance = GameObject.Find("LevelData").GetComponent<Level>();
            currentLevelData = _currentLevelInstance.levelData;
            currentLevel = currentLevelData.name;
            
            _ = currentLevel == "Level 1" ? previousLevel = null : previousLevel = UpdateLevelString(currentLevel, -1);
            nextLevel = UpdateLevelString(currentLevel, 1);
        }

        static string UpdateLevelString(string id, int increment)
        {
            string[] arr = id.Split(" ");
            return arr[0] + " " + (int.Parse(arr[1]) + increment);
        }
        
        private void LoadEndScene()
        {
            runCoroutine = false;
            StartCoroutine(DelayEndSceneLoad());
        }
        
        IEnumerator DelayEndSceneLoad()
        {
            yield return new WaitForSeconds(levelCompleteTimeOut);
            StopAllCoroutines();
            if(levelCompleteUI != null)
            {
                runCoroutine = true;
                Instantiate(levelCompleteUI);
            }
        }
    }
}