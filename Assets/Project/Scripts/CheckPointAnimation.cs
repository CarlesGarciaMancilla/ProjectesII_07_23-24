using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CheckPointAnimation : MonoBehaviour
{

    public ParticleSystem particles;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("checkpoint");
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        particles.Play();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("checkpoint2");
        if (collision.CompareTag("Player"))
        {
            particles.Play();
            audio.Play();
        }


    }

}
