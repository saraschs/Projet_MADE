using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFall : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("StartFall_Scene");
    }
}