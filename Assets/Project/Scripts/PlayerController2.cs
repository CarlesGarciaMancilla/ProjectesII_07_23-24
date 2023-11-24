using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace TarodevController
{
  
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        private BoxCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        public GameObject particles;
        private string sceneName;
        public GameObject mapa;
        public GameObject sueloMapa;
        public Slider timeSlider;
        public GameObject infierno;
        public GameObject sueloInfierno;
        public InverseMapMovement inverseMovement;
        public Movement movement;


        public bool inferno = false;
        public bool canInferno = false;
        //public bool invencible = false;
        //public float timerInvencible = 5;
        public float timer = 5f;

        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<BoxCollider2D>(); // Cambiado de CapsuleCollider2D a BoxCollider2D
            sceneName = SceneManager.GetActiveScene().name;
            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
            _frameInput = new FrameInput();
            infierno.SetActive(false);
            sueloInfierno.SetActive(false);
            movement.enabled = false;
        }

        private void Update()
        {
            _time += Time.deltaTime;


            if (infierno.activeSelf == true) 
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (infierno.activeSelf == false)
            {
                timer -= Time.deltaTime;
                timeSlider.value = timer;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (timer <= 0) 
            {
                canInferno = true;
            }


      

        



            GatherInput();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                //JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
              //  Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                //invencible = false;
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
                particles.SetActive(false);
                
            }
        }

        private void FixedUpdate()
        {
            CheckCollisions();
            Debug.Log("Ground Hit: " + groundHit);
            Debug.Log("Ceiling Hit: " + ceilingHit);

            HandleJump();
            HandleDirection();
            HandleGravity();

            ApplyMovement();
        }
        private void ToInfierno(GameObject mapa, GameObject infierno)
        {
            mapa.SetActive(false);
            sueloMapa.SetActive(false);

            infierno.SetActive(true);
            sueloInfierno.SetActive(true);
            timeSlider.enabled = false;
            movement.enabled = true;
            inverseMovement.enabled = false;
            Respawn.instance.InfernoRespawn(gameObject);
            


        }

        private void ReturnToMap(GameObject mapa, GameObject infierno)
        {
            mapa.SetActive(true);
            sueloMapa.SetActive(true);
            infierno.SetActive(false);
            sueloInfierno.SetActive(false);
            timeSlider.enabled = true;
            timer = 5f;
            movement.enabled = false;
            inverseMovement.enabled = true;
            Respawn.instance.NormalRespawn(gameObject);
            

        }

        #region Collisions

        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;

        private void OnCollisionEnter2D(Collision2D collision)
        {
          
            if (collision.collider.CompareTag("traps"))
            {
                if (infierno.activeSelf == true)
                {
                    
                    ReturnToMap(mapa, infierno);

                }
                else if(infierno.activeSelf == false && canInferno == true)
                {
                    ToInfierno(mapa, infierno);
                }
                else 
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else if (collision.collider.CompareTag("final"))
            {
                SceneManager.LoadScene("menu");
            }
            else if (collision.collider.CompareTag("checkpoint"))
            {
                Respawn.instance.respawnPosition = gameObject.transform.position;
            }
            else if (collision.collider.CompareTag("checkpointInferno"))
            {
                Respawn.instance.respawnInfernoPosition = gameObject.transform.position;
            }

        }

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;


            // Ground and Ceiling
            bool groundHit = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
                particles.SetActive(true);
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;
        private string groundHit;
        private string ceilingHit;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }


  

       
    
}