using Events;
using ScriptableObjects;
using UnityEngine;

namespace LevelGen
{
    public class LevelFactory : MonoBehaviour
    {
        [SerializeField][ReadOnly]
        private Level level;
        [SerializeField][ReadOnly]
        private LevelData levelData;
        [SerializeField][ReadOnly]
        private LevelDataGenerator levelDataGenerator;
        
        private void Awake()
        { 
            levelDataGenerator = GetComponent<LevelDataGenerator>();
            CreateNewLevel();
        }

        private void OnEnable()
        {
            GameEvents.OnRequestNewLevelEvent += CreateNewLevel;
        }

        private void OnDisable()
        {
            GameEvents.OnRequestNewLevelEvent -= CreateNewLevel;
        }

        void CreateNewLevel()
        {
            if (level != null)
                DestroyLevel();
            
            gameObject.AddComponent<Level>();
            
            if(gameObject.GetComponent<Level>()) {
                level = gameObject.GetComponent<Level>();
                if(level != null)
                    level.levelData = levelDataGenerator.GenerateNewLevelData();
                if(level.levelData != null)
                    levelData = level.levelData;
            }
            GameEvents.OnNewLevelCreatedEvent?.Invoke();
        }

        void DestroyLevel()
        {
            DestroyImmediate(level.levelData);
            Destroy(level);
        }
    }
}
