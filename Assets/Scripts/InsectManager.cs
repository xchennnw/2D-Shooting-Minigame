using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectManager : MonoBehaviour
{
    public GameObject Insect;
    public List<GameObject> InsectList;
    public int InsectNumber;  // Currently 6 insects in the scene


    // Start is called before the first frame update
    void Start()
    {
        InsectNumber = 6;
        float px;
        float py;
        InsectList = new List<GameObject>();
        for (int i=0; i< InsectNumber; i++)
        {
            px = Random.Range(-60, 0);
            py = Random.Range(-10, 50);
            var a = Instantiate(Insect, new Vector2(px, py), Quaternion.identity);
            InsectList.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Instantiate insects if some insects are destroyed
        int n = InsectNumber - InsectList.Count;
        if (n > 0)
        {
            float px;
            float py;
            for (int i = 0; i < n; i++)
            {
                px = Random.Range(-60, 0);
                py = Random.Range(-10, 50);
                var a = Instantiate(Insect, new Vector2(px, py), Quaternion.identity);
                InsectList.Add(a);
            }
        }
    }

    public void deleteFromList(GameObject a)
    {
        InsectList.Remove(a);
    }

}
