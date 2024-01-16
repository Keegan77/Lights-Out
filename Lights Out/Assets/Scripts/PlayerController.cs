using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public AudioSource jumpSound;

    [Header("Ground Check")]
    public Vector2 groundedBoxSize;
    private Vector2 groundedBoxPosition;
    public LayerMask whatIsGround;

    [Header("fall physics")]
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float maxFallingSpeed = -30;
    
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

    void FixedUpdate()
    {
        rb.velocity = Vector2.Max(rb.velocity, new Vector2(rb.velocity.x, maxFallingSpeed));
        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput != 0)
        {
            isMoving = true;
        }
        else isMoving = false;
    }
    
    void Update()
    {
        groundedBoxPosition = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2));

        OrientPlayer();
        
        // Increases fall gravity speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }

        if (CheckGround() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)))
        {
            jumpSound.Play();
            rb.velocity = Vector2.up * jumpForce;
        }
        animator.SetBool("Moving", isMoving);
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapBox(groundedBoxPosition, groundedBoxSize, 0, whatIsGround);
    }

    private void OrientPlayer()
    {
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * moveInput, 
                                                transform.localScale.y, 
                                                transform.localScale.z);
        }
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

    private void OnDrawGizmos()
    {
        //visualize grounded box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2)), groundedBoxSize);
    }
}