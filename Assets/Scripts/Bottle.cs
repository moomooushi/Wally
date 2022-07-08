using ScriptableObjects;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public BottleType bottleType;
    public FluidType fluidType;
    private Transform _tr;
    private GameObject _fluidsParent;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] public GameObject fluidPrefab;
    public int fillCount = 50;

    private void Awake()
    {

        if (bottleType != null)
        {
            fluidPrefab = bottleType.fluidPrefab;
            AssignBottleType();
            UpdateSpriteRenderer();
            UpdateColliders();
        }

        _fluidsParent = GameObject.Find("FluidsParent");
        if (!_fluidsParent)
        {
            _fluidsParent = Instantiate(new GameObject("FluidsParent"));
        }

        _tr = GetComponent<Transform>();
    }

    private void Start()
    {
        FillBottle();
    }

    ///<summary>
    /// In order to have the physics respect the objects correctly we have to parent the
    /// fluid to the bottle when the fluid is inside the bottle
    /// </summary>>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Fluid>())
        {
            other.transform.parent = _tr;
        }
    }

    ///<summary>
    /// When the fluid leaves the parent we need to swap it to another element with a static position.
    /// </summary>>
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetComponent<Fluid>())
        {
            if (_fluidsParent)
            {
                other.transform.parent = _fluidsParent.transform;
            }
        }
    }

    void AssignBottleType()
    {
        name = bottleType.bottleName;
    }

    void UpdateColliders()
    {
        if (GetComponent<PolygonCollider2D>())
        {
            DestroyImmediate(GetComponent<PolygonCollider2D>());
            _ = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        }
        else
        {
            _ = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        }
    }

    void UpdateSpriteRenderer()
    {
        if (GetComponent<SpriteRenderer>())
            DestroyImmediate(GetComponent<SpriteRenderer>());

        _spriteRenderer = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        if (_spriteRenderer != null)
            _spriteRenderer.sprite = bottleType.sprite;
    }

    void FillBottle()
    {
        if (fluidPrefab != null)
        {
            for (int i = 0; i < fillCount; i++)
            {
                GameObject _ = Instantiate(fluidPrefab, _tr.position, Quaternion.identity, _tr);
                Fluid fluid = _.GetComponent<Fluid>();
                fluid.fluidType = fluidType;
            }
        }
    }
}
