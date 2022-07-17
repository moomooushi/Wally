using Events;
using ScriptableObjects;
using UnityEngine;

namespace Ingredients
{
    public class Fluid : Ingredient
    {
    
        public FluidType fluidType;
        private GameObject _fluidsParent;
        private void Start()
        {
            _fluidsParent = GameObject.Find("FluidsParent");
            if (!_fluidsParent)
            {
                _fluidsParent = Instantiate(new GameObject("FluidsParent"));
            }
            _renderer = this.GetComponent<SpriteRenderer>();
            _renderer.color = fluidType.color;
        }

        ///<summary>
        /// In order to have the physics respect the objects correctly we have to parent the
        /// fluid to the bottle when the fluid is inside the bottle
        /// </summary>>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Receptacle>())
            {
                this.transform.parent = other.transform;
            
                if(other.GetComponent<Glass>())
                    GameEvents.OnIngredientEnterGlassEvent?.Invoke(this.fluidType);
            }
        }

        ///<summary>
        /// When the fluid leaves the parent we need to swap it to another element with a static position.
        /// </summary>>
        private void OnTriggerExit2D(Collider2D other)
        {

            if (other.GetComponent<Receptacle>())
            {
                if (_fluidsParent)
                {
                    this.transform.parent = _fluidsParent.transform;
                }
                if(other.GetComponent<Glass>())
                    GameEvents.OnIngredientExitGlassEvent?.Invoke(this.fluidType);
            }
        }
    }
}
