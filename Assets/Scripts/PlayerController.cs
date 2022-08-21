using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] Animator animator;
    private Rigidbody2D body;
    private float moveDirection;
    private bool isJumping = false;
    private bool isFacingRight = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Read left/right movement input and apply velocity, pass data to animator
        moveDirection = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveDirection * moveSpeed, body.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        // Read jump input
        if(Input.GetButtonDown("Jump") && !(isJumping))
        {
            jump();
        }
        if(body.velocity.y == 0)
        {
            isJumping = false;
        }

        if(moveDirection < 0 && isFacingRight)
        {
            flipCharacter();
        } 
        if(moveDirection == 0 && !isFacingRight)
        {
            flipCharacter();
        }

    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isJumping = true;
    }
        private void flipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180f,0f);
    }
}
