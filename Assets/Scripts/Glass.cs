using Events;
using Ingredients;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Glass : Receptacle
{
    
    ///<summary>
    /// In order to have the physics respect the objects correctly we have to parent the
    /// fluid to the bottle when the fluid is inside the bottle
    /// </summary>>
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.GetComponent<Fluid>())
        {
            FluidType otherType = other.GetComponent<Fluid>().fluidType;
            GameEvents.OnIngredientEnterGlassEvent?.Invoke(otherType, this.receptacleType, SceneManager.GetActiveScene().name);
        }
    }

    ///<summary>
    /// We need to tell the event engine that fluid has left this glass
    /// </summary>>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Fluid>())
        {
            FluidType otherType = other.GetComponent<Fluid>().fluidType;
            GameEvents.OnIngredientExitGlassEvent?.Invoke(otherType, this.receptacleType, SceneManager.GetActiveScene().name);
        }
    }
}
