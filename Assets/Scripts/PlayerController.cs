using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
/************************* Variable Declarations *************************/

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] Animator animator;
    
    private Rigidbody2D body;

    /// <summary>
    /// Player input, left or right, between -1 and 1
    /// </summary>
    private float moveDirection;
    /// <summary>
    /// True if currently jumping, false if not
    /// </summary>
    private bool isJumping = false;
    /// <summary>
    /// True if facing right, false if facing left
    /// </summary>
    private bool isFacingRight = true;

/********************** End Variable Declarations ***********************/

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        readUserInput();
        if (body.velocity.y == 0)
        {
            isJumping = false;
        }
    }

    /// <summary>
    /// Process player input, pass data to animator to
    /// animate movement, and flip direction of player if needed
    /// </summary>
    private void readUserInput()
    {
        // Read and process movement
        moveDirection = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveDirection * moveSpeed, body.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
        if (Input.GetButtonDown("Jump") && !(isJumping))
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            isJumping = true;
        }

        // Flip character model if needed
        if (moveDirection < 0 && isFacingRight){flipCharacter();}
        if (moveDirection == 0 && !isFacingRight){flipCharacter();}
    }

    private void flipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180f,0f);
    }
}
