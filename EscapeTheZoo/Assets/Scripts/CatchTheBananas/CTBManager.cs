using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CTBManager : MonoBehaviour
{
    // Manages spawning of bananas and keeping track of the players' count

    [SerializeField] GameObject bananaPrefab;
    [SerializeField] TMP_Text player1ScoreText, player2ScoreText;

    public int Player1Score { get; set; }
    public int Player2Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerScoreTexts();
        StartSpawningBananas();
    }

    void StartSpawningBananas()
    {
        StartCoroutine(TimeSpawn());
    }

    IEnumerator TimeSpawn()
    {
        float timeInterval = 4f;
        float bananaGravity = bananaPrefab.GetComponent<Rigidbody2D>().gravityScale;

        while (true)
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
        GameObject banana = Instantiate(prefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.identity);
        banana.SetActive(true);
        return banana;
    }

    public void IncrementScore(string playerName)
    {
        if (playerName == "Player1")
        {
            Player1Score++;
        }
        if (playerName == "Player2")
        {
            Player2Score++;
        }

        UpdatePlayerScoreTexts();
    }

    void UpdatePlayerScoreTexts()
    {
        player1ScoreText.text = "<size=80%>(Player 1)</size>\nScore: " + Player1Score;
        player2ScoreText.text = "<size=80%>(Player 2)</size>\nScore: " + Player2Score;
    }
}
