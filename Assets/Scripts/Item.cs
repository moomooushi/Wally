using System;
using UnityEngine;
public class Item : MonoBehaviour, IInteractable
{
    Transform tr;
    private GameObject _fluidsParent;
    private void Awake()
    {
        _fluidsParent = GameObject.Find("FluidsParent");
        if (!_fluidsParent)
        {
            _fluidsParent = Instantiate(new GameObject("FluidsParent"));
        }
        tr = this.GetComponent<Transform>();
    }
    
    ///<summary>
    /// In order to have the physics respect the objects correctly we have to parent the
    /// fluid to the bottle when the fluid is inside the bottle
    /// </summary>>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Fluid>())
        {
            other.transform.parent = tr;
        }
    }
    ///<summary>
    /// When the fluid leaves the parent we need to swap it to another element with a static position.
    /// </summary>>
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.GetComponent<Fluid>())
        {
            if (_fluidsParent) {
                other.transform.parent = _fluidsParent.transform;
            }
        }
    }

//    public void ShootFluid()
//    {
//        Instantiate(liquidObject, tr.position, Quaternion.identity, tr);
//    }
}
