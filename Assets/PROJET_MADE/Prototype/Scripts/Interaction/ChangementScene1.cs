using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene1 : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;

    public GameObject interactionCanvas; // Canvas affichant "Appuyez sur E"
    public string sceneName; // Nom de la scène à charger

    void Start()
    {
        if (interactionCanvas != null)
            interactionCanvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        bool isInRange = distance <= interactionDistance;

        // Affiche ou cache le canvas
        if (interactionCanvas != null)
            interactionCanvas.SetActive(isInRange);

        // Changement de scène avec E
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}