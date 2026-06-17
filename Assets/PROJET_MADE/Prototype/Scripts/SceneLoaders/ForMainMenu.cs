using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForMainMenu : MonoBehaviour
{
    public AudioSource keySound;
    public Image fadePanel;
    public float fadeDuration = 1f;

    private bool isLoading = false;

    void Update()
    {
        if (!isLoading && Input.anyKeyDown)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        isLoading = true;

        // Joue le son du "press any key"
        if (keySound != null)
            keySound.Play();

        // FADE OUT
        float t = 0f;

        Color c = fadePanel.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadePanel.color = c;

            yield return null;
        }

        // Optionnel : attendre la fin du son si besoin
        if (keySound != null)
        {
            yield return new WaitForSeconds(
                Mathf.Max(0, keySound.clip.length - fadeDuration)
            );
        }

        // Changement de scène
        SceneManager.LoadScene("HomeScreen2");
    }
}