using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] Sources;
    public AudioSource BGMSource;

    private void Awake()
    {
        Sources = GetComponents<AudioSource>();
    }

    public void PlayClipWithSource(AudioClip _clip, AudioSource _source)
    {
        _source.clip = _clip;
        _source.Play();
    }

    public void PlayClipAsBGM(AudioClip _clip)
    {
        BGMSource.clip = _clip;
        BGMSource.Play();
    }

    public void PlayClip(AudioClip _clip)
    {
        for(int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
            }
        }
    }
}
