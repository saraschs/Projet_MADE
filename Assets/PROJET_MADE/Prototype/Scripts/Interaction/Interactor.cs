using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    void ShowInteractionText(bool show);
}


public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactObj))
            {
                // Si on regarde un nouvel objet
                if (currentInteractable != interactObj)
                {
                    if (currentInteractable != null)
                        currentInteractable.ShowInteractionText(false);

                    currentInteractable = interactObj;
                    currentInteractable.ShowInteractionText(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }

                return;
            }
        }

        // Rien regardé
        if (currentInteractable != null)
        {
            currentInteractable.ShowInteractionText(false);
            currentInteractable = null;
        }
    }
}
   

