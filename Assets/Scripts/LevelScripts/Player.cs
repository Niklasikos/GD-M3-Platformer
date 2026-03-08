using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float coins;
    public int health = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform GroundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Image healthImage;
    public TextMeshProUGUI coinsText;
    public Scene currentScene;
    public string currentSceneName;
    public GameObject pauseMenu;
    public bool paused = false;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public AudioClip Jump;
    public AudioClip Damage;
    public float extraJumpsValue = 1;
    private float extraJumps;
    void Start()
    {
        Instance = this;
        extraJumpsValue = GameManager.Instance.data[1];
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        extraJumpsValue = GameManager.Instance.data[1];
        extraJumps = extraJumpsValue;

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        UpdateCoinCounter();
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
                    PlaySFX(Jump);
                }
                else if (extraJumps > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    extraJumps--;
                    PlaySFX(Jump);
                }
            }
        }


        SetAnimation(moveInput);

        healthImage.fillAmount = health / 100f;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            PlaySFX(Damage);  
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if(health <= 0)
            {
                Die();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            PlaySFX(Damage);  
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if(health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void UpdateCoinCounter()
    {
        coinsText.text = ": " + coins;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
