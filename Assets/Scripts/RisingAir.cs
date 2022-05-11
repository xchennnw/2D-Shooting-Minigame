using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RisingAir : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    private float speed1;
    private float speed2;
    private float speed3;
    private float pos1;
    private float pos2;
    private float pos3;


    private List<GameObject> InsectList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("setAirSpeed", 0f, 4f);   // set new speed at time intervals
        pos1 = GameObject.Find("RisingAir1").transform.position.x;
        pos2 = GameObject.Find("RisingAir2").transform.position.x;
        pos3 = GameObject.Find("RisingAir3").transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        
        InsectList = GameObject.Find("InsectManager").GetComponent<InsectManager>().InsectList;
        checkInsects();

    }

    void setAirSpeed()
    {
        speed1 = Random.Range(0, 5f);
        speed2 = Random.Range(0, 5f);
        speed3 = Random.Range(0, 5f);
        text1.text = "" + speed1.ToString("F1");
        text2.text = "" + speed2.ToString("F1");
        text3.text = "" + speed3.ToString("F1");
    }

    void checkInsects()
    {
        // Check if there is insect goes into rising air areas.
        float px;
        for (int i = 0; i < InsectList.Count; i++)
        {
            px = InsectList[i].GetComponent<Insect>().bodyPoint.transform.position.x;
            if(px >= pos1 - 3.5 && px <= pos1 + 3.5)
            {
                InsectList[i].transform.Translate(Vector3.up * Time.deltaTime * speed1);
            }
            if (px >= pos2 - 3.5 && px <= pos2 + 3.5)
            {
                InsectList[i].transform.Translate(Vector3.up * Time.deltaTime * speed2);
            }
            if (px >= pos3 - 3.5 && px <= pos3 + 3.5)
            {
                InsectList[i].transform.Translate(Vector3.up * Time.deltaTime * speed3);
            }
        }
    }
}
