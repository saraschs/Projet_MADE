using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuTransition : MonoBehaviour
{
    public AudioSource clickSound;
    public Image fadePanel;

    public float fadeDuration = 1f;

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        // Joue le son
        clickSound.Play();

        // Fade vers noir
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadePanel.color = c;

            yield return null;
        }

        // Attend que le son soit terminé
        yield return new WaitForSeconds(
            Mathf.Max(0, clickSound.clip.length - fadeDuration)
        );

        // Charge la scène
        SceneManager.LoadScene("StartFall_Scene");
    }
}