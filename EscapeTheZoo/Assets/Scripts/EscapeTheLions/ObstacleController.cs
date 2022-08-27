using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(-6f * speed * Time.deltaTime, 0, 0);
        Debug.Log("speed = " + speed);

        if(transform.position.x < -10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
            speed *= 1.1f;
        }
    }
}
