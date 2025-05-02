using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private bool isAlive = true;
    private Rigidbody2D body;

    private Animator movement;
    private BoxCollider2D boxCollider;


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;

    //Gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingRate = 0.5f;
    private float firingTimer;


    private float jumpingPower = 13f;
    private float coyoteTime;//stops jumping from being justframe
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool isSliding = false;
    public float slideForce = 100f;
    public float slideDuration = .05f;




    private bool isShooting = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); //gets rigidbody2d thats attached to the player object
        movement = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    //note when player leaves grounded, and let them jkump if its pressed within coyote threshold






    private void Update() //makes sure that on ERVERY frame, the movement is being recorded.
    {
        

        //CONTINUous shooting, space between bullets
        if (Input.GetKeyDown(KeyCode.Z) && firingTimer <= 0f)
        {
            
            isShooting = true;
            firingTimer = firingRate; 
        }

        // If Z key is released, stop shooting
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopShoot();
        }
        firingTimer -= Time.deltaTime;

        if (firingTimer <= 0f && isShooting)
        {
            Shoot();
            firingTimer = firingRate;

        }



        //else
        //{
        //   firingTimer -= Time.deltaTime;
        // }

        /*if (isShooting == true)
            Shoot();
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }*/



        //sliding
        /* if (Input.GetKeyDown(KeyCode.X) && !isSliding )
         {
             Slide();

         }*/



        //player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y); //define velocity of all three directions //y and z make it so uy dont wanna change velocity on y axis.

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

         
        movement.SetBool("isMoving", horizontalInput != 0); //arrow keys not pressed:  horizontal input = 0;
        if (isGrounded() && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            Run();
        else
        {
            // If Shift key is not held down, stop running
            movement.SetBool("isRunning", false);
        }
        movement.SetBool("Grounded", isGrounded());
        //Player movement end



        //Jumping
        if (isGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;
        
        if (jumpBufferCounter > 0f && (coyoteTimeCounter > 0f || isGrounded()))
        {
            Jump();
            body.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            //body.velocity = new Vector2(body.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * .3f);

            coyoteTimeCounter = 0f; // reset coyote time after the jump
        }

        //Jumping end


    }

    private void Shoot()
    {
        //Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        GameObject bullet = Pooling.instance.GetPooledObject();

        if (bullet != null) 
        { 
            bullet.transform.position = firingPoint.position;
            bullet.SetActive(true);
        }
        
    }

    private void StopShoot()
    {
        isShooting = false;
        Debug.Log("stopped holding");

    }

    private void Run()
    {
      
            // Increase speed when running
            float runSpeed = speed * 2f; // You can adjust the factor as needed
            float horizontalInput = Input.GetAxis("Horizontal");

            // Update velocity based on horizontal input and running speed
            body.velocity = new Vector2(horizontalInput * runSpeed, body.velocity.y);

            // Set animator parameter for running
            movement.SetBool("isRunning", true);

        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    /*
    void Slide()
    {

        //add speed when sliding
        isSliding = true;
        movement.SetTrigger("isSliding");

        body.AddForce(transform.right * slideForce , ForceMode2D.Impulse);


        // Stop sliding after a certain duration
        Invoke("StopSlide", slideDuration);
        
        Debug.Log("sliding"); //debuygger

    }

    IEnumerator StopSlideAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopSlide();
    }

    void StopSlide()
    {
        isSliding = false;
        body.velocity = Vector2.zero; //what dfoes this mean?
        movement.ResetTrigger("isSliding"); //stops slide animation
        Debug.Log("stopped sliding"); //debugger
    }

    */

    private void Jump()
    {
        Debug.Log("Jumping");

        body.velocity = new Vector2(body.velocity.x, jumpingPower);
        movement.SetTrigger("isJumping");
    }

   
    }

