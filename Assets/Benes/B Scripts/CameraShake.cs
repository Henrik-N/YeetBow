using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform transform;

    private float shakeDuration = 0f;

    [SerializeField, Range(0.01f, 0.5f)] private float shakeMagnitude = 0.3f;

    [SerializeField, Range(1f, 5f)]private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    [Range(.1f,1f)] public float setShakeDuration = 1f;

    #region SingleTon
    private static CameraShake instance;

    public static CameraShake Instance
    {
        get => instance;
    }

    private void OnEnable()
    {
        if (instance == null)
            instance = this;

        initialPosition = transform.localPosition;
    }
    #endregion

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
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
      shakeDuration = setShakeDuration;
    }
}