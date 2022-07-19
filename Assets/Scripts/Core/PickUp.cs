using UnityEngine;

public class PickUp: MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1;
    [SerializeField]
    private GameObject heldObject;
    [SerializeField]
    private Transform holdParent;
    [SerializeField]
    private float moveForce = 250f;

    private Vector3 _mousePosition;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_camera != null) _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        holdParent.transform.position = _mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(_mousePosition, Vector2.zero);

                if (hit.collider == null) return;

                PickUpObject(hit.transform.gameObject);
                Debug.Log(hit.collider.gameObject.transform.position);
            }
            else
            {
                DropObject();
            }
        }
        
        if (heldObject != null)
        {
            MoveObject();
            
            // This controls the rotation of the selected object
            if (Input.GetKey(KeyCode.R))
            {
                RotateObject(heldObject, new Vector3(0,0, -45f));
            }
            if (Input.GetKey(KeyCode.Q))
            {
                RotateObject(heldObject, new Vector3(0,0, 45f));
            }
        }
    }
    
    private void MoveObject()
    {
        if (Vector2.Distance(heldObject.transform.position, holdParent.position) > 0.1f)
        {
            Vector2 moveDirection = holdParent.position - heldObject.transform.position;
            heldObject.GetComponent<Rigidbody2D>().AddForce(moveDirection * moveForce);
        }
    }

    private void RotateObject(GameObject objectToRotate, Vector3 direction)
    {
        Debug.Log("Attempting to Rotate");
        objectToRotate.GetComponent<Transform>().Rotate(direction * (rotationSpeed * Time.deltaTime));
    }

    void PickUpObject(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<Rigidbody2D>() && !pickedObject.CompareTag("Fluid"))
        {
            Rigidbody2D objectRigidBody = pickedObject.GetComponent<Rigidbody2D>();
            objectRigidBody.transform.parent = holdParent;
            heldObject = pickedObject;
        }
    }
    
    private void DropObject()
    {
        Rigidbody2D objectRigidBody = heldObject.GetComponent<Rigidbody2D>();
        objectRigidBody.constraints = RigidbodyConstraints2D.None;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}
