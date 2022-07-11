<<<<<<< HEAD
﻿using UnityEngine;

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

    
=======
﻿public class Glass : Receptacle
{
  
>>>>>>> e951bae3ba33a2e1230a9ad4b966c53300cac773
}
