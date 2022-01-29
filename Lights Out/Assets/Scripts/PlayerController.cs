using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float ylimit = 5.5f;
    private float xlimit = 9.5f;
    public float maxYVelocity = -50;

    Animator animator;

    //Gets Rigidbody component

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    //Moves player on x axis

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    void Update()
    {
        

        if (rb.velocity.y < maxYVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxYVelocity);
        }
        if (transform.position.y < -ylimit)
        {
            transform.position = new Vector3(transform.position.x, ylimit, transform.position.z);
        }
        if (transform.position.y > ylimit)
        {
            transform.position = new Vector3(transform.position.x, -ylimit, transform.position.z);
        }
        if (transform.position.x > xlimit)
        {
            transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xlimit)
        {
            transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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


    }
}