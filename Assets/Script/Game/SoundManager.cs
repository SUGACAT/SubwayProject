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
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i]._clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
            }
        }
    }

    public void PlayBGM(string bgmName)
    {
        switch (bgmName)
        {
            case "Wind" :
                audioSourceBgm.clip = bgmSounds[0]._clip;
                audioSourceBgm.Play();
                break;
            case "Chase" :
                audioSourceBgm.clip = bgmSounds[1]._clip;
                audioSourceBgm.Play();
                break;
            case "Machine" :
                audioSourceBgm.clip = bgmSounds[2]._clip;
                audioSourceBgm.Play();
                break;
        }
    }

    public void StopBGM()
    {
        audioSourceBgm.Stop();
    }
    
    public void StopSE(string _name)
    {
        for(int i = 0; i <effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                Debug.Log("CompareName");

                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (audioSourceEffects[j].clip != null)
                    {
                        if (audioSourceEffects[j].clip.name == _name)
                        {
                            audioSourceEffects[j].Stop();
                            return;
                        }
                    }
                }
            }
        }
    }
}
