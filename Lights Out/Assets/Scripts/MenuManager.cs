using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator menuAnimator;
    public Animator textAnimator;
    public Animator enterToStart;
    public Text pressS;
    float animTime = 2.0f;
    float timer;
    bool animEnded;
    bool lightPressed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            lightPressed = true;
            timer = animTime;
        }
        if (lightPressed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                animEnded = true;
            }
            if (animEnded)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        menuAnimator.SetBool("animEnded", animEnded);
        textAnimator.SetBool("animEnd", animEnded);
        textAnimator.SetBool("lightPress", lightPressed);
        menuAnimator.SetBool("lightPressed", lightPressed);
        enterToStart.SetBool("animEnd", animEnded);
    }
}
