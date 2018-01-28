using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    private List<AudioSource> sources = new List<AudioSource>();

    private AudioSource musicSource;

    private AudioSource Source
    {
        get
        {
            foreach (var source in sources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }
            var src= gameObject.AddComponent<AudioSource>();
            sources.Add(src);
            return src;
        }
    }

    public void PlayAudio(AudioClip clip, float pitch = 1, float volume = 1)
    {
        var src = Source;
        src.volume = volume;
        src.pitch = pitch;
        src.clip = clip;
        src.Play();
    }

    public void PlayMusic(AudioClip clip, float volume = 1)
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
        musicSource = Source;
        musicSource.volume = 1;
        musicSource.pitch = 1;
        musicSource.loop = true;
        musicSource.clip = clip;
        musicSource.Play();
    }
}
