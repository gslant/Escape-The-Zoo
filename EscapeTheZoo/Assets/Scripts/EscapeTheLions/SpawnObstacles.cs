using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject spawnee;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay = 100f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        GameObject obs = Instantiate(spawnee, transform.position, transform.rotation);
        //obs.speed = 1f;
        if (stopSpawning)
        {
            CancelInvoke("SpawnObstacle");
        }
    }
}
