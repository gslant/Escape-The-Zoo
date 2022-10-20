using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSquare : MonoBehaviour
{
    // Sounds
    private AudioSource playerOneBeepAudio;
    private AudioSource playerTwoBeepAudio;

    [SerializeField]
    private SpriteRenderer sprite;
    public CTFController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<CTFController>();
        playerOneBeepAudio = GameObject.Find("PlayerOneBeepAudio").GetComponent<AudioSource>();
        playerTwoBeepAudio = GameObject.Find("PlayerTwoBeepAudio").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player 1"))
        {
            playerOneBeepAudio.Play();

            if (sprite.material.color == Color.green)
            {
                controller.greenNumber--;
            }
            if (sprite.material.color != Color.red)
            {
                sprite.material.color = Color.red;
                controller.redNumber++;
            }
        }
        else if (collision.CompareTag("Player 2"))
        {
            playerTwoBeepAudio.Play();

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
    }
}
