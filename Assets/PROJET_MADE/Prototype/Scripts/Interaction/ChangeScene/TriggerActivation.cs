using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public AudioSource audioSource;
    public static bool interactionUnlocked = false;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            alreadyTriggered = true;

            interactionUnlocked = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
