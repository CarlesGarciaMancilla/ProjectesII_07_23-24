using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraChange : MonoBehaviour
{

    public Transform target;
    public GameObject camera1;
    public GameObject camera2;
    public Image panel;

    // Start is called before the first frame update
    void Start()
    {
        camera2.active = false;
        panel.CrossFadeAlpha(0, 1.0f, false);

    }

    // Update is called once per frame
    void Update()
    {

        if (target.transform.localScale == new Vector3(-1, 1, 1))
        {
            
            camera1.active = false;
            camera2.active = true;


        }
        else if (target.transform.localScale == new Vector3(1, 1, 1))
        {
            camera1.active = true;
            camera2.active = false;

        }
    }
}
