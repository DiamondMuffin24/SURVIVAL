using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;

    private Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;
    private bool isDead = false;
    public bool canMove = true;
    public Sprite deathSprite;

    [Header("Footstep Settings")]
    public AudioSource audioSource;
    public List<AudioClip> footstepClips;
    public float footstepInterval = 0.4f; // Time between steps

    private float footstepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        footstepTimer = footstepInterval;
    }

    private void Update()
    {
        if (!canMove) return;

        if (!isDead)
        {
            ProcessInputs();
            Animate();

            if ((input.x < 0 && !facingLeft) || (input.x > 0 && facingLeft))
            {
                Flip();
            }

            HandleFootsteps(); // NEW: Footstep logic
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.linearVelocity = input * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = moveX;
        input.y = moveY;

        input.Normalize();
    }

    private void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    // ------------------ FOOTSTEP SOUND LOGIC ------------------

    private void HandleFootsteps()
    {
        if (input.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f; // Reset so it triggers quickly next time
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Count == 0 || audioSource == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Count)];
        audioSource.PlayOneShot(clip);
    }
}
