using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource musicSource;

    public AudioSource EffectSource => effectSource;
    public AudioSource MusicSource => musicSource;

    [SerializeField, Range(-3f, 3f)] private float pitch;
    [SerializeField, Range(0, 1f)] private float volume;

    public GameObject prefab;


    public float Pitch
    {
        get => pitch;
        set => pitch = value;
    }

    public float Volume
    {
        get => volume;
        set => volume = value;
    }

    #region SingleTon

    private static SoundManager instance;

    public static SoundManager Instance
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


    #region AudioClipMethods

    /// <summary>
    /// Plays an audio clip with pitch 1
    /// </summary>
    /// <param name="clip"></param>
    public void PlayClip(AudioClip clip)
    {
        PlayClip(clip, 1, false);
    }

    /// <summary>
    /// Plays an audio clip with controlled pitch
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pitchu"></param>
    public void PlayClip(AudioClip clip, float pitch)
    {
        PlayClip(clip, pitch, false);
    }

    /// <summary>
    /// Play an audio clip with controlled pitch and looping
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pitchu"></param>
    /// <param name="loop"></param>
    public void PlayClip(AudioClip clip, float pitch, bool loop)
    {
        PlayClip(clip, pitch, volume, false);
    }

    /// <summary>
    /// Plays an audio clip with full control
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pitchu"></param>
    /// <param name="volumet"></param>
    /// <param name="loop"></param>
    public void PlayClip(AudioClip clip, float pitch, float volume, bool loop)
    {
        effectSource.clip = clip;
        effectSource.pitch = pitch;
        effectSource.loop = loop;
        effectSource.volume = volume;
        effectSource.Play();
    }

    #endregion

    /// <summary>
    /// Plays music clip
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip) //Todo add loop?
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    #region Un-Mute-Mute

    public void MuteAllAudio()
    {
        effectSource.gameObject.SetActive(false);
        musicSource.gameObject.SetActive(false);
    }

    public void UnMuteAllAudio()
    {
        effectSource.gameObject.SetActive(true);
        musicSource.gameObject.SetActive(true);
    }

    public void MuteMusic()
    {
        musicSource.gameObject.SetActive(false);
    }

    public void UnMuteMusic()
    {
        musicSource.gameObject.SetActive(true);
    }

    #endregion

    public void PlaySoundAtLocation(Vector3 location, AudioClip clip)
    {
        StartCoroutine(PlayAtLocation(location, clip));
    }

    /// <summary>
    /// Instantiates a gameobject at location and play a sound there
    /// </summary>
    /// <param name="location"></param>
    /// <param name="clip"></param>
    IEnumerator PlayAtLocation(Vector3 location, AudioClip clip) //todo fix pool for this
    {
        GameObject obj = Instantiate(prefab, location, Quaternion.identity);
        //obj = Instantiate(obj, location, Quaternion.identity);
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        Destroy(obj, clip.length);
        yield return new WaitForSeconds(clip.length + .5f);
    }

    private void OnDisable()
    {
        instance = null;
    }
}