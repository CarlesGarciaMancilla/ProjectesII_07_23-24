using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerEdu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public Animator animator;
    [SerializeField] float gravity;
    [SerializeField] float waterGravity = 7f;
    private Vector2 currentVelocity;
    private Vector2 movementVector;
    private Vector2 dashVector;
    private Vector2 dashStartPos;
    private float originalGravity;
    private float dashTravelledDistance;
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpWaterForce;
    [SerializeField] float dashDistance = 5f;

    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;

    private bool wantsToJump = false;
    private bool agua = false;

    [SerializeField] bool wantsToDash = false;
    [SerializeField] bool canDash = false;
    [SerializeField] bool isDashing = false;

    [SerializeField] public bool grounded { get; private set; }
    [SerializeField] private bool lastGrounded;
    [SerializeField] Transform groundCheck;
    public ParticleSystem muerteParticle;


    //cosas mapa
    private CapsuleCollider2D _col;
    //public ParticleSystem particles;
    private string sceneName;
    public GameObject mapa;
    public GameObject fondoMapa;
    public GameObject fondoInfierno;
    public GameObject nubes;
    public GameObject hellLoading;
    public GameObject hellReady;
    public GameObject onboarding;
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
    public bool stop = true;
    public bool stopDash = false;
    //public bool invencible = false;
    public float timer = 5f;

    // Start is called before the first frame update

    private void Awake()
    {
        _col = GetComponent<CapsuleCollider2D>(); 
        sceneName = SceneManager.GetActiveScene().name;



        onboarding.SetActive(true);
        hellReady.SetActive(false);
        sceneName = SceneManager.GetActiveScene().name;
        infierno.SetActive(false);
        fondoInfierno.SetActive(false);
        timeSlider.maxValue = 10f;



        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontr� el componente Animator.", this);
        }


    }
    void Start()
    {
            currentVelocity = new Vector2(walkForce, 0);
            dashVector = new Vector2(dashPower, 0);


            movementVector = new Vector2(walkForce, 0.0f);
        
        grounded = true;
        lastGrounded = grounded;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }


            if (Input.GetKeyDown(KeyCode.G))
        {
            if (!godMode)
            {
                godMode = true;
            }
            else if (godMode)
            {
                godMode = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName);

        }


        if (!canInferno && infierno.activeSelf == false)
        {
            hellReady.SetActive(false);
            hellLoading.SetActive(true);
            onboarding.SetActive(true);
        }
        else if (infierno.activeSelf == true)
        {
            hellReady.SetActive(false);
            hellLoading.SetActive(false);
            onboarding.SetActive(false);
        }
        else
        {
            hellReady.SetActive(true);
            hellLoading.SetActive(false);
        }


        //if (Input.GetKeyDown(KeyCode.C)) 
        //{
        //wantsToDash = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.C))
        //{
        //    wantsToDash = false;
        //}
        //wantsToJump = Input.GetKey(KeyCode.Space);
        wantsToJump = Input.GetMouseButton(0);       
        wantsToDash = Input.GetMouseButton(0);
        if (Input.GetMouseButton(0)) 
        {
            stop = false;
        }


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
            /////////////////////////// MOVIMIENTO AGUA //////////////////////////////
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
                StartDash();


            }
        }
        else if (infierno.activeSelf == true) 
        {
            /////////////////////////// MOVIMIENTO INFIERNO //////////////////////////////

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
                animator.SetBool("Jump", false);
                stopDash = false;
                //I touched the floor
                currentVelocity.y = 0.0f;
                movementVector.y = 0;
            }
            else if (!grounded && lastGrounded)
            {
                //Left the floor
                animator.SetBool("Jump", true);
                movementVector.y = gravity;
            }

            lastGrounded = grounded;

            //Movement
            if (stop == false)
            {
                currentVelocity += movementVector * Time.fixedDeltaTime;
                currentVelocity.x = Mathf.Min(currentVelocity.x, walkForce);
                //VIGILAR AMB LA Y
                //currentVelocity.y = Mathf.Min(currentVelocity.y, maxFallVelocity);
                rb.MovePosition(rb.position - currentVelocity * Time.fixedDeltaTime);
            }
            if (wantsToJump && grounded && !isDashing && !stopDash)
            {
                wantsToJump = false;
                currentVelocity.y = -jumpForce;
                animator.SetBool("Jump", true);
            }

            if (wantsToDash && canDash && !isDashing)
            {
                StartDash();


            }
        }   
        else
        {
            /////////////////////////// MOVIMIENTO NORMAL //////////////////////////////
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
                animator.SetBool("Jump", false);
                stopDash = false;
                //I touched the floor
                currentVelocity.y = 0.0f;
                movementVector.y = 0;
            }
            else if (!grounded && lastGrounded)
            {
                //Left the floor
                animator.SetBool("Jump", true);
                movementVector.y = -gravity;
            }
            else if (isDashing)
            {

                ContinueDash();


            }

            lastGrounded = grounded;

            //Movement

            if (stop == false)
            {

                currentVelocity += movementVector * Time.fixedDeltaTime;
                currentVelocity.x = Mathf.Min(currentVelocity.x, walkForce);
                rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

            }

            //VIGILAR AMB LA Y
            //currentVelocity.y = Mathf.Min(currentVelocity.y, maxFallVelocity);


            if (wantsToJump && grounded && !isDashing && !stopDash)
            {
                wantsToJump = false;
                currentVelocity.y = jumpForce;
                animator.SetBool("Jump", true);
            }

            if (wantsToDash && canDash && !isDashing)
            {
                StartDash();


            }

            



        }

        

    }

    private void ToInfierno(GameObject mapa, GameObject infierno)
    {
        currentVelocity.x = 0.0f;
        _col.enabled = true;
        panel.CrossFadeAlpha(0, 0.5f, false);
        audioTp.Play();
        mapa.SetActive(false);
        fondoMapa.SetActive(false);
        nubes.SetActive(false);
        infierno.SetActive(true);
        fondoInfierno.SetActive(true);
        stop = true;
        Respawn.instance.InfernoRespawn(gameObject);
    }

    private void ReturnToMap(GameObject mapa, GameObject infierno)
    {
        _col.enabled = true;
        panel.CrossFadeAlpha(0, 0.5f, false);
        audioTp.Play();
        mapa.SetActive(true);
        fondoMapa.SetActive(true);
        nubes.SetActive(true);
        infierno.SetActive(false);
        fondoInfierno.SetActive(false);
        timeSlider.enabled = true;
        timer = 10f;
        stop = true;
        //currentVelocity.x = -currentVelocity.x;
        Respawn.instance.NormalRespawn(gameObject);


    }

    public IEnumerator FadeInInfierno()
    {
        panel.CrossFadeAlpha(1, 0.1f, false);
        yield return new WaitForSeconds(1);
        animator.SetBool("Death", false);
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
        muerteParticle.Play();
        animator.SetBool("Death", true);
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
                    animator.SetBool("Death", true);
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
            //if (!godMode)
            //{
            //    if (infierno.activeSelf == false && canInferno == true)
            //    {
            //        _col.enabled = false;
            //        Debug.LogError("Detected a trap, going to inferno");
            //        StartCoroutine(FadeInInfierno());
            //        canInferno = false;


            //    }
            //    else if (mapa.activeSelf == true && canInferno == false)
            //    {

            //        Debug.Log("traps2");
            //        StartCoroutine(Muerte());
            //    }
            //    else if (infierno.activeSelf == true)
            //    {

            //        Debug.Log("traps3");
            //        StartCoroutine(Muerte());
            //    }
            //}
            //else if (godMode)
            //{
            //    if (infierno.activeSelf == false && canInferno == true)
            //    {
            //        _col.enabled = false;
            //        Debug.LogError("Detected a trap, going to inferno");
            //        StartCoroutine(FadeInInfierno());
            //        canInferno = false;


            //    }
            //}
        }
        else if (collision.transform.CompareTag("dash")) 
        {
         canDash = true;
        }
        else if (collision.transform.CompareTag("Water"))
        {
            agua = true;
            animator.SetBool("Swim", true);

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
            animator.SetBool("Swim", false);
        }
        else if (collision.CompareTag("checkpointInferno"))
        {
            collision.enabled = false;

        }
    }


    //private IEnumerator Dash() 
    //{
    //    Debug.Log("dash");
    //    isDashing = true;
    //    float originalGravity = gravity;
    //    gravity =0f;
    //    Vector2 originalVelocity = currentVelocity;
    //    //rb.AddForce(dashVector,ForceMode2D.Impulse);
    //    rb.MovePosition(rb.position + dashVector);
    //    yield return new WaitForSeconds(dashTime);
    //    currentVelocity = originalVelocity;
    //    gravity = originalGravity;
    //    isDashing=false;
    //    yield return new WaitForSeconds(dashCooldown);

    //}


    private void StartDash()
    {
        stopDash = false;
        isDashing = true;
        originalGravity = gravity;
        dashStartPos = transform.position; // Almacenar la posicion inicial del dash
        Debug.Log(dashStartPos);
        gravity = 0f; // Desactivar la gravedad durante el dash
        currentVelocity.y = gravity;
    }

    private void ContinueDash()
    {
        if (isDashing)
        {
            
            // Calcular la distancia recorrida desde el inicio del dash
            dashTravelledDistance = Vector2.Distance(dashStartPos, transform.position);
            Debug.Log(dashTravelledDistance);
            if (dashTravelledDistance < dashDistance)
            {

                movementVector.y = gravity;
                
            }
            else
            {
                EndDash();
            }
        }
    }

    private void EndDash()
    {
        isDashing = false;
        gravity = originalGravity; // Restaurar la gravedad
        grounded = true;
        stopDash = true;

        // Restablecer la velocidad horizontal y vertical a los valores previos al dash

    }
}
