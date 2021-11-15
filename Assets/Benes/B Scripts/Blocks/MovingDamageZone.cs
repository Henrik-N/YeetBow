using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RemoteNotification = UnityEngine.iOS.RemoteNotification;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingDamageZone : MonoBehaviour
{
    private BoxCollider2D box;

    [SerializeField, Tooltip("Dmg lmao"), Range(1f, 30f)]
    private int damage = 10;

    [SerializeField] private AudioClip clip;

    [SerializeField] private float MoveLength = 10f;
    [SerializeField] private float moveSpeed = 10f;

    public enum MoveDir
    {
        Up,
        Down,
        Left,
        Right
    }
    public MoveDir dir = default;

    private Vector2 startPos;
    private Vector2 vel = default;
    private Vector2 endPos;
    
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        if (!box.isTrigger)
            box.isTrigger = true;
        
    }

    

    private void OnValidate()
    {
        startPos = transform.position;
        vel = WhichDir();
        endPos = startPos + vel * MoveLength; 
        Debug.Log("vel is " + vel);
    }

    private void Update()
    {
        if (MovedToFar()) //Todo fixa så det här funkar
        {
            vel *= -1;
        }
        transform.position += (Vector3) vel * moveSpeed * Time.deltaTime;
        Debug.Log("vel is " + vel);

    }

    private bool MovedToFar()
    {
        if (transform.position.magnitude > endPos.magnitude)
        {
            return true;
        }

        return false;
    }

    private Vector2 WhichDir()
    {
        switch (dir)
        {
            case MoveDir.Down:
                vel = Vector2.down;
                return vel;
            case MoveDir.Up:
                vel = Vector2.up;
                return vel;
            case MoveDir.Left:
                vel = Vector2.left;
                return vel;
            case MoveDir.Right:
                vel = Vector2.right;
                return vel;
        }

        return vel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BallHealthComp>().TakeDamage(damage);
            other.gameObject.GetComponentInChildren<CameraShake>().TriggerShake();
            SoundManager.Instance.PlaySoundAtLocation(transform.position, clip);
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(startPos, startPos + (vel * MoveLength), 5f);
        Handles.DrawLine(startPos, startPos - (vel * MoveLength), 5f);
        Handles.color = Color.red;
        Handles.DrawSolidDisc(startPos + vel * MoveLength, Vector3.forward, .1f);
        Handles.DrawSolidDisc(startPos - vel * MoveLength, Vector3.forward, .1f);
    }
}