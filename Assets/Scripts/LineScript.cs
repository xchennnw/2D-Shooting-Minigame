using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private GameObject p0;
    private GameObject p1;
    private float startLength;
    LineRenderer renderer;
    private float dx;
    private float dy;
    private float dist;
    private float diff;
    private float diffX;
    private float diffY;
    private PointScript p0Script;
    private PointScript p1Script;
    public bool visible;
    public float flex;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
        dx = p0.transform.position.x - p1.transform.position.x;
        dy = p0.transform.position.y - p1.transform.position.y;
        startLength = Mathf.Sqrt(dx * dx + dy * dy);
        p0Script = p0.GetComponent<PointScript>();
        p1Script = p1.GetComponent<PointScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        //Calculate the offset of point movements caused by change of line length
        if (visible)
        {
            renderer.SetPosition(0, p0.transform.position);
            renderer.SetPosition(1, p1.transform.position);
        }
        
        dx = p1.transform.position.x - p0.transform.position.x;
        dy = p1.transform.position.y - p0.transform.position.y;
        dist = Mathf.Sqrt(dx * dx + dy * dy);
        diff = (startLength - dist) / dist / 2;
       
        diffX = dx * diff * flex;
        diffY = dy * diff * flex;
       
        p0.transform.Translate(Vector3.up * -diffY);
        p0.transform.Translate(Vector3.left * diffX);
        p1.transform.Translate(Vector3.up * diffY);
        p1.transform.Translate(Vector3.left * -diffX);
    }

    public void setPoints(GameObject a, GameObject b)
    {
        p0 = a;
        p1 = b;
    }
}
