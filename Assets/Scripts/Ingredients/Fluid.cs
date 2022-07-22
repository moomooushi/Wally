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
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Receptacle>())
                if (_fluidsParent)
                    this.transform.parent = _fluidsParent.transform;
        }
    }
}
