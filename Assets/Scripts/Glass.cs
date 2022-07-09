using UnityEngine;

public class Glass : Receptacle
{
    
    private void Awake()
    {

        if (receptacleType != null)
        {
            AssignReceptacleValues();
            UpdateSpriteRenderer(receptacleType);
            UpdateColliders();
            AddTransparency();
        }
        
        receptacleTransform = GetComponent<Transform>();
    }

    
}
