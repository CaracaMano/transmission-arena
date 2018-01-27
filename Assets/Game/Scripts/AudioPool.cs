using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    private List<AudioSource> sources;

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
            return gameObject.AddComponent<AudioSource>();
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
}
