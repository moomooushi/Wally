using Core;
using ScriptableObjects;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Receptacles;
using UnityEngine;

namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass(IngredientType ingredientType, ReceptacleType receptacleType, string levelName);
        public delegate void FluidExitGlass(IngredientType ingredientType, ReceptacleType receptacleType, string levelName);
        public delegate void DetermineLevelCompletion();
        public delegate void LevelCompleted();
        public delegate void LoadEndState();
        public delegate void UpdateWallet(float valueToAdd);
        public delegate void WalletUpdated(float value);
        public delegate void NextScene();
        public delegate void AudioCollision(AudioClip clip);
        public delegate void NewLevel();
        public delegate void SessionEnd();
        public delegate void LevelCreated(Level level);
        
        public static FluidInGlass OnIngredientEnterGlassEvent;
        public static FluidExitGlass OnIngredientExitGlassEvent;
        public static DetermineLevelCompletion OnIngredientUpdatedEvent;
        public static LevelCompleted OnLevelCompletedEvent;
        public static UpdateWallet OnUpdateWalletEvent;
        public static WalletUpdated OnWalletUpdatedEvent;
        public static LoadEndState OnShowLevelEndStateEvent;
        public static NextScene OnLoadNextSceneEvent;
        public static AudioCollision OnAudioCollisionEvent;
        public static NewLevel OnRequestNewLevelEvent;
        public static LevelCreated OnNewLevelCreatedEvent;
        public static SessionEnd OnSessionEndedEvent;
    }
}