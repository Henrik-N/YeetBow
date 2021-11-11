using System;
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

    private void Start()
    {
        SoundManager.Instance.PlayMusic(Music);
    }
}