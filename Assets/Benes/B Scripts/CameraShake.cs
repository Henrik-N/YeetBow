using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0f;
    [SerializeField, Range(0.1f, .3f)] private float shakeMagnitude = 0.1f;

    [SerializeField, Range(0f, 5f)] private float dampingSpeed = 1f;
    [SerializeField, Range(0.1f, 1f)] private float shakeTime = 0.5f;
    private Vector3 initialPosition;


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
        shakeDuration = shakeTime;
    }
}