using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereInteraction : MonoBehaviour, IInteractable
{
    public GameObject interactionCanvas;
    public string sceneName;

    public SceneTransition transition;

    private void Start()
    {
        if (interactionCanvas != null)
            interactionCanvas.SetActive(false);
    }

    public void ShowInteractionText(bool show)
    {
        if (interactionCanvas != null)
            interactionCanvas.SetActive(show);
    }

    public void Interact()
    {
        if (transition != null)
        {
            transition.PlayTransition(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}