using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public Animator[] enemiesAnimator;
    public bool start = false;
    public GameObject infierno;


    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("enemigoAnimator");
            enemiesAnimator[i] = enemies[i].GetComponent<Animator>();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            start = true;
        }

        if (start == false)
        {
            for (int i = 0; i < enemiesAnimator.Length; i++)
            {
                if (enemies[i].activeSelf == false) 
                {
                    enemies[i].SetActive(false);
                }
                
            }

            for (int i = 0; i < enemiesAnimator.Length; i++)
            {
                enemiesAnimator[i].enabled = false;
            }

        }
        else if (infierno.activeSelf == true) 
        {
            for (int i = 0; i < enemiesAnimator.Length; i++)
            {
                enemies[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < enemiesAnimator.Length; i++)
            {
                enemiesAnimator[i].enabled = true;
            }
        }

    }
}

