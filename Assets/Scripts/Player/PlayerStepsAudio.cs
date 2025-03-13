using StarterAssets;
using UnityEngine;
using System.Collections;

public class PlayerStepsAudio : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    public AudioSource steps;

    public bool isMoving;
    public bool isSprinting;
    public const float sprintThresholdMultiplier = 0.9f;
    public Coroutine stepCoroutine;
    public PlayerHealth playerHealth;

    private void Update()
    {
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        isMoving = firstPersonController.MagnitudeMoveSpeed > 0.1f;
        isSprinting = firstPersonController.MagnitudeMoveSpeed > firstPersonController.SprintSpeed * sprintThresholdMultiplier;

        if (isMoving && firstPersonController.Grounded && playerHealth.isALive)
        {
            if (stepCoroutine == null)
                stepCoroutine = StartCoroutine(PlaySteps());
        }
        else
        {
            StopSteps();
        }
    }

    private IEnumerator PlaySteps()
    {
        while (isMoving && firstPersonController.Grounded && playerHealth.isALive)
        {
            steps.pitch = isSprinting ? 1.5f : 1f;
            steps.Play();

            float stepDelay = isSprinting ? 0.25f : 0.4f;
            yield return new WaitForSeconds(stepDelay);
        }

        stepCoroutine = null;
    }

    public void StopSteps()
    {
        if (stepCoroutine != null)
        {
            StopCoroutine(stepCoroutine);
            stepCoroutine = null;
        }

        steps.Stop();
    }
}
