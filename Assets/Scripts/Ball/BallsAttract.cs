using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BallsAttract : MonoBehaviour 
{
    public GameObject one;
    public GameObject two;
    public float forceMultiplier;

    public GameObject[] drops;

    
    Rigidbody2D rb1;
    Rigidbody2D rb2;
    float colliderRadius; // using same for both
    void Awake()
    {
        rb1 = one.GetComponent<Rigidbody2D>();
        rb2 = two.GetComponent<Rigidbody2D>();

        colliderRadius = one.GetComponent<CircleCollider2D>().radius;
        if (colliderRadius != two.GetComponent<CircleCollider2D>().radius)
        {
            Debug.LogError("Use identical collider radius for one and two when using this script.");
        }
    }
    
    void FixedUpdate() {
        var onePos = one.transform.position;
        var twoPos = two.transform.position;
        
        var dist = Vector2.Distance(onePos, twoPos);
        var directionTwoToOne = (Vector2) (onePos - twoPos).normalized;

        var colliderRadius = 0.1f;
        var distToTarget = dist - colliderRadius; // - (colliderRadius * 2f);
       
        
        var oneVelocityReqToReachTarget = (-directionTwoToOne * distToTarget) / Time.fixedDeltaTime;
        var twoVelocityReqToReachTarget = (directionTwoToOne * distToTarget) / Time.fixedDeltaTime;

        
        // with direct
        // rb1.velocity += oneVelocityReqToReachTarget;
        // rb2.velocity += twoVelocityReqToReachTarget;

        // with AddForce and taking mass into account
        // Force = m * a
        // a = (targetVel - initialVel) / t
        var force1 = rb1.mass * (oneVelocityReqToReachTarget) / Time.fixedDeltaTime;
        var force2 = rb2.mass * (twoVelocityReqToReachTarget) / Time.fixedDeltaTime;
       
        rb1.AddForce(force1);
        rb2.AddForce(force2);
    }
}

