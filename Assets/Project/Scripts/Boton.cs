using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public GameObject platform;
    public GameObject botonActivo;
    public AudioSource audio;
    public GameObject camera;
    public bool reset;


    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Waiter());
        reset = false;





    }
    private void OnEnable()
    {
        Debug.Log("onEnable");
        StartCoroutine(Waiter());

    }

    // Update is called once per frame
    void Update()
    {
        



        //if (camera.activeSelf == false && reset == true)
        //{
        //    Debug.Log("reset");
        //    reset = false;
        //    Reset();
        //}
        //else if (camera.activeSelf == true && reset == true) 
        //{
        //    Debug.Log("reset1");
        //    reset = false;
        //    Reset();
        //}





    }

    public IEnumerator Waiter()
    {
        
        botonActivo.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platform.SetActive(false);
        
    }

    public void Reset()
    {
        StartCoroutine(Waiter());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) 
        {
            
            Debug.Log("checkBoton");
            audio.Play();
            platform.SetActive(true);
            botonActivo.SetActive(true);
            
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            Debug.Log("checkBoton");
            audio.Play();
            platform.SetActive(true);
            botonActivo.SetActive(true);

        }


    }
}
