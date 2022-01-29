using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timeStart = 5;
    private float timer = 5;
    private float startAnim = 1.667f; // this is the time that the lightfella anim has to begin in order for the animation to end right when the scene changes from light to dark // vice versa
    private float endPreAnim = 3.33f; // value that the pre idle anim should end
    public bool colorIsWhite = true;
    public bool colorIsWhiteCopy;
    public bool timerSwitch;
    bool lightFellaAnim;
    bool endPreLightFellaAnim;
    public Color colorWhite = Color.white;
    public Color colorBlack = Color.black;
    GameObject Camera;
    Camera Background;
    Animator playerAnimator;
    Animator fellaAnimator;
    GameObject LightFella;
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
        LightFella = GameObject.FindGameObjectWithTag("LightFella");
        playerAnimator = Player.GetComponent<Animator>();
        fellaAnimator = LightFella.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        colorIsWhiteCopy = colorIsWhite;
        timer = timer - Time.deltaTime;
        //Debug.Log(timer);
        if (timer < 0)
        {
            timerSwitch = true;
            lightFellaAnim = false;
            endPreLightFellaAnim = false;
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
        if (timer < endPreAnim)
        {
            endPreLightFellaAnim = true;
        }
        if (timer < startAnim)
        {
            lightFellaAnim = true;
            Debug.Log("true");
        }
        playerAnimator.SetBool("colorIsWhite", colorIsWhite);
        fellaAnimator.SetBool("StartAnim", lightFellaAnim);
        fellaAnimator.SetBool("colorIsLight", colorIsWhiteCopy);
        fellaAnimator.SetBool("endPreAnim", endPreLightFellaAnim);
    }
}
