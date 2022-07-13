using ScriptableObjects;

namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass(IngredientType ingredientType);
        public delegate void FluidExitGlass(IngredientType ingredientType);
        public delegate void DetermineLevelCompletion();
        public delegate void LevelCompleted();
        public delegate void UpdateWallet(float valueToAdd);
        public delegate void WalletUpdated(float value);
        
        public static FluidInGlass OnIngredientEnterGlassEvent;
        public static FluidExitGlass OnIngredientExitGlassEvent;
        public static DetermineLevelCompletion OnIngredientUpdatedEvent;
        public static LevelCompleted OnLevelCompletedEvent;
        public static UpdateWallet OnUpdateWalletEvent;
        public static WalletUpdated OnWalletUpdatedEvent;
    }
}