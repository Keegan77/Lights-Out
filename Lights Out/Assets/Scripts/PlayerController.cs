using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    [Header("Layer Mask")]
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Jump")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("fall physics")]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float maxYVelocity = -50;

    public bool isMoving;
    Animator animator;
    GameObject GameManagerObj;
    GameManager gameManager;
    GameObject LightFella;

    //Gets Rigidbody component

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = GameManagerObj.GetComponent<GameManager>();
        LightFella = GameObject.FindGameObjectWithTag("LightFella");
    }

    //Moves player on x axis

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput != 0)
        {
            isMoving = true;
        }
        else isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelComplete"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Complete");
        }

        if (other.CompareTag("DeathPlane") || other.CompareTag("Spikes"))
        {
            gameManager.playerDied = true;
            Destroy(gameObject);
        }
        if (other.CompareTag("MoveLightFella"))
        {
            LightFella.transform.position = new Vector3(17.68f, 1.44f, 0);
        }
    }

    void Update()
    {

        if (rb.velocity.y < maxYVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxYVelocity);
        }


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1.248608f, 1.249893f, 1.248608f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1.248608f, 1.248608f, 1.248608f);
        }

        //cool jump fall

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (rb.velocity.y > 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        }
        //fixed double jump bug
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }

        //lets player jump

        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        //makes you jump higher when you hold down space
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                //rb.velocity = Vector2.up * jumpForce;
                //jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        animator.SetBool("Moving", isMoving);
    }

}