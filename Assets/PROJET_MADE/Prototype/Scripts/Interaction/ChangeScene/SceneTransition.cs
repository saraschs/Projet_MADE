using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator cameraAnimator;
    public SimpleFade fade;

    public string sceneName;

    public float delayBeforeLoad = 1f;

    private bool isTransitioning = false;

    public void PlayTransition(string targetScene)
    {
        if (isTransitioning) return;

        sceneName = targetScene;
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        isTransitioning = true;

        // 1. Lance animation caméra
        cameraAnimator.SetTrigger("Transition");

        // 2. Petit délai avant fade (tu peux ajuster ou remplacer par event)
        yield return new WaitForSeconds(delayBeforeLoad);

        // 3. Fade écran noir
        fade.FadeIn();

        // 4. attendre que le fade soit visible
        yield return new WaitForSeconds(1f);

        // 5. Load scène
        SceneManager.LoadScene(sceneName);
    }
}