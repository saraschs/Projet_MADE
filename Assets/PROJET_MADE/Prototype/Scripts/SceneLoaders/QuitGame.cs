using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Fermeture du jeu");
        Application.Quit();
    }
}