using Events;
using ScriptableObjects;
using UnityEngine;

namespace LevelGen
{
    public class LevelFactory : MonoBehaviour
    {
        [SerializeField]
        private Level level;
        [SerializeField]
        private LevelData levelData;
        [SerializeField]
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
            {
                DestroyLevel();
            }
            level = gameObject.AddComponent<Level>();
            level.levelData = levelDataGenerator.GenerateNewLevelData();
            levelData = level.levelData;
        }

        void DestroyLevel()
        {
            DestroyImmediate(level.levelData);
            Destroy(level);
        }
    }
}
