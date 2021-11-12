using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AccelerationZone : MonoBehaviour
{
   
    private BoxCollider2D box;
    [SerializeField, Range(-30f, 30f)] private float speed = 10f;


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
        Vector2 velocity = body.velocity;

        velocity.x = speed;
        body.velocity = velocity;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, (transform.position + transform.right * Mathf.Clamp(speed, -2, 2)), 5f);
    }
}