using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform transform;

    private float shakeboi = 0f;

    [SerializeField, Range(0.01f, 0.5f)] private float shakeMagnitude = 0.3f;

    [SerializeField, Range(1f, 5f)]private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    [Range(.1f,1f)] public float shakeDuration = 1f;

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    
    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void Update()
    {
        if (shakeboi > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeboi -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeboi = 0f;
            transform.localPosition = initialPosition;
        }
    }
    
    public void TriggerShake() 
    {
        shakeboi = shakeDuration;
    }
}