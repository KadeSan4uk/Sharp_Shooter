using StarterAssets;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource stepsSource;
    public List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> audioDictionary;

    FirstPersonController firstPersonController;
    StarterAssetsInputs starterAssetsInputs;

    bool isRunning;

    private void Awake()
    {
        audioDictionary = new Dictionary<string, AudioClip>();

        foreach (var clip in audioClips)
        {
            audioDictionary[clip.name] = clip;
        }
    }
    private void Start()
    {
        firstPersonController = FindFirstObjectByType<FirstPersonController>();
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
    }

    private void Update()
    {
        isRunning = starterAssetsInputs.sprint;

        if (firstPersonController.isMove && firstPersonController.Grounded)
        {
            PlaySteps("steps");
        }
        else if (starterAssetsInputs.sprint && firstPersonController.isMove && firstPersonController.Grounded)
        {
            PlaySteps("steps");
        }
        else
        {
            stepsSource.Stop();
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

    public void PlaySteps(string soundName)
    {

        if (audioDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            if (!stepsSource.isPlaying)
            {
                stepsSource.clip = clip;
                stepsSource.loop = true;
                stepsSource.pitch = isRunning ? 2f : 1f;
                stepsSource.Play();
            }
        }        
    }
}
