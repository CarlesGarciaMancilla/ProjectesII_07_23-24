using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    
    

    [SerializeField]
    private float movementScale = 10.0f;
    private float xMovement = 0.0f;
    private float jumpForce = 20.0f;
    private bool canJump = false;


    private string sceneName;



    [field:SerializeField]
    public int Health { get; private set; }


    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //xMovement = Input.GetAxis("Horizontal");

        rb.AddForce(Vector2.right * xMovement * movementScale, ForceMode2D.Force);

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            animator.SetBool("jump", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;

        }
        animator.SetBool("jump", false);

    }


    void FixedUpdate()
    {
        
        
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

