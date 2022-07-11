using ScriptableObjects;
using UnityEngine;

public class Bottle : Receptacle
{
    [Space(20)]
    [Header("Bottle Settings")]
    public FluidType fluidType;
    [ReadOnly] public GameObject fluidPrefab;
    [Range(0,60)]
    public int fillCount = 50;

    private void OnEnable()
    {
<<<<<<< HEAD

        if (fluidType != null)
        {
            fluidPrefab = fluidType.prefab;
            AssignReceptacleValues();
            UpdateSpriteRenderer(receptacleType);
            UpdateColliders();
            AddTransparency();
        }
        
        receptacleTransform = GetComponent<Transform>();
=======
        if (fluidType != null)
        {
            fluidPrefab = fluidType.prefab;
        }
>>>>>>> e951bae3ba33a2e1230a9ad4b966c53300cac773
    }

    private void Start()
    {
        FillBottle();
    }
    
    void FillBottle()
    {
        if (fluidPrefab != null)
        {
            var rb = this.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            for (int i = 0; i < fillCount; i++)
            {
                GameObject _ = Instantiate(fluidPrefab, receptacleTransform.position, Quaternion.identity, receptacleTransform);
                Fluid fluid = _.GetComponent<Fluid>();
                fluid.fluidType = fluidType;
            }
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
