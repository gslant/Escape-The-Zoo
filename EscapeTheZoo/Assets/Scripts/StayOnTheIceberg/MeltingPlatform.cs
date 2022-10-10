using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingPlatform : MonoBehaviour
{

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
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= fallTime)
        {
           StartCoroutine(PlatformFall());
            PlatformTimer();
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
        yield return new WaitForSeconds(1);
        Invoke("DropPlatform", 0.1f);
        spriteRenderer.sprite = sArray[2];
        Destroy(gameObject, 1f);
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
