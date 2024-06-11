using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    // Start is called before the first frame update
    public FondoMovimiento[] parallax;

    public bool start = false;
    public GameObject infierno;
    public GameObject parallaxFather;
    public PlayerControllerEdu player;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            start = true;
        }

        if (player.stop)
        {
            for (int i = 0; i < parallax.Length; i++)
            {
                parallax[i].enabled = false;
            }

            



        }
        else
        {
            for (int i = 0; i < parallax.Length; i++)
            {
                parallax[i].enabled = true;
            }
        }

        if (infierno.activeSelf == true)
        {
            parallaxFather.SetActive(false);
        }
        else if (infierno.activeSelf == false)
        {
            parallaxFather.SetActive(true);
        }

    }


}

