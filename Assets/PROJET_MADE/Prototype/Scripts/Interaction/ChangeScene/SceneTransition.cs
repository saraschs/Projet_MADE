using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator cameraAnimator;
    public float transitionDuration = 2f;

    private bool isTransitioning = false;

    public void PlayTransition(string sceneName)
    {
        if (isTransitioning) return;

        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string sceneName)
{
    isTransitioning = true;

    Debug.Log("Trigger animation");

    cameraAnimator.SetTrigger("Transition");

    yield return new WaitForSeconds(transitionDuration);

    Debug.Log("Loading scene");

    SceneManager.LoadScene(sceneName);
}
}