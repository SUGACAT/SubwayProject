using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip _clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySE(string _name)
    {
        for(int i = 0; i <effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                if (!audioSourceEffects[i].isPlaying)
                {
                    audioSourceEffects[i].clip = effectSounds[i]._clip;
                    audioSourceEffects[i].Play();
                    return;
                }
            }
        }
    }
}
