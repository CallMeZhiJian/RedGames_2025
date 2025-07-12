using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource BGM_Source;
    public AudioSource SFX_Source;

    public AudioClip BGM_Clip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(BGM_Source == null) BGM_Source = GetComponentInChildren<AudioSource>();
        if(SFX_Source == null) SFX_Source = GetComponentInChildren<AudioSource>();

        if(BGM_Clip == null && BGM_Source != null) BGM_Source.clip = BGM_Clip;
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        SFX_Source.clip = clip;
        SFX_Source.volume = volume;

        if (SFX_Source.isPlaying) SFX_Source.Stop();

        SFX_Source.Play();
    }
}
