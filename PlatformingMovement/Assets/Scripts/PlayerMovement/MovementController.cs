using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float runForce;
    [SerializeField] float jumpforce;
    [SerializeField] float maxSpeed;

    float runInput;
    float jumpInput;
    bool moveRight;
    bool moveLeft;

    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        //gets access to the rigidbody component
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        print(rb.velocity.x);
    }
    

    void FixedUpdate()
    {
        if(runInput != 0)
        {
            Run();
        }

    }

    //gets the player input and sets it to a float
    void GetMovementInput()
    {
        runInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Vertical");
        if(runInput > 0)
        {
            moveRight = true;
            moveLeft = false;
        }

        if(runInput < 0)
        {
            moveLeft = true;
            moveRight = false;
        }
    }

    void Run()
    {
        //checks if the player has reached max speed
        if(Mathf.Abs(rb.velocity.x) >= maxSpeed)
        {
            return;
        }

        if(moveRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.AddForce(transform.right * runForce, ForceMode2D.Force);
            
        }

        if(moveLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.AddForce(transform.right * runForce, ForceMode2D.Force);
            
        }

    }

    
}
