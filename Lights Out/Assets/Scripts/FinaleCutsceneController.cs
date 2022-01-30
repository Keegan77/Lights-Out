using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("Trigger");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            littleFellaDeath = true;
        }
    }
}
