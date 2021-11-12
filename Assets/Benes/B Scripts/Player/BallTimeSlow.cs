using System.Collections;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BallTimeSlow : MonoBehaviour
{
    [Tooltip("Hur länge det ska va slowat"), SerializeField, Range(0.1f, 2f)]
    private float maxSlowTime = 0.5f;
    [SerializeField, Range(1, 10)] private float timeCooldown = 1f;
    
    [SerializeField, Range(1, 10)] private float timeYouCanSlow = 5f;
    private float timeYouCanSlowMax;
    [SerializeField, Range(0.1f, 1f)] private float minSlowmotionSpeed = 0.3f;


    public Image timebar;

    private bool isPointerDown;

    public bool IsPointerDown
    {
        get => isPointerDown;
        set => isPointerDown = value;
    }

    
    private WaitForSeconds delay; // för mjölkning/ko rutiner
    private float amountOfTimeSlowed = 0f;
    private bool timeIsSlowed = false;
     

    // Start is called before the first frame update
    private void Start()
    {
        //delay = new WaitForSeconds(timeCooldown);
        timeYouCanSlowMax = timeYouCanSlow;
        timebar.fillAmount = timeYouCanSlow / timeYouCanSlowMax;
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (isPointerDown && timeYouCanSlow > 0)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, minSlowmotionSpeed, Time.deltaTime * 4);
            timeYouCanSlow -= Time.deltaTime;
            Debug.Log("Time scale" + Time.timeScale);
            //Debug.Log("timeYouCanSlow" + timeYouCanSlow);
        }
        else if (!isPointerDown && timeYouCanSlow < timeYouCanSlowMax)
        {
            //StartCoroutine(ReturnToNormalTime());
            Time.timeScale = 1;
            timeYouCanSlow += Time.deltaTime;
            Debug.Log("Time scale" + Time.timeScale);
            Debug.Log("timeYouCanSlow" + timeYouCanSlow);
        }

        timebar.fillAmount = timeYouCanSlow / timeYouCanSlowMax;
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        
         if (Input.GetButtonDown("Jump")) //Todo add UI för detta för mobile
        {
            if (!timeIsSlowed) // && slowCharges > 0) 
            {
                StartCoroutine(SlowTime());
            }
        }
        else if (amountOfTimeSlowed > maxSlowTime && timeIsSlowed)
        {
            StartCoroutine(ReturnToNormalTime());
        }

        amountOfTimeSlowed += Time.deltaTime;
        
#endif
    }


    private IEnumerator ReturnToNormalTime()
    {
        Debug.Log("return to normaltime");
        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime;
            timeYouCanSlow += Time.deltaTime;
        }

        timeIsSlowed = false;
        yield break;
    }

    private IEnumerator SlowTime()
    {
        Debug.Log("slowtime");
        while (Time.timeScale > minSlowmotionSpeed)
        {
            Time.timeScale -= Time.deltaTime;
            timeYouCanSlow -= Time.deltaTime;
        }

        timeIsSlowed = true;
        amountOfTimeSlowed = 0;
        yield break; //return delay;
    }
}