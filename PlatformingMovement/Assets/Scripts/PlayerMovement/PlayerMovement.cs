using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Data")]
    [SerializeField] float runForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;

    
    bool inputRight;
    bool inputLeft;

    //data for jump
    bool inputJump;
    bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        //gets the rigidbody componenet
        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    /*METHOD INFO
     * Checks what key the player has pressed and sets bools according to the input
     */
    void GetPlayerInput()
    {
        //checks if the player pressed the right or left keys
        if (Input.GetKey(KeyCode.D))
        {
            inputRight = true;
            inputLeft = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputRight = false;
            inputLeft = true;
        }
        else //if none of them are pressed then it defualts to no inputs being pressed
        {
            inputRight = false;
            inputLeft = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            inputJump = true;
        }
        else
        {
            inputJump = false;
        }
    }

    /*RUN METHOD INFO
     * Checks if the player has inputed basic movement options if so then adds a force in the direction depending on the input
     * in additon checks if the player has reached the movementspeed and limits according to the max speed
     */
    void Run()
    {
        //checks if the player reached max speed if so then it returns the function without adding any force
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            return;
        }

        if(inputRight == true && inputLeft == false)
        {
            rb.AddForce(Vector3.right * runForce,ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if(inputLeft == true && inputRight == false)
        {
            rb.AddForce(Vector3.left * runForce, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    /*
     * Checks if the player is on the ground and has pressed the jump input
     * if so then the player performs a jump by an applied impulse force
     * resets the short hop timer for next input
     */
    void Jump()
    {
            if(inputJump == true && isOnGround == true)
            {
                rb.AddForce(Vector3.up * jumpForce,ForceMode2D.Force);
                isOnGround = false;
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i =0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isOnGround = true;
                
            }
        }
    }
}
