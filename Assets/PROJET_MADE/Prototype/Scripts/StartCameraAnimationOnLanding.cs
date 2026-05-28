using UnityEngine;

public class StartCameraAnimationOnLanding : MonoBehaviour
{
    public Animator cameraAnimator;

    private bool hasLanded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.CompareTag("Ground"))
        {
            hasLanded = true;

            cameraAnimator.enabled = true;
            cameraAnimator.SetTrigger("StartFallAnimation");
        }
    }
}