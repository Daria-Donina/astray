using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask ground;
    public float moveInput;
    public float girlSpeed;
    public float girlJumpForce;

    private Rigidbody2D girlRigidbody2D;
    private Animator girlAnimator;

    private bool girlIsFacingRight = true;
    private bool girlIsJumping = false;
    private bool girlIsGrounded = false;

    void Start()
    {
        girlRigidbody2D = GetComponent<Rigidbody2D>();
        girlAnimator = GetComponent<Animator>();
    }

   
    void Update()
    {
        girlIsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);

        moveInput = Input.GetAxis("Horizontal");

        if (girlIsGrounded)
        {
            girlAnimator.SetFloat("Velocity", Mathf.Abs(moveInput));
        }

        else if (Input.GetButtonDown("Jump") && girlIsGrounded)
        {
            girlIsJumping = true;
            girlAnimator.SetTrigger("Jump");
        }


      
    }

    private void FixedUpdate()
    {
        girlRigidbody2D.velocity = new Vector2(moveInput * girlSpeed, girlRigidbody2D.velocity.y);

        if (girlIsFacingRight == false && moveInput > 0)
        {
            Flipgirl();
        }
        else if (girlIsFacingRight == true && moveInput < 0)
        {
            Flipgirl();
        }

        if (girlIsJumping)
        {
            girlRigidbody2D.AddForce(new Vector2(0f, girlJumpForce));

            girlIsJumping = false;
        }
    }

    private void Flipgirl()
    {
        girlIsFacingRight = !girlIsFacingRight;

        Vector3 girlScale = transform.localScale;
        girlScale.x *= -1;

        transform.localScale = girlScale;
    }
}

