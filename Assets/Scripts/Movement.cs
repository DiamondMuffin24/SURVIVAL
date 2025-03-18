using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        ProccessInputs();
        Animate();
        if(input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }
        



    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }
    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveX", lastMoveDirection.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}
