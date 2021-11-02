using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStateList pState;
    public Rigidbody2D rb2d;


    float xAxis;
    float yAxis;
    float grabity;
    int stepsJumped = 0;

    int stepsXRecoiled;
    int stepsYRecoiled;

    [Header("X Axis Movement")]
    [SerializeField] float walkSpeed = 5f;

    [Space(5)]

    [Header("Y Axis Movement")]
    [SerializeField] float jumpSpeed = 5;
    [SerializeField] float fallSpeed = 5;
    [SerializeField] int jumpSteps = 7;
    [SerializeField] int jumpThreshold = 3;

    [Space(5)]
    [Header("Recoil")]
    [SerializeField] int recoilXSteps = 4;
    [SerializeField] int recoilYSteps = 10;
    [SerializeField] float recoilXSpeed = 45;
    [SerializeField] float recoilYSpeed = 45;


    private void Start()
    {
        pState = GetComponent<PlayerStateList>();
        rb2d = GetComponent<Rigidbody2D>();

        grabity = rb2d.gravityScale;

        pState.lookingRight = true;
    }

    private void Update()
    {
        GetInputs();

        Flip();
        Walk(xAxis);
        Recoil();
    }
    void FixedUpdate()
    {
        if (pState.recoilingX == true && stepsXRecoiled < recoilXSteps)
        {
            stepsXRecoiled++;
        }
        else
        {
            StopRecoilX();
        }
        if (pState.recoilingY == true && stepsYRecoiled < recoilYSteps)
        {
            stepsYRecoiled++;
        }
        else
        {
            StopRecoilY();
        }
        if (pState.isGrounded)
        {
            StopRecoilY();
        }

        Jump();
    }

    

    void GetInputs()
    {
        //WASD/Joystick
        yAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");

        //This is essentially just sensitivity.
        if (yAxis > 0.25)
        {
            yAxis = 1;
        }
        else if (yAxis < -0.25)
        {
            yAxis = -1;
        }
        else
        {
            yAxis = 0;
        }

        if (xAxis > 0.25)
        {
            xAxis = 1;
        }
        else if (xAxis < -0.25)
        {
            xAxis = -1;
        }
        else
        {
            xAxis = 0;
        }


        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (pState.isGrounded){
                pState.jumping = true;
                if (pState.usingDragon)
                {
                    pState.doublejump = true;
                }
            }
            else if(pState.doublejump){
                pState.jumping = true;
                pState.doublejump = false;
            }
        }

        if (!Input.GetButton("Jump") && stepsJumped < jumpSteps && stepsJumped > jumpThreshold && pState.jumping)
        {
            StopJumpQuick();
        }
        else if (!Input.GetButton("Jump") && stepsJumped < jumpThreshold && pState.jumping)
        {
            StopJumpSlow();
        }

        
    }

    void Jump()
    {
        if (pState.jumping)
        {

            if (stepsJumped < jumpSteps && !pState.isRoofed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                stepsJumped++;
            }
            else
            {
                StopJumpSlow();
            }
        }

        //This limits how fast the player can fall
        //Since platformers generally have increased gravity, you don't want them to fall so fast they clip trough all the floors.
        if (rb2d.velocity.y < -Mathf.Abs(fallSpeed))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
        }
    }
    void StopJumpQuick()
    {
        //Stops The player jump immediately, causing them to start falling as soon as the button is released.
        stepsJumped = 0;
        pState.jumping = false;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    }
    void StopJumpSlow()
    {
        //stops the jump but lets the player hang in the air for awhile.
        stepsJumped = 0;
        pState.jumping = false;
    }

    void Recoil()
    {
        //since this is run after Walk, it takes priority, and effects momentum properly.
        if (pState.recoilingX)
        {
            if (pState.lookingRight)
            {
                rb2d.velocity = new Vector2(-recoilXSpeed, 0);
            }
            else
            {
                rb2d.velocity = new Vector2(recoilXSpeed, 0);
            }
        }
        if (pState.recoilingY)
        {
            if (yAxis < 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, recoilYSpeed);
                rb2d.gravityScale = 0;
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -recoilYSpeed);
                rb2d.gravityScale = 0;
            }

        }
        else
        {
            rb2d.gravityScale = grabity;
        }
    }
    void StopRecoilX()
    {
        stepsXRecoiled = 0;
        pState.recoilingX = false;
    }
    void StopRecoilY()
    {
        stepsYRecoiled = 0;
        pState.recoilingY = false;
    }

    void Flip()
    {
        if (xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            pState.lookingRight = true;
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            pState.lookingRight = false;
        }
    }
    void Walk(float MoveDirection)
    {
        if (!pState.recoilingX)
        {
            rb2d.velocity = new Vector2(MoveDirection * walkSpeed, rb2d.velocity.y);

            if (Mathf.Abs(rb2d.velocity.x) > 0)
            {
                pState.walking = true;
            }
            else
            {
                pState.walking = false;
            }
            if (xAxis > 0)
            {
                pState.lookingRight = true;
            }
            else if (xAxis < 0)
            {
                pState.lookingRight = false;
            }

        }

    }
}
