using UnityEngine;

public class HeartBeats : MonoBehaviour
{
    public AudioSource heartBeats;
    public AudioClip AudioClip;

    public PlayerHealth playerHealth;

    public void PlaySound()
    {
        heartBeats.Play();
        heartBeats.loop = true;
    }

    public void StopPlay()
    {
        heartBeats.Stop();
    }
}
