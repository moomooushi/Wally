using ScriptableObjects;

namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass(Ingredient ingredient);
        public delegate void FluidExitGlass(Ingredient ingredient);

        public static FluidInGlass OnFluidEnterGlassEvent;
        public static FluidExitGlass OnFluidExitGlassEvent;
    }
}