using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaGallina : MonoBehaviour
{
    public ParticleSystem runParticle;

    // Start is called before the first frame update
    void Start()
    {
        runParticle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
