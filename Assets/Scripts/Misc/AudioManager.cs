using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;    
    public List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> audioDictionary;

    private void Awake()
    {
        audioDictionary = new Dictionary<string, AudioClip>();

        foreach (var clip in audioClips)
        {
            audioDictionary[clip.name] = clip;
        }
    }

    public void PlaySound(string soundName, float volume = 1f, float pith = 1f)
    {
        if (audioDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.pitch = pith;
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogError("AudioClip not found!");
        }
    }

}
