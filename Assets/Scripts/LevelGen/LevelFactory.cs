using System;
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
        }

        private void Start()
        {
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
            {
                DestroyLevel();
                level = gameObject.AddComponent<Level>();
            }
            else
                level = gameObject.AddComponent<Level>();
            
            if(level) {
                level.levelData = levelDataGenerator.GenerateNewLevelData();
                levelData = level.levelData;
                levelData.name = "Random Levels";
            }
            GameEvents.OnNewLevelCreatedEvent?.Invoke(level);
        }

        void DestroyLevel()
        {
            DestroyImmediate(level.levelData);
            Destroy(level);
        }
    }
}
