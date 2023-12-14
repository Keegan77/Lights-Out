using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public Vector3 lightFellaTransport;
    public AudioSource jumpSound;
    public AudioSource diedSound;

    [Header("Layer Mask")]
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Jump")]
    private bool isJumping;

    [Header("fall physics")]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    [FormerlySerializedAs("maxYVelocity")] public float maxFallingSpeed = -30;

    [SerializeField] private float deathWaitTime;
    
    public bool isMoving;
    public bool onMovingPlat;
    Animator animator;
    private GameObject LightFella;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            diedSound.Play();
            StartCoroutine(WaitToDie());
        }
        if (other.CompareTag("MoveLightFella"))
        {
            LightFella.transform.position = lightFellaTransport;
        }

    }

    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(deathWaitTime);
        GameManager.Instance.RestartGame();
    }

    void Update()
    {
        rb.velocity = Vector2.Max(rb.velocity, new Vector2(rb.velocity.x, maxFallingSpeed));
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                                                 transform.localScale.y, 
                                                 transform.localScale.z);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 
                                               transform.localScale.y, 
                                               transform.localScale.z);
        }
        // Increases fall gravity speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }


        //lets player jump

        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)))
        {
            jumpSound.Play();
            rb.velocity = Vector2.up * jumpForce;
        }
        animator.SetBool("Moving", isMoving);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onMovingPlat = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onMovingPlat = false;
        }
    }
}