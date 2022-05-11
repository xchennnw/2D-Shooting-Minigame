using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    
    public GameObject Point;
    public GameObject Line;

    private float directionx;
    private float directiony;

    private InsectManager managerScript;
    private List<GameObject> pointList;
    private List<GameObject> lineList;
    public GameObject bodyPoint;

    private float[] groundX;
    private float[] groundY;
    private float PosX_LeftWall;
    private List<GameObject> CanonBallList;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly change direction of movement 
        InvokeRepeating("setDirection", 0f, 2f);

        pointList = new List<GameObject>();
        lineList = new List<GameObject>();
        managerScript = GameObject.Find("InsectManager").GetComponent<InsectManager>();
      
        float px = transform.position.x;
        float py = transform.position.y;

       
        //Draw an insect
        //Points

        
        var body = Instantiate(Point, new Vector2(px, py), Quaternion.identity);
        body.transform.parent = transform;
        bodyPoint = body;

        var c1 = Instantiate(Point, new Vector2(px-0.5f, py+2f), Quaternion.identity);
        c1.transform.parent = transform;
        var c2 = Instantiate(Point, new Vector2(px+0.5f, py+2f), Quaternion.identity);
        c2.transform.parent = transform;

        var a1 = Instantiate(Point, new Vector2(px - 3, py + 3), Quaternion.identity);
        a1.transform.parent = transform;
        var a2 = Instantiate(Point, new Vector2(px - 3, py + 1), Quaternion.identity);
        a2.transform.parent = transform;
        var a3 = Instantiate(Point, new Vector2(px - 3, py - 1), Quaternion.identity);
        a3.transform.parent = transform;
        var a4 = Instantiate(Point, new Vector2(px - 3, py - 3), Quaternion.identity);
        a4.transform.parent = transform;

        var b1 = Instantiate(Point, new Vector2(px + 3, py + 3), Quaternion.identity);
        b1.transform.parent = transform;
        var b2 = Instantiate(Point, new Vector2(px + 3, py + 1), Quaternion.identity);
        b2.transform.parent = transform;
        var b3 = Instantiate(Point, new Vector2(px + 3, py - 1), Quaternion.identity);
        b3.transform.parent = transform;
        var b4 = Instantiate(Point, new Vector2(px + 3, py - 3), Quaternion.identity);
        b4.transform.parent = transform;

        //Lines
        
        var L1 = Instantiate(Line);
        L1.transform.parent = transform;
        L1.GetComponent<LineScript>().setPoints(a1, a2);

        var L2 = Instantiate(Line);
        L2.transform.parent = transform;
        L2.GetComponent<LineScript>().setPoints(a2, a3);

        var L3 = Instantiate(Line);
        L3.transform.parent = transform;
        L3.GetComponent<LineScript>().setPoints(a3, a4);

        var L4 = Instantiate(Line);
        L4.transform.parent = transform;
        L4.GetComponent<LineScript>().setPoints(a1, body);

        var L5 = Instantiate(Line);
        L5.transform.parent = transform;
        L5.GetComponent<LineScript>().setPoints(a2, body);

        var L6 = Instantiate(Line);
        L6.transform.parent = transform;
        L6.GetComponent<LineScript>().setPoints(a3, body);

        var L7 = Instantiate(Line);
        L7.transform.parent = transform;
        L7.GetComponent<LineScript>().setPoints(a4, body);

        var L8 = Instantiate(Line);
        L8.transform.parent = transform;
        L8.GetComponent<LineScript>().setPoints(c1, body);


        var R1 = Instantiate(Line);
        R1.transform.parent = transform;
        R1.GetComponent<LineScript>().setPoints(b1, b2);

        var R2 = Instantiate(Line);
        R2.transform.parent = transform;
        R2.GetComponent<LineScript>().setPoints(b2, b3);

        var R3 = Instantiate(Line);
        R3.transform.parent = transform;
        R3.GetComponent<LineScript>().setPoints(b3, b4);

        var R4 = Instantiate(Line);
        R4.transform.parent = transform;
        R4.GetComponent<LineScript>().setPoints(b1, body);

        var R5 = Instantiate(Line);
        R5.transform.parent = transform;
        R5.GetComponent<LineScript>().setPoints(b2, body);

        var R6 = Instantiate(Line);
        R6.transform.parent = transform;
        R6.GetComponent<LineScript>().setPoints(b3, body);

        var R7 = Instantiate(Line);
        R7.transform.parent = transform;
        R7.GetComponent<LineScript>().setPoints(b4, body);

        var R8 = Instantiate(Line);
        R8.transform.parent = transform;
        R8.GetComponent<LineScript>().setPoints(c2, body);

        var T1 = Instantiate(Line);
        T1.transform.parent = transform;
        T1.GetComponent<LineScript>().setPoints(a1, c1);
        
        var T2 = Instantiate(Line);
        T2.transform.parent = transform;
        T2.GetComponent<LineScript>().setPoints(c1, c2);

        var T3 = Instantiate(Line);
        T3.transform.parent = transform;
        T3.GetComponent<LineScript>().setPoints(c2, b1);
       
        var T4 = Instantiate(Line);
        T4.transform.parent = transform;
        T4.GetComponent<LineScript>().setPoints(a4, b4);
        
        pointList.Add(a1);
        pointList.Add(a2);
        pointList.Add(a3);
        pointList.Add(a4);
        pointList.Add(b1);
        pointList.Add(b2);
        pointList.Add(b3);
        pointList.Add(b4);
        pointList.Add(body);
        pointList.Add(c1);
        pointList.Add(c2);

        lineList.Add(L1);
        lineList.Add(L2);
        lineList.Add(L3);
        lineList.Add(L4);
        lineList.Add(L5);
        lineList.Add(L6);
        lineList.Add(L7);
        lineList.Add(L8);

        lineList.Add(R1);
        lineList.Add(R2);
        lineList.Add(R3);
        lineList.Add(R4);
        lineList.Add(R5);
        lineList.Add(R6);
        lineList.Add(R7);
        lineList.Add(R8);       

        lineList.Add(T1);
        lineList.Add(T2);
        lineList.Add(T3);
        lineList.Add(T4);

        for (int i = 0; i < lineList.Count; i++)
        {
            lineList[i].GetComponent<LineScript>().visible = true;
            lineList[i].GetComponent<LineScript>().flex = 1f;
        }
        T1.GetComponent<LineScript>().visible = false;
        T2.GetComponent<LineScript>().visible = false;
        T3.GetComponent<LineScript>().visible = false;
        T4.GetComponent<LineScript>().visible = false;


        //Get positions of ground and wall
        GameObject render = GameObject.Find("TerrainRender");
        groundX = render.GetComponent<TerrainRender>().groundPosX;
        groundY = render.GetComponent<TerrainRender>().groundPosY;
        PosX_LeftWall = GameObject.Find("LeftWall").transform.position.x;
       


    }

    // Update is called once per frame
    void Update()
    {
        CanonBallList = GameObject.Find("Fire").GetComponent<Fire>().CanonBallList;
        DetectDestroyCondition();
        DetectCollidion_LeftWall();
        DetectCollidion_Ground();
        DetectCollidion_Canonball();

        //Random movement
        bodyPoint.transform.Translate(new Vector2(directionx, directiony) * Time.deltaTime * 0.6f);
       
    }
    
    void setDirection()
    {
        directionx = Random.Range(-1f, 1f);
        directiony = Random.Range(-1f, 1f);
    }

    void DetectDestroyCondition()
    {
        // Going out of boundry
        if (bodyPoint.transform.position.x <= -80 || bodyPoint.transform.position.x >= 50)
        {
            destroyAll();           
        }
        else if (bodyPoint.transform.position.y <= -42 || bodyPoint.transform.position.y >= 50)
        {
            destroyAll();        
        }
       
    }

    
    void DetectCollidion_LeftWall()
    {
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i].transform.position.x - PosX_LeftWall <= 4)
            {
                pointList[i].transform.Translate(Vector3.right * Time.deltaTime * 3f);
            }
        }

    }

    void DetectCollidion_Ground()
    {
        float distX;
        float distY;
        float dist;

        for (int i = 0; i < pointList.Count; i++)
        {
            for (int j = 0; j < 600; j++)
            {
                distX = pointList[i].transform.position.x - groundX[j];
                distY = pointList[i].transform.position.y - groundY[j];
                dist = Mathf.Sqrt(distX * distX + distY * distY);

                if (dist <= 5)
                {
                    //Hit the ground
                    pointList[i].transform.Translate(Vector3.up * Time.deltaTime * 2f);

                    //Hit the left side of the mound
                    if (pointList[i].transform.position.x <= 20 && pointList[i].transform.position.x >= 0)
                    {
                        pointList[i].transform.Translate(Vector3.right * Time.deltaTime * 3f);
                    }
                    //Hit the right side of the mound
                    else if (pointList[i].transform.position.x < 0 && pointList[i].transform.position.x >= -20)
                    {
                        pointList[i].transform.Translate(Vector3.left * Time.deltaTime * 4f);
                    }
                }
            }
        }

    }

    void DetectCollidion_Canonball()
    {
        float distX;
        float distY;
        float dist;
        for (int i = 0; i < pointList.Count; i++)
        {

            for (int j = 0; j < CanonBallList.Count; j++)
            {
                distX = pointList[i].transform.position.x - CanonBallList[j].transform.position.x;
                distY = pointList[i].transform.position.y - CanonBallList[j].transform.position.y;
                dist = Mathf.Sqrt(distX * distX + distY * distY);

                if (dist <= 6)
                {

                    if (distX <= 0) // If the insect is at left side of the ball
                    {
                        pointList[i].transform.Translate(Vector3.left * Time.deltaTime * 8f);
                    }
                    else            // at right side of the ball
                    {
                        pointList[i].transform.Translate(Vector3.right * Time.deltaTime * 8f);
                    }

                    if (distY <= 0) // below the ball
                    {
                        pointList[i].transform.Translate(Vector3.down * Time.deltaTime * 8f);
                    }
                    else           // above the ball
                    {
                        pointList[i].transform.Translate(Vector3.up * Time.deltaTime * 8f);
                    }

                }
            }
        }
    }

    

    void destroyAll()
    {
        // Destroy all the lines and points of this insect
        for(int i = 0; i < pointList.Count; i++)
        {
            Destroy(pointList[i]);
        }
        for (int i = 0; i < lineList.Count; i++)
        {
            Destroy(lineList[i]);
        }
        managerScript.deleteFromList(gameObject);
        Destroy(gameObject);
    }




}
