using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider2D standingCollider;
    [SerializeField]
    private Collider2D crouchingCollider;
    [SerializeField]
    private float movementScale = 10.0f;
    private float xMovement = 0.0f;
    private float jumpForce = 20.0f;
    private bool canJump = false;
    private bool isCrouching = false;
    private string sceneName;



    //contador 
    private float crouchTime = 0.0f;
    public float maxCrouchTime = 5.0f;


    [field: SerializeField]
    public int Health { get; private set; }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;
    }

    void Update()
    {
      

        

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            StandUp();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }


       

        // Detectar si se presiona la flecha hacia abajo (tecla S o abajo)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Crouch();
        }


        // Controla el tiempo de agacharse
        if (isCrouching)
        {
            crouchTime += Time.deltaTime;

            if (crouchTime >= maxCrouchTime)
            {
                StandUp();
            }
        }

    }

    private void Crouch()
    {
        animator.SetBool("jump", true);
        standingCollider.enabled = false;
        crouchingCollider.enabled = true;
        isCrouching = true;
        crouchTime = 0.0f; // Reinicia el contador al agacharse.
    }

    private void StandUp()
    {
        animator.SetBool("jump", false);
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;
        isCrouching = false;
    }


    void FixedUpdate()
    {
        // Realizar acciones en FixedUpdate si es necesario.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("traps"))
        {
            SceneManager.LoadScene(sceneName);
        }

        if (collision.collider.CompareTag("ground"))
            canJump = true;
    }
}
