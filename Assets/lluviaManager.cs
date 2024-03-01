using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class lluviaManager : MonoBehaviour
{

    public Transform target;
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (target.transform.localScale == new Vector3(-1, 1, 1))
        {

            if (particle.isPlaying) 
            {
                particle.Clear();
                particle.Stop();
            }


        }
        else if (target.transform.localScale == new Vector3(1, 1, 1))
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }

        }

    }
}
