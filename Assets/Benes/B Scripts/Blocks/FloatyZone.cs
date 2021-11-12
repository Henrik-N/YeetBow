using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FloatyZone : MonoBehaviour
{
    private BoxCollider2D box;

    [SerializeField, Tooltip("Negativ speed åker man neråt"), Range(-30f, 30f)]
    private float speed = 10f;

    [SerializeField, Tooltip("Hur snabbt man åker, minst 10 cuz gravity"), Range(10f, 100f)]
    private float acceleration = 10f;

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
            Acceleration(bod);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("bod trigger", other.attachedRigidbody);
        Rigidbody2D bod = other.attachedRigidbody;
        if (bod)
            Acceleration(bod);
    }

    private void Acceleration(Rigidbody2D body)
    {
        Vector2 velocity = body.velocity;

        velocity.y = Mathf.MoveTowards(velocity.y, speed, acceleration * Time.deltaTime);

        body.velocity = velocity;
    }


    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, (transform.position + transform.up * Mathf.Clamp(speed, -2, 2)), 5f);
    }
}