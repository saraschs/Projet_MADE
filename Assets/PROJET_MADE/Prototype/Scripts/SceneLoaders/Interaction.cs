using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Interaction");
    }
}