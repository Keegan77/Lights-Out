using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashActOneScript : MonoBehaviour
{
    float startTime = 2;
    float timer;
    void Start()
    {
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
