using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour


{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audiosource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    public int delay = 1;
    



    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        DebugKeys();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("we are at the start");
                break;
            case "Finish":
                startSuccessSequence();

                break;
            default:
                startCrashSequence();
                break;
        }

    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }

    }

    void loadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
        

    }
    void LoadStart()
    {
        SceneManager.LoadScene("Flyover");
    }
    void reloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        
    }
    private void startSuccessSequence()

    {
        isTransitioning = true;
        //add finish sound
        audiosource.PlayOneShot(success);
        //add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", delay);
        successParticle.Play();

    }

    private void startCrashSequence()
    {
        isTransitioning = true;
        //add crash sound
        audiosource.PlayOneShot(crash);

        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", delay);
        crashParticle.Play();

    }

}
