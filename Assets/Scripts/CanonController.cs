using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanonController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 q = transform.rotation.eulerAngles;
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {         
            if ((q.z>=-2&&q.z<=0) || (q.z > 270 &&q.z <= 360))
            {
                
                transform.Rotate(Vector3.forward*-10);

            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (q.z >=270 && q.z < 360)
            {
                transform.Rotate(Vector3.forward * 10);
            }
        }

       

    }
}
