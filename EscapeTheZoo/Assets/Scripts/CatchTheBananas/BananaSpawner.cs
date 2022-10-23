using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    [SerializeField] GameObject bananaPrefab;
    [SerializeField] GameObject parentObject;

    public bool IsSpawning { get; set; }

    public IEnumerator SpawnBananas()
    {
        IsSpawning = true;
        float timeInterval = 4f;
        float bananaGravity = bananaPrefab.GetComponent<Rigidbody2D>().gravityScale;

        while (IsSpawning)
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
        GameObject banana = Instantiate(prefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.identity, parentObject.transform);
        banana.SetActive(true);
        return banana;
    }
}
