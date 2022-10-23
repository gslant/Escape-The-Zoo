using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingPlatform : MonoBehaviour
{
    // Sounds
    public AudioSource crackedIceAudio;
    public AudioSource shatteredIceAudio;

    public static bool gameOver = false;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sArray; //this is here so that when the platform is about to break it can show a different sprite to warn the players

    Rigidbody2D rb;

    public float maxTime = 25;
    public float minTime = 6;

    private float time;

    private float fallTime;

    void Start()
    {
        spriteRenderer.sprite = sArray[0];
        PlatformTimer();
        time = minTime;
        rb = GetComponent<Rigidbody2D>();
        gameOver = false;
        crackedIceAudio.mute = false;
        shatteredIceAudio.mute = false;
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            time += Time.deltaTime;

            if (time >= fallTime)
            {
                StartCoroutine(PlatformFall());
                PlatformTimer();
            }
        }
        else
        {
            crackedIceAudio.mute = true;
            shatteredIceAudio.mute = true;
        }
    }

    void PlatformTimer()
    {
        fallTime = Random.Range(minTime, maxTime);
    }

    IEnumerator PlatformFall()
    {
        time = minTime;
        spriteRenderer.sprite = sArray[1];
        crackedIceAudio.Play();
        yield return new WaitForSeconds(1);
        Invoke("DropPlatform", 0.1f);
        spriteRenderer.sprite = sArray[2];
        shatteredIceAudio.Play();
        Destroy(gameObject, 1f);
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
