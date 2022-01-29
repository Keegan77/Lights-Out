using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlatformCollOff : MonoBehaviour
{
    BoxCollider2D BoxCol;
    GameObject GameManagerObj;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        BoxCol = GetComponent<BoxCollider2D>();
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.colorIsWhite)
        {
            BoxCol.enabled = false;
        }
        else BoxCol.enabled = true;
    }
}
