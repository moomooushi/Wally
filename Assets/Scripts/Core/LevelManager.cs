using System.Collections;
using System.Linq;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode {
    Random,
    Progressive
}

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance = null;
        [ReadOnly]
        public string previousLevel, currentLevel, nextLevel;
        public int currentSceneBuildID;
        [SerializeField]
        private Level _currentLevelInstance;
        [SerializeField] public LevelData currentLevelData;
        [SerializeField] private float levelCompleteTimeOut = 5;
        public GameObject levelCompleteUI;
        [SerializeField][ReadOnly] private bool runCoroutine;
        [SerializeField] private string fallBackScene = "MainMenu";
        public GameMode gameMode = GameMode.Random;
        public bool RunCoroutine
        {
            get => runCoroutine;
            private set => runCoroutine = value;
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
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameEvents.OnShowLevelEndStateEvent += LoadEndState;
            GameEvents.OnNewLevelCreatedEvent += GetCurrentLevelData;
            
            if(gameMode == GameMode.Progressive)
                GameEvents.OnLoadNextSceneEvent += LoadNextScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            GameEvents.OnShowLevelEndStateEvent -= LoadEndState;
            GameEvents.OnNewLevelCreatedEvent -= GetCurrentLevelData;
            GameEvents.OnLoadNextSceneEvent -= LoadNextScene;
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
            // We don't need this code below to run if we are in the random level mode
            GetCurrentLevelData(null);
            if (gameMode == GameMode.Random)
            {
                return;
            }

            ManageSceneNames();

        }

        void GetCurrentLevelData(Level level)
        {
            if (level != null)
            {
                _currentLevelInstance = level;
                SetLevelData();
            } else {
                FindLevelDataInScene("LevelData");
            }
            
            SetLevelData();
            
            void SetLevelData()
            {
                if (_currentLevelInstance != null)
                {
                    currentLevelData = _currentLevelInstance.levelData;
                    currentLevel = SceneManager.GetActiveScene().name;
                }  
            }
        }

        void FindLevelDataInScene(string toFind)
        {
            if (GameObject.Find(toFind)) {
                _currentLevelInstance = GameObject.Find(toFind).GetComponent<Level>();
            }
        }

        void ManageSceneNames()
        {
            if( currentLevel == "Level 1")
            {
                previousLevel = null;
            } else {
                 previousLevel = UpdateLevelString(currentLevel, -1);
            }
                
            string nextLevelTemp = UpdateLevelString(currentLevel, 1);
                
            string[] nextSceneName = UnityEngine.SceneManagement.SceneUtility
                .GetScenePathByBuildIndex(currentSceneBuildID + 1).Split("/");
                
            nextLevel = nextLevelTemp == nextSceneName.Last().Replace(".unity", "")
                ? nextLevelTemp
                : nextLevel = fallBackScene;
                
            Debug.Log(nextLevelTemp + " " + nextSceneName.Last());
        }
        
        static string UpdateLevelString(string id, int increment)
        {
            int i;
            var arr = id.Split(" ");
            if(int.TryParse(arr.Last(), out i) ) {
                return arr[0] + " " + arr.Last() + increment;
            }

            return null;
        }
        
    }
}