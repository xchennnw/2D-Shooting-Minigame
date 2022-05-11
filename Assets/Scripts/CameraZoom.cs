using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera; //Uses the camera. Made public for testing purposes//


    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize += 70;

    }


    void Update()
    {

    }
}
