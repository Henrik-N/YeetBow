using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    
    [SerializeField, Range(1, 10)] private float force = 10f;

    [SerializeField, Tooltip("Hur långt man kan dra i bollen"), Range(1, 5)]
    private float maxForceDragDistance = 3f;
    
    //fuck this scuffed shit
    [SerializeField, Range(0f, 100f)] private float speed = 10f;
    
    private Rigidbody2D body;
    private Camera cam;
    
    private Vector2 dragDir;
    private Vector2 startPoint;
    
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        startPoint = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            body.velocity += Vector2.left * speed;
        if (Input.GetKeyDown(KeyCode.D))
            body.velocity -= Vector2.left * speed;
    }
    
    private void OnMouseOver()
    {
        //UI grejer?? typ highlight etc
    }

    private void OnMouseDrag()
    {
        startPoint = transform.position;
        dragDir = startPoint - (Vector2) cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        //add fårce
        //and reset startpoint i guess
        if (dragDir.magnitude > maxForceDragDistance)
        {
            Mathf.Clamp(dragDir.x, dragDir.x, maxForceDragDistance);
            Mathf.Clamp(dragDir.y, dragDir.y, maxForceDragDistance);
        }

        body.AddForce(dragDir * force, ForceMode2D.Impulse);
        startPoint = Vector2.zero;
    }
    
    private void OnDrawGizmos()
    {
        if (cam != null && startPoint != Vector2.zero)
        {
            Vector2 dir = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition) - startPoint;

            Debug.DrawRay(startPoint, Vector2.ClampMagnitude(dir, maxForceDragDistance), Color.black);
            Debug.DrawRay(startPoint, Vector2.ClampMagnitude(-dir, maxForceDragDistance), Color.green);
        }
    }
}
