using StarterAssets;
using System.Net;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerSteps;
    public AudioClip playerTakeHit;
    public AudioClip enemySteps;
    public AudioClip enemyTakeHit;
    public AudioClip deathRobot;
    public AudioClip energyGate;
    public AudioClip ammoTake;
    public AudioClip shieldTake;
    public AudioClip pistol;
    public AudioClip machineGunStart;
    public AudioClip machineGunEnd;
    public AudioClip sniperRifle;
    public AudioClip sniperRifleZoom;
    public AudioClip bulletDropped;
    public AudioClip turretHit;
    public AudioClip zoomOn;
    public AudioClip zoomOff;


    public void PlayerSteps(float volume)
    {
        PlayAudio(playerSteps, volume);
    }

    public void PlayerSprintSteps(float volume)
    {
        audioSource.pitch = 2f;
        PlayAudio(playerSteps, volume);
    }

    public void DeathRobot(float volume)
    {
        PlayAudio(deathRobot, volume);
    }

    public void AmmoTake(float volume)
    {
        PlayAudio(ammoTake, volume);
    }

    public void ZoomOn(float volume)
    {
        PlayAudio(zoomOn, volume);
    }

    public void ZoomOff(float volume)
    {
        PlayAudio(zoomOff, volume);
    }


    void PlayAudio(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }

}
