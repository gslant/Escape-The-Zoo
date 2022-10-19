using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSquare : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    public CTFController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<CTFController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player 1"))
        {
            if(sprite.material.color == Color.green)
            {
                controller.greenNumber--;
            }
            if (sprite.material.color != Color.red)
            {
                sprite.material.color = Color.red;
                controller.redNumber++;
            }
        }
        if (collision.CompareTag("Player 2"))
        {
            if (sprite.material.color == Color.red)
            {
                controller.redNumber--;
            }
            if (sprite.material.color != Color.green)
            {
                sprite.material.color = Color.green;
                controller.greenNumber++;
            }
        }
        controller.updateSquareCounters();
    }
}
