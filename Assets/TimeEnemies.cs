using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnemies : MonoBehaviour
{
    public Animation[] enemies;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player.transform.position == new Vector3(0, 0, 0))
        {
            for (int i = 0; i <= enemies.Length - 1; i++) 
            {
                enemies[i].Stop();
            }
            
        }
        else 
        {
            for (int i = 0; i <= enemies.Length - 1; i++)
            {
                enemies[i].Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
