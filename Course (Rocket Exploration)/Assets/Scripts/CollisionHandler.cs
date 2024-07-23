using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement movement;
    [SerializeField] float levelLoadDelay = 2f;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();

                break;

            case "Friendly":
                Debug.Log("Friendly!");

                break;

            default:
                StartCrashSequence();

                break;
        }
    }

    void StartSuccessSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash

        movement.enabled = false;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash

        movement.enabled = false;

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
