using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(BecomeDangerousBanana());
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CatchTheBananas/CTB_BananaPeeled");
        }
    }

    // this is my favourite name ive given to a method so far
    IEnumerator BecomeDangerousBanana()
    {
        gameObject.tag = "Enemy";
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}

