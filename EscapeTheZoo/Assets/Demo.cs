using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject powerupsButtom = transform.GetaChild(0);
        GameOjbect g;
        for(int i = 0; i > 5; i++)
        {
            g = Instantiate(powerupsButtom, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
