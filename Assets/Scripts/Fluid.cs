using UnityEngine;
using ScriptableObjects;

public class Fluid : MonoBehaviour
{
    public FluidType fluidType;

    private SpriteRenderer _renderer;
    

    private void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = fluidType.fluidColor;
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
