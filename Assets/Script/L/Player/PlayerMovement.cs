using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 
    [Header("Movement")]
    public float moveSpeed;
    

    public float groundDrag;

    public float jumpForce;
    public float jumpColdDown;
    public float airMultiplier;
    public float jumpMultiplier;
    bool readyToJump;

    public float drop;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHight;
    public LayerMask whatIsGround;
    public LayerMask ice;
    public LayerMask jumpPad;
    public LayerMask sticky;
    bool grounded;
    bool iced;
    bool stickied;
    bool jumpPaded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    //[Header("Pos")]
    //[SerializeField];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, whatIsGround);
        iced = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, ice);
        stickied = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, sticky);
        jumpPaded = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, jumpPad);

        MyInput();
        SpeedControl();

        //handed drag
        DragCheck();
        
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump)
        {
            if (grounded || iced || stickied || jumpPaded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpColdDown);
            }
        }
    }

    void MovePlayer()
    {
        //Caculate Player Direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if(grounded || jumpPaded == true)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f ,ForceMode.Force);
        else if(stickied)
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f ,ForceMode.Force);
        else if(iced)
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f ,ForceMode.Force);

        //in air
        else /*if(!grounded)*/
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(0, drop * -1, 0);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (stickied == true)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else if (jumpPaded == true)
        {
            rb.AddForce(transform.up * (jumpForce * jumpMultiplier), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void DragCheck()
    {
        if (grounded  || jumpPaded == true)
        {

            rb.drag = groundDrag;
        }
        else if(stickied == true) 
        {
            rb.drag = groundDrag * 1.2f;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
