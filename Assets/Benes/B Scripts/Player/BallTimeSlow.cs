using System.Collections;
using UnityEngine;

public class BallTimeSlow : MonoBehaviour
{
    [Tooltip("Hur länge det ska va slowat"), SerializeField, Range(0.1f, 2f)]
    private float maxSlowTime = 0.5f;

    [SerializeField, Range(1, 10)] private float timeCooldown = 1f;

    //[SerializeField, Tooltip("Gör inget just nu lmao")]
    //private int slowCharges = 3;

    private WaitForSeconds delay; // för mjölkning/ko rutiner
    private float amountOfTimeSlowed = 0f;

    private bool timeIsSlowed = false;
    //Todo en cooldown för charges om vi ska ha det
    // Start is called before the first frame update
    private void Start()
    {
        delay = new WaitForSeconds(timeCooldown);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!timeIsSlowed) // && slowCharges > 0)
            {
                Debug.Log("slowtime");
                StartCoroutine(SlowTime());
            }
        }
        else if (amountOfTimeSlowed > maxSlowTime && timeIsSlowed)
        {
            Debug.Log("normaltime"); //Todo tweak this shit
            StartCoroutine(ReturnToNormalTime());
        }

        amountOfTimeSlowed += Time.deltaTime;
    }
    
    
    private IEnumerator ReturnToNormalTime()
    {
        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime;
        }

        timeIsSlowed = false;
        yield break;
        //Todo Kan lägga till cd emellan här 
    }
    private IEnumerator SlowTime()
    {
        while (Time.timeScale > 0.5f)
        {
            Time.timeScale -= Time.deltaTime;
        }

        //slowCharges--;
        timeIsSlowed = true;
        amountOfTimeSlowed = 0;
        yield break; //return delay;
    }
}
