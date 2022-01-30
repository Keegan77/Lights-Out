using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhiteLines : MonoBehaviour
{
    public GameObject dottedLine;
    float startTimer = .5f;
    float timer;
    public bool justSwitched;
    public float numOfLines;
    GameObject GameManagerObj;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfLines; i++)
        {
            Instantiate(dottedLine, new Vector3(transform.position.x - .49f * i, transform.position.y, transform.position.z), transform.rotation);
        } // putting this here so you get continuous line movement even when you first start the level
        timer = startTimer;
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (justSwitched)
        {
            for (int i = 0; i < numOfLines; i++)
            {
                Instantiate(dottedLine, new Vector3(transform.position.x - .49f * i, transform.position.y, transform.position.z), transform.rotation);
            }
            justSwitched = false;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (gameManager.colorIsWhite)
            {
                Instantiate(dottedLine, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                timer = startTimer;
            }

        }
    }
}
