using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timeStart = 5;
    private float timer = 5;
    public bool colorIsWhite = true;
    public bool timerSwitch; // 
    public Color colorWhite = Color.white;
    public Color colorBlack = Color.black;
    GameObject Camera;
    Camera Background;
    Animator playerAnimator;
    GameObject Player;
    [SerializeField] SpawnLines[] LineSpawner;
    [SerializeField] SpawnWhiteLines[] WhiteLineSpawner;
    //GameObject[] dottedLineSpawners;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Background = Camera.GetComponent<Camera>();
        LineSpawner = FindObjectsOfType<SpawnLines>();
        WhiteLineSpawner = FindObjectsOfType<SpawnWhiteLines>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        //Debug.Log(timer);
        if (timer < 0)
        {
            timerSwitch = true;
            timer = timeStart;
        }
        if (timerSwitch == true)
        {
            if (colorIsWhite)
            {
                Background.backgroundColor = colorBlack;
                colorIsWhite = false;
                foreach (SpawnLines LineSpawner in LineSpawner) // sets line spawner bools to true
                {
                    LineSpawner.justSwitched = true;
                }
            } else if (!colorIsWhite)
            {
                Background.backgroundColor = colorWhite;
                colorIsWhite = true;
                foreach (SpawnWhiteLines WhiteLineSpawner in WhiteLineSpawner)
                {
                    WhiteLineSpawner.justSwitched = true;
                }
            }
            



            timerSwitch = false;
        }
        playerAnimator.SetBool("colorIsWhite", colorIsWhite);
    }
}
