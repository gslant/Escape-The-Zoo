using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] BackgroundScroller groundImage;

    public float speed;
    bool isPaused = false;
    float pauseTime = 0;
    float oldSpeed;
    float randomTime = 4;
    public static bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.8f;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (isPaused && Time.time > pauseTime + randomTime)
            {
                randomTime = Random.Range(0, 100f / Time.time);
                speed = oldSpeed;
                isPaused = false;
            }
            transform.position = transform.position + new Vector3(-6f * speed * Time.deltaTime, 0, 0);

            if (transform.position.x < -10)
            {
                isPaused = true;
                pauseTime = Time.time;
                transform.position = new Vector3(10, transform.position.y, transform.position.z);
                oldSpeed = speed += 0.1f;
                speed = 0;
                groundImage.IncreaseXSpeed(oldSpeed-0.6f);
            }
        }
    }
}
