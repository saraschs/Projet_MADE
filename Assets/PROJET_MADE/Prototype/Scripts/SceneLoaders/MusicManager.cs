using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 👉 Scènes de menu où la musique doit jouer
        if (scene.name == "HomeScreen" || scene.name == "HomeScreen2")
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // 👉 scènes de jeu = stop musique
            audioSource.Stop();
        }
    }
}