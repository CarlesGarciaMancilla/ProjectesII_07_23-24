using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject target;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);
    }
}
