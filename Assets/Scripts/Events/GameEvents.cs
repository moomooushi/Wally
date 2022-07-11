using ScriptableObjects;

namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass(Ingredient ingredient);
        public delegate void FluidExitGlass(Ingredient ingredient);

        public delegate void DetermineLevelCompletion();
        
        public static FluidInGlass OnIngredientEnterGlassEvent;
        public static FluidExitGlass OnIngredientExitGlassEvent;
        public static DetermineLevelCompletion OnIngredientUpdatedEvent;
    }
}