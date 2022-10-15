using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSquare : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player 1"))
        {
            Debug.Log("Hit Player 1");
            sprite.material.color = Color.red;
        }
        if (collision.CompareTag("Player 2"))
        {
            Debug.Log("Hit Player 2");
            sprite.material.color = Color.green;
        }
    }
}
