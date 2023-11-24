using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    private Vector3 offset = new Vector3(5f, 2f, -1f);
    private Vector3 offsetReverse = new Vector3(-5f, 2f, -1f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    //public GameObject target;
    //public float xOffset = 0;
    //public float yOffset = 0;
    //public float zOffset = 0;
    //public float xOffsetI = 0;
    //public float yOffsetI = 0;
    //public float zOffsetI = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Update()
    {


        //if (target.transform.localScale == new Vector3(-1, 1, 1))
        //{
        //    gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        //    this.transform.position = new Vector3(target.transform.position.x + xOffsetI,
        //                                     target.transform.position.y + yOffsetI,
        //                                     target.transform.position.z + zOffsetI);

        //}
        //else if (target.transform.localScale == new Vector3(1, 1, 1))
        //{
        //    gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);


        //    this.transform.position = new Vector3(target.transform.position.x + xOffset,
        //                                      target.transform.position.y + yOffset,
        //                                      target.transform.position.z + zOffset);



        //}




        if (target.transform.localScale == new Vector3(-1, 1, 1))
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            Vector3 targetPositionReverse = target.position + offsetReverse;
            transform.position = Vector3.SmoothDamp(transform.position, targetPositionReverse, ref velocity, smoothTime);

        }
        else if (target.transform.localScale == new Vector3(1, 1, 1))
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.position = new Vector3(targetPosition.x,smoothPosition.y,targetPosition.z);




        }



    }


    }
