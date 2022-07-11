using UnityEngine;

public abstract class Ingredient : MonoBehaviour
{
    public SpriteRenderer _renderer;

    private void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

}