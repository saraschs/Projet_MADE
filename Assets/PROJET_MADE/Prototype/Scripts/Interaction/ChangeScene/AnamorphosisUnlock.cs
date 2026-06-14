using UnityEngine;

public class AnamorphosisUnlock : MonoBehaviour
{
    public SphereInteractionAnamorp entrance;
    public AudioSource unlockSound;

    private bool unlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag("Player"))
        {
            unlocked = true;

            entrance.UnlockPassage();

            if (unlockSound != null)
            {
                unlockSound.Play();
            }
        }
    }
}