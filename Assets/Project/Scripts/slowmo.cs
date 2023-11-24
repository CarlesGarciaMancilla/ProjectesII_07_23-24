using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Time.timeScale = 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        { 
            Time.timeScale = 0.25f;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale = 1.0f;
        }


    }
}
