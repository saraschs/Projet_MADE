using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereInteractionAnamorp : MonoBehaviour, IInteractable
{
    public GameObject interactionText;
    public string sceneName;

    private bool isUnlocked = false;

    private void Start()
    {
        interactionText.SetActive(false);
    }

    public void UnlockPassage()
    {
        isUnlocked = true;
    }

    public void ShowInteractionText(bool show)
    {
        interactionText.SetActive(show && isUnlocked);
    }

    public void Interact()
    {
        if (!isUnlocked)
            return;

        SceneManager.LoadScene(sceneName);
    }
}