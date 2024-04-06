using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    // Start is called before the first frame update
    public FondoMovimiento parallax;
    public PlayerController player;

    void Awake()
    {
        parallax.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.inverseMovement.enabled == true)
        {

            parallax.enabled = true;
        }
        else 
        {
            parallax.enabled = false;
        }
        
        
    }
}
