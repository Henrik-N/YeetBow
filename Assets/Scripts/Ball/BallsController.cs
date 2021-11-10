using UnityEngine;

public class BallsController : MonoBehaviour
{
    public GameObject mainActor;
    public float speed;
    
    Rigidbody2D mainActorRb;
    Vector2 input;

    void Awake()
    {
        mainActorRb = mainActor.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var xIn = Input.GetAxis("Horizontal");
        var yIn = Input.GetAxis("Vertical");
        input = new Vector2(xIn, yIn).normalized;
    }

    void FixedUpdate()
    {
        mainActorRb.AddForce(input * (speed * Time.fixedDeltaTime));
    }
}
