using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAnimMid : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite darkGround;
    public Sprite lightGround;
    GameObject GameManagerObj;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.colorIsWhite)
        {
            spriteRenderer.sprite = lightGround;
        }
        else spriteRenderer.sprite = darkGround;
    }
}
