namespace Events
{
    public class GameEvents
    {
        public delegate void FluidInGlass();
        public delegate void FluidExitGlass();

        public static FluidInGlass OnFluidEnterGlassEvent;
        public static FluidExitGlass OnFluidExitGlassEvent;
    }
}