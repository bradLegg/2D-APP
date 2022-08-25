using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
/************************* Variable Declarations *************************/

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] Animator animator;
    [SerializeField] private LayerMask jumpableGround;
    
    private Rigidbody2D body;
    private BoxCollider2D coll;

    /// <summary>
    /// Player input, left or right, between -1 and 1
    /// </summary>
    private float moveDirection;
    /// <summary>
    /// True if facing right, false if facing left
    /// </summary>
    private bool isFacingRight = true;

/********************** End Variable Declarations ***********************/

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        readUserInput();

        // Flip character model if needed
        if (moveDirection < 0 && isFacingRight){flipCharacter();}
        if (moveDirection == 0 && !isFacingRight){flipCharacter();}
    }

    /// <summary>
    /// Process player input, pass data to animator
    /// </summary>
    private void readUserInput()
    {
        // Horizontal input
        moveDirection = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveDirection * moveSpeed, body.velocity.y);
        
        // Vertical input
        if (Input.GetButtonDown("Jump") && IsGrounded()){jump();}

        //Pass data to animator       
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    /************************* Function Definitions *************************/
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void flipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180f,0f);
    }
    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
    /********************** End Function Definitions **********************/
}
