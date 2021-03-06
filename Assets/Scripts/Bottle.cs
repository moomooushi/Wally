using System;
using System.Collections;
using Ingredients;
using ScriptableObjects;
using ScriptableObjects.Receptacles;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bottle : Receptacle
{
    [Space(20)]
    [Header("Bottle Settings")]
    public FluidType fluidType;
    [ReadOnly] public GameObject fluidPrefab;
    private int _fillCount = 50;
    private void OnEnable()
    {
        if (fluidType != null)
        {
            fluidPrefab = fluidType.prefab;
        }
    }

    private void Start()
    {
        FillBottle();
    }
    
    void FillBottle()
    {
        if (receptacleType && fluidPrefab != null ) {
            
            BottleType bottleType = (BottleType)receptacleType;
            _fillCount = bottleType.fillCount;

            var rb = this.GetComponent<Rigidbody2D>();
            
            
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            for (int i = 0; i < _fillCount; i++)
            {
                float randomiseX = Random.Range(0f, Single.Epsilon);
                Vector3 newPos = new Vector3(receptaclePosition.x - randomiseX,
                    receptaclePosition.y, receptaclePosition.z);
                GameObject _ = Instantiate(fluidPrefab, newPos, Quaternion.identity, receptacleTransform);
                Fluid fluid = _.GetComponent<Fluid>();
                fluid.fluidType = fluidType;
            }
            StartCoroutine(ResetRbConstraint(rb));
        }
    }

    IEnumerator ResetRbConstraint(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
