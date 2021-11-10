using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FloatyZone : MonoBehaviour
{
    private BoxCollider2D box;

    [SerializeField,Tooltip("Negativ speed åker man neråt"), Range(-30f, 30f)] private float Speed = 10f;
    [SerializeField,Tooltip("Hur snabbt man åker, minst 10 cuz gravity"), Range(10f, 20f)] private float acceleration = 10f;

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

    void Acceleration(Rigidbody2D body)
    {
        Vector2 velocity = body.velocity;
        if (acceleration > 0f)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, Speed, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.y = Speed;
        }

        body.velocity = velocity;
    }
}
