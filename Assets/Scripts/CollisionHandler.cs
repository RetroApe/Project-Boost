using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float volume = 0.2f;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip success;
    

    void OnCollisionEnter(Collision collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                print("Player dies");
                StartCrashSequence();
                break;
            case "Finish":
                print("Player wins");
                StartWinSequence();
                break;
            case "Fuel":
                print("fuel");
                break;

        }
    }

    void StartCrashSequence()
    {
        GetComponent<AudioSource>().PlayOneShot(deathExplosion, volume);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void StartWinSequence()
    {
        GetComponent<AudioSource>().PlayOneShot(success, volume);
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
