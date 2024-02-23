using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public GameObject platform;
    public GameObject botonActivo;


    // Start is called before the first frame update
    void Start()
    {
        platform.SetActive(false);
        botonActivo.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            Debug.Log("checkBoton");
            platform.SetActive(true);
            botonActivo.SetActive(true);

        }
        

    }
}