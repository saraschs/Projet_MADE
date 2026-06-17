using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;

    public GameObject interactionCanvas; // Canvas entier
    public string sceneName;

    private bool isInRange = false;

    void Start()
    {
        if (interactionCanvas != null)
            interactionCanvas.SetActive(false);
    }

    void Update()
    {
        if (!TriggerActivation.interactionUnlocked)
        {
            if (interactionCanvas != null)
                interactionCanvas.SetActive(false);

            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);
        isInRange = distance <= interactionDistance;

        if (interactionCanvas != null)
            interactionCanvas.SetActive(isInRange);

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}