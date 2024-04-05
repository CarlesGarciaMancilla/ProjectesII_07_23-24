using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerEdu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravity = 7f;
    [SerializeField] float waterGravity = 7f;
    private Vector2 currentVelocity;
    private Vector2 movementVector;
    private Vector2 dashVector;
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpWaterForce;

    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;

    private bool wantsToJump = false;
    private bool agua = false;

    [SerializeField] bool wantsToDash = false;
    [SerializeField] bool canDash = false;
    [SerializeField] bool isDashing = false;

    public bool grounded { get; private set; }
    private bool lastGrounded;
    [SerializeField] Transform groundCheck;


    //cosas mapa
    private CapsuleCollider2D _col;
    //public ParticleSystem particles;
    private string sceneName;
    public GameObject mapa;
    public GameObject fondoMapa;
    public GameObject fondoInfierno;
    //public GameObject nubes;
    public Slider timeSlider;
    public GameObject infierno;
    public AudioSource audioTp;
    public AudioSource audioDeath;
    private Vector3 position;
    public Image panel;
    //public ParticleSystem muerteParticle;


    public bool inferno = false;
    public bool canInferno = false;
    public bool canReturn = false;
    public bool godMode = false;
    //public bool invencible = false;
    public float timer = 5f;

    // Start is called before the first frame update

    private void Awake()
    {
        _col = GetComponent<CapsuleCollider2D>(); 
        sceneName = SceneManager.GetActiveScene().name;

        infierno.SetActive(false);
        fondoInfierno.SetActive(false);
        timeSlider.maxValue = 10f;


    }
    void Start()
    {
        currentVelocity = new Vector2(walkForce, 0);
        dashVector = new Vector2(dashPower, 0);

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
        wantsToJump = Input.GetMouseButton(0);
        wantsToDash = Input.GetMouseButton(0);


        if (infierno.activeSelf == true)
        {
            timeSlider.gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (infierno.activeSelf == false)
        {
            timeSlider.gameObject.SetActive(true);
            if (currentVelocity == new Vector2(0.0f,0.0f))
            {
                timer = timeSlider.maxValue;
            }
            else
            {
                if (timer > 0.0f)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0 && !canInferno)
                    {
                        canInferno = true;
                    }
                }
            }

            timeSlider.value = timer;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (agua)
        {
            Collider2D[] checks1 = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);

            grounded = false;
            foreach (Collider2D c in checks1)
            {
                grounded |= c.transform.CompareTag("ground");
            }

            grounded &= rb.velocity.y <= 0.1f;

            if (grounded && !lastGrounded)
            {
                //I touched the floor
                currentVelocity.y = 0.0f;
                movementVector.y = 0;
            }
            else if (!grounded && lastGrounded)
            {
                //Left the floor
                movementVector.y = -waterGravity;
            }

            lastGrounded = grounded;

            //Movement
            currentVelocity += movementVector * Time.fixedDeltaTime;
            currentVelocity.x = Mathf.Min(currentVelocity.x, walkForce);
            //VIGILAR AMB LA Y
            //currentVelocity.y = Mathf.Min(currentVelocity.y, maxFallVelocity);
            rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

            if (wantsToJump && !isDashing)
            {
                wantsToJump = false;
                currentVelocity.y = jumpWaterForce;
            }

            if (wantsToDash && canDash && !isDashing)
            {
                StartCoroutine(Dash());


            }
        }
        else if (infierno.activeSelf == true) 
        {
            //Grounded
            Collider2D[] checks = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);

            grounded = false;
            foreach (Collider2D c in checks)
            {
                grounded |= c.transform.CompareTag("ground");
            }

            grounded &= rb.velocity.y <= 0.1f;

            if (grounded && !lastGrounded)
            {
                //I touched the floor
                currentVelocity.y = 0.0f;
                movementVector.y = 0;
            }
            else if (!grounded && lastGrounded)
            {
                //Left the floor
                movementVector.y = gravity;
            }

            lastGrounded = grounded;

            //Movement
            currentVelocity += movementVector * Time.fixedDeltaTime;
            currentVelocity.x = Mathf.Min(currentVelocity.x, walkForce);
            //VIGILAR AMB LA Y
            //currentVelocity.y = Mathf.Min(currentVelocity.y, maxFallVelocity);
            rb.MovePosition(rb.position - currentVelocity * Time.fixedDeltaTime);

            if (wantsToJump && grounded && !isDashing)
            {
                wantsToJump = false;
                currentVelocity.y = -jumpForce;
            }

            if (wantsToDash && canDash && !isDashing)
            {
                StartCoroutine(Dash());


            }
        }   
        else
        {
            //Grounded
            Collider2D[] checks = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);

            grounded = false;
            foreach (Collider2D c in checks)
            {
                grounded |= c.transform.CompareTag("ground");
            }

            grounded &= rb.velocity.y <= 0.1f;

            if (grounded && !lastGrounded)
            {
                //I touched the floor
                currentVelocity.y = 0.0f;
                movementVector.y = 0;
            }
            else if (!grounded && lastGrounded)
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

            if (wantsToJump && grounded && !isDashing)
            {
                wantsToJump = false;
                currentVelocity.y = jumpForce;
            }

            if (wantsToDash && canDash && !isDashing)
            {
                StartCoroutine(Dash());


            }
        }

        

    }

    private void ToInfierno(GameObject mapa, GameObject infierno)
    {
        currentVelocity.x = -currentVelocity.x;
        _col.enabled = true;
        panel.CrossFadeAlpha(0, 0.5f, false);
        audioTp.Play();
        mapa.SetActive(false);
        fondoMapa.SetActive(false);
        //nubes.SetActive(false);
        infierno.SetActive(true);
        fondoInfierno.SetActive(true);
        Respawn.instance.InfernoRespawn(gameObject);
    }

    private void ReturnToMap(GameObject mapa, GameObject infierno)
    {
        _col.enabled = true;
        panel.CrossFadeAlpha(0, 0.5f, false);
        audioTp.Play();
        mapa.SetActive(true);
        fondoMapa.SetActive(true);
        //nubes.SetActive(true);
        infierno.SetActive(false);
        fondoInfierno.SetActive(false);
        timeSlider.enabled = true;
        timer = 10f;
        //currentVelocity.x = -currentVelocity.x;
        Respawn.instance.NormalRespawn(gameObject);


    }

    public IEnumerator FadeInInfierno()
    {
        panel.CrossFadeAlpha(1, 0.1f, false);
        yield return new WaitForSeconds(1);
        ToInfierno(mapa, infierno);
    }

    public IEnumerator FadeInTierra()
    {
        panel.CrossFadeAlpha(1, 0.1f, false);
        yield return new WaitForSeconds(1);
        ReturnToMap(mapa, infierno);
    }
    public IEnumerator Muerte()
    {

        _col.enabled = false;
        Debug.Log("muerte");
        audioDeath.Play();
        //muerteParticle.Play();
        yield return new WaitForSeconds(0.3f);
        panel.CrossFadeAlpha(1, 0.05f, false);
        yield return new WaitForSeconds(0.5f);
        Respawn.instance.RestartLevel();
        SceneManager.LoadScene(sceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("traps"))
        {
            if (!godMode)
            {
                if (infierno.activeSelf == false && canInferno == true)
                {
                    _col.enabled = false;
                    Debug.LogError("Detected a trap, going to inferno");
                    StartCoroutine(FadeInInfierno());
                    canInferno = false;


                }
                else if (mapa.activeSelf == true && canInferno == false)
                {

                    Debug.Log("traps2");
                    StartCoroutine(Muerte());
                }
                else if (infierno.activeSelf == true)
                {

                    Debug.Log("traps3");
                    StartCoroutine(Muerte());
                }
            }
            else if (godMode)
            {
                if (infierno.activeSelf == false && canInferno == true)
                {
                    _col.enabled = false;
                    Debug.LogError("Detected a trap, going to inferno");
                    StartCoroutine(FadeInInfierno());
                    canInferno = false;


                }
            }
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("traps"))
        {
            Debug.Log(collision.name);
            //Destroy(this.gameObject);
        }
        else if (collision.transform.CompareTag("dash")) 
        {
         canDash = true;
        }
        else if (collision.transform.CompareTag("Water"))
        {
            agua = true;
        }
        else if (collision.CompareTag("checkpoint"))
        {
            Respawn.instance.respawnPosition = gameObject.transform.position;
        }
        else if (collision.CompareTag("checkpointInferno"))
        {

            Respawn.instance.respawnInfernoPosition = gameObject.transform.position;
            StartCoroutine(FadeInTierra());
        }
        else if (collision.CompareTag("final"))
        {
            if (sceneName == "Nivel1")
            {
                SceneManager.LoadScene("Nivel2");
            }
            else if (sceneName == "Nivel2")
            {
                SceneManager.LoadScene("Nivel3");
            }
            else if (sceneName == "Nivel3")
            {
                SceneManager.LoadScene("Nivel4");
            }
            else if (sceneName == "Nivel4")
            {
                SceneManager.LoadScene("Nivel5");
            }
            else if (sceneName == "Nivel5")
            {
                SceneManager.LoadScene("Nivel6");
            }
            else if (sceneName == "Nivel6")
            {
                SceneManager.LoadScene("Nivel7");
            }
            else
            {
                SceneManager.LoadScene("menu");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.transform.CompareTag("dash"))
        {
            canDash = false;
        }
        else if (collision.transform.CompareTag("Water"))
        {
            agua = false;
        }
        else if (collision.CompareTag("checkpointInferno"))
        {
            collision.enabled = false;

        }
    }


    private IEnumerator Dash() 
    {
        Debug.Log("dash");
        isDashing = true;
        float originalGravity = gravity;
        float initialVelX = currentVelocity.x;
        gravity =0f;
        currentVelocity = new Vector2(initialVelX * dashPower, 0.0f);
        movementVector.y = 0.0f;
        //Vector2 originalVelocity = currentVelocity;
        //rb.AddForce(dashVector,ForceMode2D.Impulse);
        //rb.MovePosition(rb.position + dashVector);
        yield return new WaitForSeconds(dashTime);
        movementVector.y = -gravity;
        currentVelocity.x = initialVelX;
        gravity = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashCooldown);

    }
}
