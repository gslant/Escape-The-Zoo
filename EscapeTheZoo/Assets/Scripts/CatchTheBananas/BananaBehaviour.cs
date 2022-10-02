using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // For now the banana gets destroyed...
            Destroy(gameObject);
        }
    }
}
