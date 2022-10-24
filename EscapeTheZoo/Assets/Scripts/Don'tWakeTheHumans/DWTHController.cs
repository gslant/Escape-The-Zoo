using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

public class DWTHController : MonoBehaviour
{
    // Sounds
    public AudioSource DWTHMusic;

    // Objects
    public GameObject GameOverCanvas;
//    public TextMeshProUGUI gameOverText;
    public GameObject warning;
    public GameObject silhouette;
    public GameObject pushableObjectPrefab;
    public Transform pushableObjectList;
//    public TextMeshProUGUI player1Score;
//    public TextMeshProUGUI player2Score;
    private GameObject latestPushableObject; // The newest created pushable object

    // Components
    private BoxCollider2D newPushableBoxCollider; // The newest created pushable object's box collider

    // Players
    public DWTHPlayerController player1;
    public DWTHPlayerController player2;

    // Variables
    private List<string> pushableObjectSprites = new List<string>(); // List of all the different pushable object sprites.
    public static int numObjects = 0; // Keeps track of the number of objects on the table.
    public static int numberOfPlayersRemaining = 2; // Keeps track of the number of players still playing the game. Starts off at 2.
    private bool addedPushableObject = false; // Makes sure only 1 pushable object is added at a time.
    private int warningDuration = 3; // How long the warning sign stays for. Initial duration is 3.
    private int silhouettePauseDuration = 10; // Time between each moment the human shows up. Initial duration is 10.
    private int nextShowSilhouetteTime; // When the silhouette is shown.
    private int nextHideSilhouetteTime; // When the silhouette is hidden.
    private bool gameOver = false; // Keeps track of if the game is over.
    //private int winner = -1; // Holds the winner of the game. Initially -1 as no winner is set.
    private float latestPushableObjectScale; // The scale of the newest created pushable object.

    // Constants
    private int MAX_NUM_OBJECTS = 5; // Maximum number of pushable objects.
    private int SPAWN_INTERVAL_BETWEEN_EACH_OBJECT = 6; // Time between when each object spawns.
    private int HUMAN_SILHOUETTE_DURATION = 3; // Duration the human silhouette is shown for.
//    private int WINNER_REWARD = 5; // The number of bonus coins the winner earns.
    private int SILHOUETTE_PAUSE_DURATION_DECREASE = 1; // How much the silhouette pause duration will decrease by.
    private int WARNING_DURATION_DECREASE = 1; // How much the warning duration will decrease by.
    private int MINIMUM_SILHOUETTE_PAUSE_DURATION = 1; // Minimum silhouette pause duration.
    private int MINIMUM_WARNING_DURATION = 1; // Minimum warning duration.

    // Start is called before the first frame update
    void Start()
    {
        // Hides certain game objects
        GameOverCanvas.SetActive(false);
        silhouette.SetActive(false);
        warning.SetActive(false);

        // Sets the next shown and hidden sihouette times
        nextShowSilhouetteTime = (int)Time.fixedTime + silhouettePauseDuration;
        nextHideSilhouetteTime = nextShowSilhouetteTime + HUMAN_SILHOUETTE_DURATION;

        // Destroys every object in the pushable object list
        foreach (Transform child in pushableObjectList)
        {
            Destroy(child.gameObject);
        }

        DWTHMusic.mute = false;

        numObjects = 0; // Keeps track of the number of objects on the table.
        numberOfPlayersRemaining = 2; // Keeps track of the number of players still playing the game. Starts off at 2.
        addedPushableObject = false; // Makes sure only 1 pushable object is added at a time.
        warningDuration = 3; // How long the warning sign stays for. Initial duration is 3.
        silhouettePauseDuration = 10; // Time between each moment the human shows up. Initial duration is 10.
        gameOver = false; // Keeps track of if the game is over.
        //winner = -1; // Holds the winner of the game. Initially -1 as no winner is set.

        // Hides certain game objects
        GameOverCanvas.SetActive(false);
        silhouette.SetActive(false);
        warning.SetActive(false);

        // Add the pushable object sprites to the list. Current sprites are placeholders
        pushableObjectSprites.Add("DiamondRing");
        pushableObjectSprites.Add("Diamond");
        pushableObjectSprites.Add("Clock");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) // While the game isn't finished
        {
            // Updates the score counter
//            player1Score.text = "Player 1 Score: " + player1.numPushed;
//            player2Score.text = "Player 2 Score: " + player2.numPushed;

            // Enlarges the newly spawned pushable object until it reaches its proper size
            if (latestPushableObjectScale < 1.0f && latestPushableObject != null)
            {
                latestPushableObjectScale += 3.0f * Time.deltaTime;
                latestPushableObject.transform.localScale = new Vector2(latestPushableObjectScale, latestPushableObjectScale);
                newPushableBoxCollider.edgeRadius += 1.8f * Time.deltaTime;
            }

            // Makes a new pushable object every 6 seconds
            if (((int)Time.fixedTime % SPAWN_INTERVAL_BETWEEN_EACH_OBJECT) == 0 && numObjects < MAX_NUM_OBJECTS && addedPushableObject == false)
            {
                createNewPushableObject();
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

            // Shows the silhouette, hides the warning, and updates the next warning and silhouette times
            if ((int)Time.fixedTime == nextShowSilhouetteTime)
            {
                silhouette.SetActive(true);
                warning.SetActive(false);
                updateSilhouetteTimes();
            }

            // Hides the silhouette
            if ((int)Time.fixedTime == nextHideSilhouetteTime)
            {
                silhouette.SetActive(false);
            }

            // If the game is over
            if (numberOfPlayersRemaining <= 1)
            {
                gameOverEvent();
            }
        }
    }

    // Creates a new pushable object
    private void createNewPushableObject()
    {
        GameObject newPushableObject = Instantiate(pushableObjectPrefab);
        newPushableObject.SetActive(true);
        newPushableObject.transform.SetParent(pushableObjectList);
        newPushableObject.transform.localScale = new Vector2(0, 0);
        float x, y;
        do
        { // Sets the pushable object spawn point to within a certain range
            x = Random.Range(0.0f, 8.0f);
            y = Random.Range(0.0f, 8.0f);
        } while (x + y >= 8.0f);
        y *= -1; // Flips the Y axis
        newPushableObject.transform.localPosition = new Vector3(x, y, 0);
        newPushableObject.transform.Rotate(0, 0, Random.Range(0.0f, 360.0f));
        newPushableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DWTHSprites/" + pushableObjectSprites[Random.Range(0, pushableObjectSprites.Count)]);
        newPushableBoxCollider = newPushableObject.GetComponent<BoxCollider2D>();
        newPushableBoxCollider.edgeRadius = 0.0f;
        latestPushableObjectScale = 0.0f;
        latestPushableObject = newPushableObject;

        numObjects++;
        addedPushableObject = true;
    }

    // Updates the silhouette times
    private void updateSilhouetteTimes()
    {
        nextShowSilhouetteTime = (int)Time.fixedTime + silhouettePauseDuration;
        nextHideSilhouetteTime = (int)Time.fixedTime + HUMAN_SILHOUETTE_DURATION;

        if (silhouettePauseDuration > MINIMUM_SILHOUETTE_PAUSE_DURATION + HUMAN_SILHOUETTE_DURATION)
        {
            silhouettePauseDuration -= SILHOUETTE_PAUSE_DURATION_DECREASE;
        }

        if (warningDuration > MINIMUM_WARNING_DURATION)
        {
            warningDuration -= WARNING_DURATION_DECREASE;
        }
    }

    // Actions taken when the game is over
    private void gameOverEvent()
    {
        DWTHMusic.mute = true;

        /*if (player1.alive)
        {
            winner = 0;
        }
        else if (player2.alive)
        {
            winner = 1;
        }*/

//        List<Player> playersPlaying = MinigameLoadPlayers.GetListOfPlayersPlaying();

        GameOverCanvas.SetActive(true);
//        gameOverText.text = (winner == -1 ? "No one wins!" : playersPlaying[winner].getName() + " Wins!");

/*        playersPlaying[0].changeGameBalanceByAmount(player1.numPushed);
        playersPlaying[1].changeGameBalanceByAmount(player2.numPushed);

        if (winner != -1)
        {
            playersPlaying[winner].changeGameBalanceByAmount(WINNER_REWARD);
        }*/

        gameOver = true;
        //StartCoroutine(GameOverPause());
    }

/*    IEnumerator GameOverPause()
    {
        // Pause game for 2 seconds
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        GoBackToGameBoard();
    }*/

/*    private void GoBackToGameBoard()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("Don'tWakeTheHumans");
    }*/
}
