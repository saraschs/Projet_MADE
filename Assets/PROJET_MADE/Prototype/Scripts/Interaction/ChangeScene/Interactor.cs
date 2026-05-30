using UnityEngine;

public interface IInteractable
{
    void Interact();
    void ShowInteractionText(bool show);
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public float viewAngle = 25f;
    public float loseTargetDelay = 0.15f;

    private IInteractable currentInteractable;
    private float loseTimer;

    void Update()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);

        IInteractable detected = null;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                Vector3 dirToTarget = (hitInfo.collider.transform.position - InteractorSource.position).normalized;

                if (Vector3.Angle(InteractorSource.forward, dirToTarget) <= viewAngle)
                {
                    detected = interactable;
                }
            }
        }

        if (detected != null)
        {
            loseTimer = loseTargetDelay;

            if (currentInteractable != detected)
            {
                if (currentInteractable != null)
                    currentInteractable.ShowInteractionText(false);

                currentInteractable = detected;
                currentInteractable.ShowInteractionText(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact();
            }
        }
        else
        {
            loseTimer -= Time.deltaTime;

            if (loseTimer <= 0f && currentInteractable != null)
            {
                currentInteractable.ShowInteractionText(false);
                currentInteractable = null;
            }
        }
    }
}