using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    // Objects
    // Main Menu Objects
    public GameObject MainMenuCanvas;
    // Main Lobby Objects
    public GameObject lobbyCanvas;
    public Button lobbyBackButton;
    public Button startGameButton;
    // "Create a Player" Objects
    public GameObject createPlayerScreen;
    public Button createPlayerButton;
    public Button createPlayerScreenCloseButton;
    public GameObject createPlayerScreenInput;
    public Button makePlayerButton;
    // "Player Select" Objects
    public Transform playersScrollContent;
    public GameObject playersScrollPanelPrefab;
    // "Selected Player" Objects
    public GameObject currentSelectedPlayerText;
    // "Game Settings" Objects
    public Button decreasePlayersButton;
    public Button increasePlayersButton;
    public Button previousMapButton;
    public Button nextMapButton;
    public GameObject playerAmount;
    public GameObject mapName;

    // Variables
    ArrayList playerList = new ArrayList();
    ArrayList playersPlaying = new ArrayList();
    ArrayList maps = new ArrayList();
    int currentPlayers = 2;
    int currentMap = 0;

    // Constants
    int MINPLAYERS = 2;
    int MAXPLAYERS = 2;
    int MAXMAPS = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Lobby Buttons
        lobbyBackButton.onClick.AddListener(delegate { lobbyBack(); });
        createPlayerButton.onClick.AddListener(delegate { createPlayerScreenOpen(); });
        createPlayerScreenCloseButton.onClick.AddListener(delegate { createPlayerScreenClose(); });
        makePlayerButton.onClick.AddListener(delegate { createPlayer(); });
        startGameButton.onClick.AddListener(delegate { StartGame(); });
        decreasePlayersButton.onClick.AddListener(delegate { decreasePlayers(); });
        increasePlayersButton.onClick.AddListener(delegate { increasePlayers(); });
        previousMapButton.onClick.AddListener(delegate { previousMap(); });
        nextMapButton.onClick.AddListener(delegate { nextMap(); });

        playerAmount.GetComponent<TextMeshProUGUI>().text = currentPlayers + "";
        mapName.GetComponent<TextMeshProUGUI>().text = "Select a Map";

        maps.Add("The Zoo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        lobbyCanvas.SetActive(false);
    }

    public void lobbyBack()
    {
        MainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
    }

    public void createPlayerScreenOpen()
    {
        createPlayerScreen.SetActive(true);
    }

    public void createPlayerScreenClose()
    {
        createPlayerScreen.SetActive(false);
    }

    // When the "Make Player" button is clicked, inputted name is added to the player list.
    public void createPlayer()
    {
        GameObject newPlayer = Instantiate(playersScrollPanelPrefab);
        newPlayer.SetActive(true);
        newPlayer.transform.SetParent(playersScrollContent);
        newPlayer.GetComponentInChildren<TextMeshProUGUI>().text = createPlayerScreenInput.GetComponentInChildren<TextMeshProUGUI>().text;
        createPlayerScreenInput.GetComponentInChildren<TextMeshProUGUI>().text = "";
        newPlayer.transform.localScale = Vector2.one;
        int currentButtonNum = playerList.Count;
        Button newPlayerButton = newPlayer.transform.Find("PlayersScrollPanelButton").gameObject.GetComponent<Button>();
        newPlayerButton.onClick.AddListener(delegate { showSelected(currentButtonNum); });
        newPlayerButton = newPlayer.transform.Find("PlayersScrollPanelToggle").gameObject.GetComponent<Button>();
        newPlayerButton.onClick.AddListener(delegate { toggleSelected(currentButtonNum); });
        playerList.Add(newPlayer);
        playersPlaying.Add(false);
    }

    // Shows the selected player's name and equipment.
    public void showSelected(int playerNumber)
    {
        currentSelectedPlayerText.GetComponent<TextMeshProUGUI>().text = ((GameObject) playerList[playerNumber]).GetComponentInChildren<TextMeshProUGUI>().text;
        // Debug.Log(playersPlaying[playerNumber]);
    }

    // Toggles the "X" button beside the player's name on the player list, which decides if the player is playing or not.
    public void toggleSelected(int playerNumber)
    {
        TextMeshProUGUI textBox = ((GameObject) playerList[playerNumber]).transform.Find("PlayersScrollPanelToggle").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textBox.text == "X")
        {
            textBox.text = "";
            playersPlaying[playerNumber] = false;
        } else
        {
            textBox.text = "X";
            playersPlaying[playerNumber] = true;
        }
    }

    // Decreases the number of players in the game.
    public void decreasePlayers()
    {
        if (currentPlayers > MINPLAYERS)
        {
            currentPlayers--;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = currentPlayers + "";
    }

    // Increases the number of players in the game.
    public void increasePlayers()
    {
        if (currentPlayers < MAXPLAYERS)
        {
            currentPlayers++;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = currentPlayers + "";
    }

    // Changes the map to the previous map.
    public void previousMap()
    {
        if (currentMap > 0)
        {
            currentMap--;
        }

        mapName.GetComponent<TextMeshProUGUI>().text = (string) maps[currentMap];
    }

    // Changes the map to the next map.
    public void nextMap()
    {
        if (currentMap < MAXMAPS-1)
        {
            currentMap++;
        }

        mapName.GetComponent<TextMeshProUGUI>().text = (string) maps[currentMap];
    }

    // Lobby TODO:
    // Add the inventory selector.
    // Clear all toggled players in the player list, and set all values in the "playersPlaying" list to false.
    // Make getters and setters for the player information and game settings information.
    // Limit the number of characters possible in a name.
    // Clear the "Created Player Input" bar whenever a new player is created.
    // Check that the number of players toggled in the "playersPlaying" list matches the number of players in the "Number of Players" game setting.
}
