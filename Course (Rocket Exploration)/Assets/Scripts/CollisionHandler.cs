using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    Movement movement;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionOff = false;

    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCollisionOff)
        {
            return;
        }
        else
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
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(success);

        successParticles.Play();

        movement.enabled = false;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crash);

        crashParticles.Play(); 

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

    void RespondToDebugKeys()
    {
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionOff = !isCollisionOff;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Invoke("LoadNextLevel", 0.5f);
        }*/
    }
}
