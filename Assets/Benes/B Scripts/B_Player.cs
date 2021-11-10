using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class B_Player : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float force = 10f;

    [SerializeField, Tooltip("Hur långt man kan dra i bollen"), Range(1, 5)]
    private float maxForceDragDistance = 3f;

    /// <summary>
    /// För dragningsgrejen
    /// </summary>
    private Vector2 dragDir;

    private Vector2 startPoint;

    #region Cached stuff

    private Camera cam;
    private Rigidbody2D body;
    private CircleCollider2D col;

    #endregion

    #region I HAVE MASTERED TIME FEAR ME

    [Tooltip("Hur länge det ska va slowat"), SerializeField, Range(0.1f, 2f)]
    private float maxSlowTime = 0.5f;

    [SerializeField, Range(1, 10)] private float TimeCooldown = 1f;
    [SerializeField] private int slowCharges = 3;
    private WaitForSeconds delay; // för mjölkning/ko rutiner
    private float AmountOfTimeSlowed = 0f;

    private bool timeIsSlowed = false;
    //Todo en cooldown för charges om vi ska ha det

    #endregion
   

    //fuck this scuffed shit
    [SerializeField, Range(0f, 100f)] private float Speed = 10f;

    void Start()
    {
        startPoint = Vector2.zero;
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        delay = new WaitForSeconds(TimeCooldown);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!timeIsSlowed) // && slowCharges > 0)
            {
                Debug.Log("slowtime");
                StartCoroutine(SlowTime());
            }
        }
        else if (AmountOfTimeSlowed > maxSlowTime && timeIsSlowed)
        {
            Debug.Log("normaltime"); //Todo tweak this shit
            StartCoroutine(ReturnToNormalTime());
        }

        AmountOfTimeSlowed += Time.deltaTime;

        #region Shit fast implementation

        if (Input.GetKeyDown(KeyCode.A))
            body.velocity += Vector2.left * Speed;
        if (Input.GetKeyDown(KeyCode.D))
            body.velocity -= Vector2.left * Speed;

        #endregion
    }

    IEnumerator ReturnToNormalTime()
    {
        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime;
        }

        timeIsSlowed = false;
        yield break;
        //Todo Kan lägga till cd emellan här 
    }

    IEnumerator SlowTime()
    {
        while (Time.timeScale > 0.5f)
        {
            Time.timeScale -= Time.deltaTime;
        }

        slowCharges--;
        timeIsSlowed = true;
        AmountOfTimeSlowed = 0;
        yield break; //return delay;
    }

    private void OnMouseOver()
    {
        //UI grejer??
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