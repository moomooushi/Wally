using ScriptableObjects;

namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass(IngredientType ingredientType);
        public delegate void FluidExitGlass(IngredientType ingredientType);

        public delegate void DetermineLevelCompletion();
        
        public static FluidInGlass OnIngredientEnterGlassEvent;
        public static FluidExitGlass OnIngredientExitGlassEvent;
        public static DetermineLevelCompletion OnIngredientUpdatedEvent;
    }
}