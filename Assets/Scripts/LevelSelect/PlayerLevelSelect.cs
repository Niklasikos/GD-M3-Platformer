using System.Collections;
using UnityEngine;

public class PlayerLevelSelect : MonoBehaviour
{
    public static PlayerLevelSelect Instance;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform GroundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    public GameObject pauseMenu;
    public bool paused = false;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public float extraJumpsValue = 1;
    private float extraJumps;
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        extraJumpsValue = GameManager.Instance.data[1];
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if(!paused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    audioSource.Play();
                }
                else if (extraJumps > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    extraJumps--;
                    audioSource.Play();
                }
            }
        }

        SetAnimation(moveInput);

        if (moveInput > 0f) spriteRenderer.flipX = false;
        else if (moveInput < 0f) spriteRenderer.flipX = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation( float moveInput)
    {
        if(isGrounded)
        {
            if(moveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rb.linearVelocity.y > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
    }
}
