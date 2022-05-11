using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    private float oldpx;
    private float oldpy;
    public float vx;
    public float vy;
    public float offsetx;
    public float offsety;

    // Start is called before the first frame update
    void Start()
    {
        oldpx = transform.position.x;
        oldpy = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void FixedUpdate()
    {
        vx = transform.position.x - oldpx;
        vy = transform.position.y - oldpy;
        
        oldpx = transform.position.x;
        oldpy = transform.position.y;

        //Here I add '-' for vx and offsetx because in the scene, left is the -1 direction.
        transform.Translate(Vector3.up  * vy);
        transform.Translate(Vector3.left  * -vx);
       
    }


}
