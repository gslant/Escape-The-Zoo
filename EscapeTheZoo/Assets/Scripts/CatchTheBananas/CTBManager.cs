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
    void StartSpawningBananas()
    {
        StartCoroutine(TimeSpawn());
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

            SpawnBanana(bananaPrefab);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public GameObject SpawnBanana(GameObject prefab)
    {
        GameObject banana = Instantiate(prefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.identity);
        banana.SetActive(true);
        return banana;
    }
}
