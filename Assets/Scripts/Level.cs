using System;
using System.Linq;
using Events;
using ScriptableObjects;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelData level;
    [SerializeField]
    private bool levelIsComplete;

    private void OnEnable()
    {
        GameEvents.OnFluidEnterGlassEvent += CheckCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnFluidEnterGlassEvent -= CheckCompleted;
    }
    
    private void CheckCompleted()
    {
        _ = level.ingredientsList.All(e => e.complete) && levelIsComplete;
    }
}
