using UnityEngine;
using UnityEngine.SceneManagement;

public class MortMur : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }
}