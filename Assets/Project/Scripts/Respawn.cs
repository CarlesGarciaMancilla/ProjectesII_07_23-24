using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn instance;
    public Vector3 respawnPosition;
    public Vector3 respawnInfernoPosition;


    private void Awake() 
    {
        if (instance == null) 
        {
        instance = this;
            DontDestroyOnLoad(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InfernoRespawn(GameObject player) 
    {
        if (respawnInfernoPosition != null)
        {
            player.transform.position = respawnInfernoPosition;
        }
    }

    public void NormalRespawn(GameObject player)
    {
        if (respawnPosition != null)
        {
            player.transform.position = respawnPosition;
        }
    }
}
