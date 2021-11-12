using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform transform;

    [SerializeField, Range(0.1f, 0.5f)] private float shakeDuration = 0f;

    [SerializeField, Range(0.1f, 0.5f)] private float shakeMagnitude = 0.3f;

    [SerializeField, Range(1f, 5f)]private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    
    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    
    public void TriggerShake() 
    {
      shakeDuration = 1.0f;
    }
}