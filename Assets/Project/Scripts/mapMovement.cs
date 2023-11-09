using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum Speeds { Lento = 0, Normal = 1, Rapido = 2, MuyRapido = 3,Nada = 4 };
public class Movement : MonoBehaviour
{
    
    public Speeds CurrentSpeed;

    public GameObject player;
    public GameObject mapa;
    public GameObject infierno;

    float[] SpeedValues = { 5f, 10f, 15f, 20f, 0f };
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("personaje");
    }
    void Update()
    {
        transform.position += Vector3.left * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        
    }
}