using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DWTHController : MonoBehaviour
{
    // Objects
    public GameObject GameOverCanvas;
    public Button goBackButton;
    public TextMeshProUGUI gameOverText;
    public GameObject warning;
    public GameObject silhouette;
    public GameObject pushableObjectPrefab;
    public Transform pushableObjectList;
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;

    // Players
    public DWTHPlayerController player1;
    public DWTHPlayerController player2;

    // Variables
    private List<string> pushableObjectSprites = new List<string>();
    public static int numObjects = 0; // Keeps track of the number of objects pushed off the table
    private bool addedPushableObject = true; // Makes sure only 1 pushable object is added at a time. Starts off at "True" so that the first pushable object isn't spawned instantly when the game starts
    private int warningDuration = 3; // How long the warning sign stays for
    private int silhouettePauseDuration = 10; // Time between each moment the human shows up. Initial time is 10
    private int nextShowSilhouetteTime; // When the silhouette is shown
    private int nextHideSilhouetteTime; // When the silhouette is hidden
    public static int numberOfPlayersRemaining = 2; // Keeps track of the number of players still playing the game. Starts off at 2
    private bool gameOver = false; // Keeps track of if the game is over
    private int winner = -1; // Holds the winner of the game. Initially -1 as no winner is set

    // Constants
    private int MAX_NUM_OBJECTS = 5; // Maximum number of pushable objects
    private int SPAWN_INTERVAL_BETWEEN_EACH_OBJECT = 6; // Time between when each object spawns
    private int HUMAN_SILHOUETTE_DURATION = 3; // Duration the human silhouette is shown for
    private int WINNER_REWARD = 5; // The number of bonus coins the winner earns
    private int SILHOUETTE_PAUSE_DURATION_DECREASE = 1; // How much the silhouette pause duration will decrease by
    private int WARNING_DURATION_DECREASE = 1; // How much the warning duration will decrease by
    private int MINIMUM_SILHOUETTE_PAUSE_DURATION = 1; // Minimum silhouette pause duration
    private int MINIMUM_WARNING_DURATION = 1; // Minimum warning duration

    void Start()
    {
        // Hides certain game objects
        GameOverCanvas.SetActive(false);
        silhouette.SetActive(false);
        warning.SetActive(false);

        // Sets listeners
        goBackButton.onClick.AddListener(delegate { goBack(); });

        // Sets the next shown sihouette time
        nextShowSilhouetteTime = (int)Time.fixedTime + silhouettePauseDuration;
        nextHideSilhouetteTime = nextShowSilhouetteTime + HUMAN_SILHOUETTE_DURATION;

        // Add the pushable object sprites to the list. Current sprites are placeholders
        pushableObjectSprites.Add("DiamondRing");
        pushableObjectSprites.Add("Diamond");
        pushableObjectSprites.Add("Clock");
    }

    void Update()
    {
        if (!gameOver) // While the game isn't finished
        {
            // Updates the score counter
            player1Score.text = "Player 1 Score: " + player1.numPushed;
            player2Score.text = "Player 2 Score: " + player2.numPushed;

            // Makes a new pushable object every 6 seconds
            if (((int)Time.fixedTime % SPAWN_INTERVAL_BETWEEN_EACH_OBJECT) == 0 && numObjects < MAX_NUM_OBJECTS && addedPushableObject == false)
            {
                GameObject newPushableObject = Instantiate(pushableObjectPrefab);
                newPushableObject.SetActive(true);
                newPushableObject.transform.SetParent(pushableObjectList);
                newPushableObject.transform.localScale = Vector2.one;
                newPushableObject.transform.localPosition = new Vector3(Random.Range(-4.0f, -7.5f), Random.Range(0.0f, 4.0f), 0);
                newPushableObject.transform.Rotate(0, 0, Random.Range(0.0f, 360.0f));
                newPushableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DWTHSprites/" + pushableObjectSprites[Random.Range(0, pushableObjectSprites.Count)]);

                numObjects++;
                addedPushableObject = true;
            }
            else if ((int)Time.fixedTime % SPAWN_INTERVAL_BETWEEN_EACH_OBJECT != 0)
            {
                addedPushableObject = false;
            }

            // Shows the warning sign
            if ((int)Time.fixedTime == nextShowSilhouetteTime - warningDuration)
            {
                warning.SetActive(true);
            }

            // Shows the silhouette
            if ((int)Time.fixedTime == nextShowSilhouetteTime)
            {
                nextShowSilhouetteTime = (int)Time.fixedTime + silhouettePauseDuration;
                nextHideSilhouetteTime = (int)Time.fixedTime + HUMAN_SILHOUETTE_DURATION;

                silhouette.SetActive(true);
                warning.SetActive(false);

                if (silhouettePauseDuration > MINIMUM_SILHOUETTE_PAUSE_DURATION + HUMAN_SILHOUETTE_DURATION)
                {
                    silhouettePauseDuration -= SILHOUETTE_PAUSE_DURATION_DECREASE;
                }

                if (warningDuration > MINIMUM_WARNING_DURATION)
                {
                    warningDuration -= WARNING_DURATION_DECREASE;
                }
            }

            // Hide the silhouette
            if ((int)Time.fixedTime == nextHideSilhouetteTime)
            {
                silhouette.SetActive(false);
            }

            // If the game is over
            if (numberOfPlayersRemaining <= 1)
            {
                if (player1.alive)
                {
                    winner = 0;
                }
                else if (player2.alive)
                {
                    winner = 1;
                }

                List<Player> playersPlaying = MinigameLoadPlayers.GetListOfPlayersPlaying();

                GameOverCanvas.SetActive(true);
                gameOverText.text = (winner == -1 ? "No one wins!" : playersPlaying[winner].getName() + " Wins!");

                playersPlaying[0].changeGameBalanceByAmount(player1.numPushed);
                playersPlaying[1].changeGameBalanceByAmount(player2.numPushed);

                if (winner != -1)
                {
                    playersPlaying[winner].changeGameBalanceByAmount(WINNER_REWARD);
                }

                gameOver = true;
            }
        }
    }

    // Functionality for the "goBackButton"
    private void goBack()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("Don'tWakeTheHumans");
    }
}
