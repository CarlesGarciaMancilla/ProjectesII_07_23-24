using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{

    private Rigidbody2D _rb;

    [SerializeField] private float dashDistance = 5f; // Distancia que el dash deber�a recorrer
    private Vector2 dashStartPos;
    private float dashTravelledDistance;


    [SerializeField] private float dashSpeed = 10f; // Velocidad del dash


    private bool isTouchingDashTrigger = false;
    private bool isDashing = false;
    private float dashTimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingDashTrigger && Input.GetMouseButtonDown(1) && !isDashing)
        {
            StartDash();

        }
        else if (isDashing)
        {
            ContinueDash();
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("dash"))
        {
            isTouchingDashTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("dash"))
        {
            isTouchingDashTrigger = false;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashStartPos = transform.position; // Almacenar la posici�n inicial del dash
        dashTravelledDistance = 0f;
        _rb.gravityScale = 0; // Desactivar la gravedad durante el dash
        _rb.velocity = new Vector2(transform.right.x * dashSpeed, 0); // Establecer velocidad de dash
    }

    private void ContinueDash()
    {
        if (isDashing)
        {
            // Calcular la distancia recorrida desde el inicio del dash
            dashTravelledDistance = Vector2.Distance(dashStartPos, transform.position);

            if (dashTravelledDistance < dashDistance)
            {
                // El movimiento se controla mediante la velocidad establecida en StartDash
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
        _rb.gravityScale = 1; // Restaurar la gravedad
                              // Restablecer la velocidad horizontal y vertical a los valores previos al dash
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

}
