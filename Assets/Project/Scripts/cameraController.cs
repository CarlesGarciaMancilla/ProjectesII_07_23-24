using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject target;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;
    public float xOffsetI = 0;
    public float yOffsetI = 0;
    public float zOffsetI = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);

        if (target.transform.localScale == new Vector3(-1, 1, 1)) 
        {
            gameObject.transform.localRotation = Quaternion.Euler(0,0,180);
            this.transform.position = new Vector3(target.transform.position.x + xOffsetI,
                                             target.transform.position.y + yOffsetI,
                                             target.transform.position.z + zOffsetI);

        }
    }
}
