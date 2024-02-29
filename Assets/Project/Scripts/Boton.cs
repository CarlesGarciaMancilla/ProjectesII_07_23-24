using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public GameObject platform;
    public GameObject botonActivo;
    public AudioSource audio;
    public GameObject camera;
    public bool reset = true;


    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Waiter());


        



    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.activeSelf == false && reset == true)
        {

            Reset();


        }





    }

    public IEnumerator Waiter()
    {
        botonActivo.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platform.SetActive(false);
        reset = false;
    }

    public void Reset()
    {
        StartCoroutine(Waiter());
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
