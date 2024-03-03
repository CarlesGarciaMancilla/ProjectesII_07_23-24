using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public Animator[] enemiesAnimator;
    public Movement movementScript;
    public InverseMapMovement inverseMapMovement;

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
        if (movementScript.enabled == false && inverseMapMovement.enabled == false)
        {

            for (int i = 0; i < enemiesAnimator.Length; i++)
            {
                enemiesAnimator[i].enabled = false;
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

