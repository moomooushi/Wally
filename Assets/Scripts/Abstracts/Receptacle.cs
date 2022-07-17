using ScriptableObjects;
using UnityEngine;

public abstract class Receptacle : MonoBehaviour
{    
    [Header("Receptacle Settings")]
    public ReceptacleType receptacleType;
    [ReadOnly]
    public Transform receptacleTransform;
    [ReadOnly]
    public Vector3 receptaclePosition;
    [Space(20)]
    [Header("Sprite Settings")]
    [ReadOnly]
    public SpriteRenderer spriteRenderer;
    [SerializeField] [Range(0,1)]
    private float glassTransparency = 0.9f;

    private void Awake()
    {
        AssignReceptacleValues();
        UpdateSpriteRenderer(receptacleType);
        UpdateColliders();
        AddTransparency();
        receptacleTransform = GetComponent<Transform>();
        receptaclePosition = receptacleTransform.position;
    }
    public void UpdateColliders()
    {
        _ = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        Debug.Log("added polygon col");
    }

    public void UpdateSpriteRenderer(ReceptacleType data)
    {
        if (GetComponent<SpriteRenderer>())
            DestroyImmediate(GetComponent<SpriteRenderer>());

        spriteRenderer = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        if (spriteRenderer != null)
            spriteRenderer.sprite = data.sprite;
    }
    
    public void AssignReceptacleValues()
    {
        if (receptacleType != null)
            name = receptacleType.name;
    }
    
    public void AddTransparency()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color glassSprite = sr.color;
        glassSprite.a = glassTransparency;
        sr.color = glassSprite;
    }

}
