using UnityEngine;

public class WallEventTrigger : MonoBehaviour
{
    public WallMove[] wallsToActivate;
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            foreach (WallMove wall in wallsToActivate)
            {
                wall.StartMoving();
            }
        }
    }
}