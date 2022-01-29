using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLines : MonoBehaviour
{
    public GameObject dottedLine;
    float startTimer = .5f;
    float timer;
    public bool justSwitched;
    GameObject GameManagerObj;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        timer = startTimer;
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (justSwitched)
        {
            for (int i = 0; i < 6; i++)
            {
                Instantiate(dottedLine, new Vector3(transform.position.x -.49f * i, transform.position.y, transform.position.z), transform.rotation);
            }
            justSwitched = false;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (!gameManager.colorIsWhite)
            {
                Instantiate(dottedLine, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                timer = startTimer;
            }

        }
    }
}
