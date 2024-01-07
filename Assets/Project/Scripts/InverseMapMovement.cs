using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InverseMapMovement : MonoBehaviour
{
    public Speeds CurrentSpeed;

    float[] SpeedValues = { -5f, -10f, -15f, -20f, -0f };
    // Update is called once per frame

    private void Start()
    {

    }
    void Update()
    {
        transform.position += Vector3.left * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;


    }
}
