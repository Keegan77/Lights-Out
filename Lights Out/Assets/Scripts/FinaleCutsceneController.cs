using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinaleCutsceneController : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public BoxCollider2D stop;
    bool littleFellaDeath;
    bool stopIdle;
    float idleTimerEnd = 2.0f;
    float idleTimer;
    float stopBarrier = 5.167f;
    float stopBarrierTimer;
    float timerEnd = 5.917f;
    float timer;

    private void Start()
    {
        TimePhaseManager.Instance.PauseTimephase(true);
        
        Destroy(FindObjectOfType<DontDestroyMusic>()?.gameObject);
        
        // Gets rid of music for finale scene. In the future,
        // "DontDestroyMusic" will be changed to a class called
        // "MusicManager" which will have a method called "StopMusic"
        // or something like that. This class will be a singleton,
        // and can be accessed from it's static Instance variable.
        
        Destroy(FindObjectOfType<LightFellaBehaviour>()?.gameObject); //Destroys the light fella so he doesn't interfere with the cutscene                                               

        
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTimerEnd)
        {
            stopIdle = true;
        }
        stopBarrierTimer += Time.deltaTime;
        if (stopBarrierTimer >= stopBarrier)
        {
            stop.enabled = false;
        }
        if (littleFellaDeath)
        {
            timer += Time.deltaTime;
            if (timer >= timerEnd)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
        
        cutsceneAnimator.SetBool("littleFellaDeath", littleFellaDeath);
        cutsceneAnimator.SetBool("stopIdle", stopIdle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            littleFellaDeath = true;
        }
    }
}
