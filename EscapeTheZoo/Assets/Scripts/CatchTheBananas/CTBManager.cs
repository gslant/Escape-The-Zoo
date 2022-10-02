using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBManager : MonoBehaviour
{
    // Manages spawning of bananas and keeping track of the players' count

    [SerializeField] GameObject bananaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawningBananas();
    }

    // Update is called once per frame
    void Update()
    {
        // update coin count
    }

    IEnumerator TimeSpawn()
    {
        float timeInterval = 4f;
        float bananaGravity = bananaPrefab.GetComponent<Rigidbody2D>().gravityScale;

        while (true)
        {
            // Increase speed of banana spawning and falling 
            if (timeInterval >= 1f)
            {
                timeInterval *= 0.9f;
            }
            if (bananaGravity < 1f)
            {
                bananaGravity *= 1.1f;
                bananaPrefab.GetComponent<Rigidbody2D>().gravityScale = bananaGravity;
            }

            SpawnBanana();
            yield return new WaitForSeconds(timeInterval);
        }
    }

    void StartSpawningBananas()
    {
        StartCoroutine(TimeSpawn());
    }

    void SpawnBanana()
    {
        GameObject banana = Instantiate(bananaPrefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.identity);
        banana.SetActive(true);
    }
}
