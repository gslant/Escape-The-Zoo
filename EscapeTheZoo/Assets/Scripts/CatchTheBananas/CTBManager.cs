using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBManager : MonoBehaviour
{
    [SerializeField] GameObject bananaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBanana();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBanana()
    {
        GameObject banana = Instantiate(bananaPrefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.identity);
        banana.SetActive(true);
    }
}
