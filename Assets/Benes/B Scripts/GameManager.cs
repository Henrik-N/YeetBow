using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion


    public AudioClip Music;

    [Range(0.1f, 1f)] public float screenShakeTime;
    
    
    
    private void Awake()
    {
        SoundManager.Instance.PlayMusic(Music);
        CameraShake.Instance.setShakeDuration = screenShakeTime;
    }
}
