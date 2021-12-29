using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    private Rigidbody2D rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform frontCheck;
    bool faceRight = true;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    int extraJumps;
    [SerializeField] int extraJumpValue;
    float jumpTimeCounter;
    [SerializeField] float jumpTime;
    bool isHoldJumping = false;
    bool pressingJumpButton = false;
    float movementHorizontal;
    [SerializeField] float xWallForce;
    [SerializeField] float yWallForce;
    [SerializeField] float wallJumptime;
    bool walljumping;
    [SerializeField] LayerMask whatIsWall;
    bool wallSliding;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] ParticleSystem dustEffect;

    private Animator anim;

    void Start() {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(movementHorizontal * movementSpeed, rb.velocity.y);

        if (wallSliding) rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);

        if (walljumping) rb.velocity = new Vector2(xWallForce * -movementHorizontal, yWallForce);
    }

    void Update() {
        if (movementHorizontal < 0 && faceRight) {
            Flip();
        } else if (movementHorizontal > 0 && !faceRight) {
            Flip();
        }

        if (isGrounded() || isTouchingWall())
            extraJumps = extraJumpValue;

        if (pressingJumpButton && isHoldJumping) {
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else isHoldJumping = false;
        }
        if (isTouchingWall() && !isGrounded() && movementHorizontal != 0 && rb.velocity.y < 0)
            wallSliding = true;
        else wallSliding = false;

    }

    void Flip() {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        faceRight = !faceRight;
        transform.localScale = scaler;
        dustEffect.Play();
    }

    bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    bool isTouchingWall() {
        return Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);
    }


    public void Jump(InputAction.CallbackContext context) {

        //ilgili tusa basma eylemi
        if (context.started) {
            pressingJumpButton = true;
            anim.SetTrigger("jumpTrigger");
        }
        //ilgili tusu birakma eylemi 
        else if (context.canceled) {
            isHoldJumping = false;
            pressingJumpButton = false;
        }

        //duvardan ziplama
        if (context.started && isTouchingWall()) {
            dustEffect.Play();
            walljumping = true;
            rb.velocity = new Vector2(xWallForce * -movementHorizontal, yWallForce);
            Invoke("setWallJumpingToFalse", wallJumptime);
        }
        //normal ziplama 
        else if (context.started && isGrounded()) {
            rb.velocity = Vector2.up * jumpForce;
            isHoldJumping = true;
            jumpTimeCounter = jumpTime;
            dustEffect.Play();
        }
        //extra ziplama 
        else if (context.started && extraJumps > 0 && !isGrounded() && !isTouchingWall()) {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            dustEffect.Play();
        }
    }

    void setWallJumpingToFalse() {
        walljumping = false;
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.ReadValue<Vector2>().x != 0) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }
        movementHorizontal = context.ReadValue<Vector2>().x;
    }
}
