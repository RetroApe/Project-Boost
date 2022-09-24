using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float volume = 0.2f;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem deathExplosionParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;


    bool isTransitioning = false;
    bool isCheating = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatingCheck();
    }
    void CheatingCheck()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C) && !isCheating)
        {
            isCheating = true;
            print("Collision turned off!");
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCheating)
        {
            isCheating = false;
            print("Collision turned on!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Respawn":
                print("Start of the level");
                break;
            case "Finish":
                print("Player wins");
                StartWinSequence();
                break;
            //case "Fuel":
            //    print("fuel");
            //    break;
            default:
                print("Player dies");
                if (!isCheating)
                {
                    StartCrashSequence();
                }
                break;

        }
    }

    void StartCrashSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        deathExplosionParticles.Play();
        audioSource.PlayOneShot(deathExplosion, volume);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void StartWinSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        successParticles.Play();
        audioSource.PlayOneShot(success, volume);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", loadDelay);
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
   
}
