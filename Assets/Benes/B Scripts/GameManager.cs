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


    public GameObject player;
    public AudioClip music;
    
    private void Awake()
    {
        if (music != null)
            SoundManager.Instance.PlayMusic(music);
    }
}
