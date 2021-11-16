using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BouncyZone : MonoBehaviour
{
    private BoxCollider2D box;
    [SerializeField, Tooltip("Hur hårt man blir slängd"), Range(1f, 30f)] private float speed = 10f;

    [SerializeField] private AudioClip clip;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        if (!box.isTrigger)
            box.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D bod = other.attachedRigidbody;
        if (bod)
            AccelerateBod(bod);
        
        if (clip != null)
            SoundManager.Instance.PlaySoundAtLocation(transform.position, clip);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("bod trigger", other.attachedRigidbody);
        Rigidbody2D bod = other.attachedRigidbody;
        if (bod)
            AccelerateBod(bod);
        
    }

    private void AccelerateBod(Rigidbody2D body)
    {
        Vector2 velocity = transform.InverseTransformDirection(body.velocity);

        velocity.y = speed;
        body.velocity = transform.TransformDirection(velocity);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.magenta;
        Handles.DrawLine(transform.position, transform.position + transform.up, 5f);
    }
}




