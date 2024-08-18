using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] Rigidbody2D rb;
    [SerializeField] float Speed, jumpForce, maxSpeed;
    float playerInput;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Animator animator;
    



    // Update is called once per frame
    void Update()
    {

        playerInput = Input.GetAxisRaw("Horizontal");

        
    }

    private void FixedUpdate()
    {
        if (isGrounded())
        {
            rb.velocity += new Vector2(playerInput * Speed, 0f);

            if (playerInput != 0)
            {
                animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            }
            else
            {
                animator.SetFloat("xVelocity", 0);
            }
            

        }
        else
        {
            animator.SetFloat("xVelocity", 0);
        }
        

        if (rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed ,maxSpeed),rb.velocity.y);
            
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }

        FlipPlayerSprite();

    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    private void FlipPlayerSprite()
    {
        if (rb.velocity.x > 0.2)
        {
            playerSprite.flipX = false;
        }
        if (rb.velocity.x < -0.2)
        {
            playerSprite.flipX = true;
        }
    }
}
