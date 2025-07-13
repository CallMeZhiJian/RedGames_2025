using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainMenuBGM : MonoBehaviour
{
    public void PlayMusic()
    {
        AudioManager.Instance.BGM_Source.Play();
    }

    public void StopMusic()
    {
        AudioManager.Instance.BGM_Source.Stop();
    }
}
