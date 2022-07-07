using UnityEngine;
public class PickUp: MonoBehaviour
{
    [SerializeField]
    private GameObject heldObject;
    [SerializeField]
    private Transform holdParent;
    [SerializeField]
    private float moveForce = 250f;

    private Vector3 _mousePosition;
    
    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateObject(heldObject, -1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject(heldObject, 1);
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

    private void RotateObject(GameObject objectToRotate, float direction)
    {
        Debug.Log("Attempting to Rotate");
        objectToRotate.GetComponent<Rigidbody2D>().AddTorque(direction, ForceMode2D.Force);
    }

    void PickUpObject(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<Rigidbody2D>() && pickedObject.GetComponent<Item>())
        {
            Rigidbody2D objectRigidBody = pickedObject.GetComponent<Rigidbody2D>();
//            objectRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
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
