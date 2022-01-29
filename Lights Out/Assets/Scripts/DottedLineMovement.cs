using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineMovement : MonoBehaviour
{
    GameObject GameManagerObj;
    GameManager gameManager;
    private void Start()
    {
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime);
        if (gameManager.colorIsWhite)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LineDeleter"))
        {
            Destroy(gameObject);
        }
    }
}