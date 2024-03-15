using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEdu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravity = 7f;
    private Vector2 currentVelocity;
    private Vector2 movementVector;
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float swimForce;

    private bool wantsToJump = false;

    public bool grounded { get; private set; }
    private bool lastGrounded;
    [SerializeField] Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2(walkForce, 0);

        grounded = true;
        lastGrounded = grounded;
        movementVector = new Vector2(walkForce, 0.0f);
    }

    private void Update()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("ground"))
        {
            Debug.Log(collision.name);
            Destroy(this.gameObject);
        }
    }
}
