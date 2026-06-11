using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Interaction");
    }
}