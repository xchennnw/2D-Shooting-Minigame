using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{

    public int speedValue;  //Speed value got from canon speed
    public float speedScale = 30f;
    public float gravity = 50f;
    public float friction = 0.005f;
    public float angle;     // Canon angle
    public float velocityX;
    public float velocityY;
    private float stationaryTime = 0;

    private float PosX_LeftWall;
    private float ScaleX_LeftWall;
    private float PosX_InvisibleWall;
    private float ballRadium;

    private float[] groundX;
    private float[] groundY;
    private bool addGravity;
    private Fire fireScript;



    // Start is called before the first frame update
    void Start()
    {
        //Initial speed
        float degree = 90 - (angle - 270);
        float radian = (degree / 360) * 2 * Mathf.PI;
        velocityX = speedValue* speedScale * Mathf.Cos(radian);
        velocityY = speedValue* speedScale * Mathf.Sin(radian);

        //Get wall positions and scales
        PosX_LeftWall = GameObject.Find("LeftWall").transform.position.x;
        ScaleX_LeftWall = GameObject.Find("LeftWall").transform.lossyScale.x;
        PosX_InvisibleWall = GameObject.Find("InvisibleWall").transform.position.x;
        ballRadium = transform.localScale.x / 6f;

        //Get ground positions and scales
        GameObject render = GameObject.Find("TerrainRender");
        groundX = render.GetComponent<TerrainRender>().groundPosX;
        groundY = render.GetComponent<TerrainRender>().groundPosY;

        fireScript = GameObject.Find("Fire").GetComponent<Fire>();
        addGravity = true;
    }



    // Update is called once per frame
    void Update()
    {
        
        // Update the time that canonball stays in stationary
        if (velocityY < 0.01 && velocityX < 0.01)
        {
            stationaryTime += 0.1f;
        }
        else
        {
            stationaryTime = 0;
        }

        transform.Translate(Vector3.left * Time.deltaTime * velocityX);
        transform.Translate(Vector3.up * Time.deltaTime * velocityY);
        
        DetectDestroyCondition();
        addGravity = true;
        DetectCollidion_LeftWall();
        DetectCollidion_Ground();

        //Gravity
        if (addGravity)
        {
            velocityY -= gravity * Time.deltaTime;
        }
        
       
        // Friction f = c*V^2 (c is some constant)
        velocityX += friction * (-velocityX) / velocityX* Time.deltaTime * velocityX * velocityX;
        velocityY += friction * (-velocityY) / velocityY* Time.deltaTime * velocityY * velocityY;
    }

    void DetectCollidion_LeftWall()
    {       
        if(transform.position.x- PosX_LeftWall <= ScaleX_LeftWall/2 + ballRadium/2)
        {
            velocityX = -velocityX * 0.8f;
        }
    }

    void DetectCollidion_Ground()
    {
        float distX ;
        float distY ;
        float dist ;
        float collisionNormX;
        float collisionNormY;
        float collisionSpeed;
        
        for (int i=0; i<600; i++)
        {
            distX = transform.position.x - groundX[i];
            distY = transform.position.y - groundY[i];
            dist = Mathf.Sqrt(distX * distX + distY * distY);
           
            if (dist <= 4)
            {
                collisionNormX = distX / dist;
                collisionNormY = -distY / dist;   

                //Here I add '-' for x.
                //because left is the -1 direction.
                //But in my code, velocityX is positive when translating to left.

                collisionSpeed = velocityX * collisionNormX + velocityY * collisionNormY;
                velocityY -= collisionSpeed * collisionNormY * 2f;
                velocityX -= collisionSpeed * collisionNormX * 2f;

                //some restitution     
                velocityY = velocityY * 0.9f; 
                velocityX = velocityX * 0.9f;

                // Make sure object not move into the ground
                if (velocityY < 0 && transform.position.y<=-35)
                {
                    velocityY = -velocityY;
                    addGravity = false;
                }
                if (velocityX > 0 && transform.position.x<=25 && transform.position.x >= 0)
                {
                    velocityX = -velocityX;                   
                }
                else if (velocityX < 0 && transform.position.x <= 0 && transform.position.x >= -25)
                {
                    velocityX = -velocityX;
                }
                break;
            }
            
        }

        
    }

    void DetectDestroyCondition()
    {
        //Boundary of scene
        if(transform.position.x<=-90 || transform.position.x >= 100)
        {
            fireScript.deleteFromList(gameObject);
            Destroy(gameObject);
            
        }
        else if (transform.position.y <= -42 || transform.position.y >= 50)
        {
            fireScript.deleteFromList(gameObject);
            Destroy(gameObject);
           
        }
        // Invisible wall
        else if (velocityX < 0 && transform.position.x>PosX_InvisibleWall && transform.position.x < PosX_InvisibleWall+5)
        {
            fireScript.deleteFromList(gameObject);
            Destroy(gameObject);
            
        }
        // Stay in stationary
        else if (stationaryTime >= 10)
        {

            fireScript.deleteFromList(gameObject); 
            Destroy(gameObject);
            
        }
    }
}
