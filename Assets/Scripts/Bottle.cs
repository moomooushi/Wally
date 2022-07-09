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

    private void Awake()
    {

        if (fluidType != null)
        {
            fluidPrefab = fluidType.prefab;
            AssignReceptacleValues();
            UpdateSpriteRenderer(receptacleType);
            UpdateColliders();
            AddTransparency();
        }
        
        receptacleTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        FillBottle();
    }
    
    void FillBottle()
    {
        if (fluidPrefab != null)
        {
            for (int i = 0; i < fillCount; i++)
            {
                GameObject _ = Instantiate(fluidPrefab, receptacleTransform.position, Quaternion.identity, receptacleTransform);
                Fluid fluid = _.GetComponent<Fluid>();
                fluid.fluidType = fluidType;
            }
        }
    }
}
