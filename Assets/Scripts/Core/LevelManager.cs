using System.Collections;
using System.Linq;
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
        [SerializeField] private LevelData currentLevelData;
        [SerializeField] private float levelCompleteTimeOut = 5;
        public GameObject levelCompleteUI;
        [SerializeField][ReadOnly] private bool runCoroutine;
        [SerializeField] private string fallBackScene = "MainMenu";
        public bool RunCoroutine
        {
            get => runCoroutine;
            private set => runCoroutine = value;
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
            } else if (Instance != null)
            {
                Destroy(this.gameObject);
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
            Debug.Log("SHOWING END STATE");
            if (levelCompleteUI == null)
                return;
            
            RunCoroutine = false;
            
            if(RunCoroutine == false)
                StartCoroutine(DelayEndStateLoad());
        }
        
        IEnumerator DelayEndStateLoad()
        {
            yield return new WaitForSeconds(levelCompleteTimeOut);
            Instantiate(levelCompleteUI);
            StopAllCoroutines();
            RunCoroutine = true;
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
            currentSceneBuildID = scene.buildIndex;
            if (GameObject.Find("LevelData")) {
                _currentLevelInstance = GameObject.Find("LevelData").GetComponent<Level>();
            }
            currentLevelData = _currentLevelInstance.levelData;
            currentLevel = currentLevelData.name;
            
            
            if (!currentLevel.Contains("Random Level"))
            {
                _ = currentLevel == "Level 1" ? previousLevel = null : previousLevel = UpdateLevelString(currentLevel, -1);
                string nextLevelTemp = UpdateLevelString(currentLevel, 1);
                string[] nextSceneName = UnityEngine.SceneManagement.SceneUtility
                    .GetScenePathByBuildIndex(currentSceneBuildID + 1).Split("/");
                nextLevel = nextLevelTemp == nextSceneName.Last().Replace(".unity", "")
                    ? nextLevelTemp
                    : nextLevel = fallBackScene;
                Debug.Log(nextLevelTemp + " " + nextSceneName.Last());
            }
            
        }
        
    }
}