using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Fire : MonoBehaviour
{
 
    //------------------------------------------------------------
    //The Fire script controlls the instantiate of canon balls
    //And do the collision detection between canon balls
    //------------------------------------------------------------  
    
    private float CanonAngle;
    public Text text;
    public int speed;
    public GameObject point;
    public List<GameObject> CanonBallList;

    // Start is called before the first frame update
    void Start()
    {       
        speed = 5;
        CanonBallList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        // Get canon angle
        CanonAngle = GameObject.Find("Canon").transform.rotation.eulerAngles.z;

        // Control canon speed
        text.text = "speed: " + speed;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (speed <= 15 && speed > 1)
            {
                speed -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (speed < 15 && speed >= 1)
            {
                speed += 1;
            }
        }

        // Fire a canon ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var pointObj = Instantiate(point, transform.position, Quaternion.identity) as GameObject;
            pointObj.GetComponent<CanonBall>().speedValue = speed;
            if (CanonAngle >= 270 && CanonAngle <= 360)
            {
                pointObj.GetComponent<CanonBall>().angle = CanonAngle;
            }
            else
            {
                pointObj.GetComponent<CanonBall>().angle = 360;
            }
            CanonBallList.Add(pointObj);
        }

        // Detect collisions bewtween canon balls
        CanonBallCollision();
    }

    public void deleteFromList(GameObject a)
    {
        CanonBallList.Remove(a);
    }

    void CanonBallCollision()
    {
        GameObject obj1;
        GameObject obj2;
        float dist;
        float distX;
        float distY;
        for (int i=0; i<CanonBallList.Count; i++)
        {
            obj1 = CanonBallList[i];
            for (int j = i+1; j < CanonBallList.Count; j++)
            {
                obj2 = CanonBallList[j];
                distX = obj2.transform.position.x - obj1.transform.position.x;
                distY = obj2.transform.position.y - obj1.transform.position.y;
                dist = Mathf.Sqrt(distX * distX + distY * distY);
                if (dist <= 5)
                {
                    float collisionNormX = -distX / dist;
                    float collisionNormY = distY / dist;
                    float relativeVelocityX = obj1.GetComponent<CanonBall>().velocityX - obj2.GetComponent<CanonBall>().velocityX;
                    float relativeVelocityY = obj1.GetComponent<CanonBall>().velocityY - obj2.GetComponent<CanonBall>().velocityY;
                    float collisionSpeed = relativeVelocityX * collisionNormX + relativeVelocityY * collisionNormY;
                    float c = 1f;
                    obj1.GetComponent<CanonBall>().velocityY -= collisionSpeed * collisionNormY * c;
                    obj1.GetComponent<CanonBall>().velocityX -= collisionSpeed * collisionNormX * c;
                    obj2.GetComponent<CanonBall>().velocityY += collisionSpeed * collisionNormY * c;
                    obj2.GetComponent<CanonBall>().velocityX += collisionSpeed * collisionNormX * c;

                    //Currently not adding restitution for collsions between canon balls
                    float res = 1f;
                    obj1.GetComponent<CanonBall>().velocityY *= res;
                    obj1.GetComponent<CanonBall>().velocityX *= res;
                    obj2.GetComponent<CanonBall>().velocityY *= res;
                    obj2.GetComponent<CanonBall>().velocityX *= res;

                }
            }
        }
    }
}
