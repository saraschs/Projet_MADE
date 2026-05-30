using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SphereInteraction : MonoBehaviour, IInteractable
{
    public GameObject interactionText;
    public string sceneName;

    private void Start()
    {
        interactionText.SetActive(false);
    }

    public void ShowInteractionText(bool show)
    {
        interactionText.SetActive(show);
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}