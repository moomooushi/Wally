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
        private bool runCoroutine;

        public bool RunCoroutine
        {
            get => runCoroutine;
            private set { runCoroutine = value;
                if (runCoroutine == false)
                {
                    StopAllCoroutines();
                }
            }
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameEvents.OnShowLevelEndStateEvent += LoadEndState;
            GameEvents.OnLoadNextSceneEvent += LoadNextScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            GameEvents.OnShowLevelEndStateEvent -= LoadEndState;
            GameEvents.OnLoadNextSceneEvent -= LoadNextScene;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            RunCoroutine = true;
        }

        static string UpdateLevelString(string id, int increment)
        {
            string[] arr = id.Split(" ");
            return arr[0] + " " + (int.Parse(arr[1]) + increment);
        }
        
        private void LoadEndState()
        {
            RunCoroutine = false;
            StartCoroutine(DelayEndStateLoad());
        }
        
        IEnumerator DelayEndStateLoad()
        {
            yield return new WaitForSeconds(levelCompleteTimeOut);
            if(levelCompleteUI != null && RunCoroutine == false)
            {
                RunCoroutine = true;
                Instantiate(levelCompleteUI);
            }
        }

        private void LoadNextScene()
        {
            if (currentLevel != null)
                SceneManager.LoadScene(nextLevel);
            else
                SceneManager.LoadScene(currentSceneBuildID + 1);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StopAllCoroutines();
            currentSceneBuildID = SceneManager.GetActiveScene().buildIndex;
            
            _currentLevelInstance = GameObject.Find("LevelData").GetComponent<Level>();
            currentLevelData = _currentLevelInstance.levelData;
            currentLevel = currentLevelData.name;
            
            _ = currentLevel == "Level 1" ? previousLevel = null : previousLevel = UpdateLevelString(currentLevel, -1);
            nextLevel = UpdateLevelString(currentLevel, 1);
        }
        
    }
}