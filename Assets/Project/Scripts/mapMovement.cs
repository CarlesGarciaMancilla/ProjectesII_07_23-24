using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Speeds { Lento = 0, Normal = 1, Rapido = 2, MuyRapido = 3 };
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Speeds CurrentSpeed;

    float[] SpeedValues = { 2f, 4f, 6f, 8f };
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
    }
}