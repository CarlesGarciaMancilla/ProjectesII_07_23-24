using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEdu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravity = 7f;
    private Vector2 currentVelocity;
    private Vector2 movementVector;
    private Vector2 dashVector;
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float swimForce;
    [SerializeField] float positionYDash;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;

    private bool wantsToJump = false;

    [SerializeField] bool wantsToDash = false;
    [SerializeField] bool canDash = false;
    [SerializeField] bool isDashing = false;

    public bool grounded { get; private set; }
    private bool lastGrounded;
    [SerializeField] Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2(walkForce, 0);
        dashVector = new Vector2(20, 0);

        grounded = true;
        lastGrounded = grounded;
        movementVector = new Vector2(walkForce, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
        wantsToDash = true;
        }
        else if (Input.GetKeyUp(KeyCode.C)) 
        {
            wantsToDash = false;
        }
        wantsToJump = Input.GetKey(KeyCode.Space);
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Grounded
        Collider2D[] checks = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);
        

        grounded = false;
        foreach (Collider2D c in checks)
        {
            grounded |= c.transform.CompareTag("ground");
        }

        grounded &= rb.velocity.y <= 0.1f;

        if(grounded && !lastGrounded)
        {
            //I touched the floor
            currentVelocity.y = 0.0f;
            movementVector.y = 0;
        }
        else if(!grounded && lastGrounded)
        {
            //Left the floor
            movementVector.y = -gravity;
        }

        lastGrounded = grounded;

        //Movement
        currentVelocity += movementVector * Time.fixedDeltaTime;
        currentVelocity.x = Mathf.Min(currentVelocity.x, walkForce);
        //VIGILAR AMB LA Y
        //currentVelocity.y = Mathf.Min(currentVelocity.y, maxFallVelocity);
        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

        if (wantsToJump && grounded)
        {
            wantsToJump = false;
            currentVelocity.y = jumpForce;
        }
        else if (wantsToDash && canDash && !isDashing) 
        {
            StartCoroutine(Dash());

            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("traps"))
        {
            Debug.Log(collision.name);
            Destroy(this.gameObject);
        }
        else if (collision.transform.CompareTag("dash")) 
        {
         canDash = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.transform.CompareTag("dash"))
        {
            canDash = false;
        }
    }


    private IEnumerator Dash() 
    {
        Debug.Log("dash");
        isDashing = true;
        float originalGravity = gravity;
        gravity =0f;
        Vector2 originalVelocity = currentVelocity;
        currentVelocity.y = 0.0f;
        currentVelocity = currentVelocity * dashPower;
        yield return new WaitForSeconds(dashTime);
        currentVelocity = originalVelocity;
        gravity = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashCooldown);

    }
}
