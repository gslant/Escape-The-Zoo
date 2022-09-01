using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Lobby TODO:
// lobbyOpen()
// startGame()
// lobbyBack()
// Make getters and setters for the player information and game settings information.
// Make a pop-up funtion, that shows text on a pop-up message.

public class LobbyManager : MonoBehaviour
{
    // Temporary "Player" class
    public class Player
    {
        public string name;
        public int animal;
        public int accessory;

        public Player(string name)
        {
            this.name = name;
            this.animal = 0;
            this.accessory = 0;
        }
    }

    // Objects
    // Main Menu Objects
    public GameObject mainMenuCanvas;
    // Main Lobby Objects
    public GameObject lobbyCanvas;
    public Button lobbyBackButton;
    public Button startGameButton;
    // "Create a Player" Objects
    public GameObject createPlayerScreen;
    public Button createPlayerButton;
    public Button createPlayerScreenCloseButton;
    public TMP_InputField createPlayerScreenInput;
    public Button makePlayerButton;
    // "Player Select" Objects
    public Transform playersScrollContent;
    public GameObject playersScrollPanelPrefab;
    // "Selected Player" Objects
    public GameObject currentSelectedPlayerText;
    public GameObject currentSelectedPlayerAnimal;
    public Button selectedPlayerAnimalPreviousButton;
    public Button selectedPlayerAnimalNextButton;
    public Button selectedPlayerAccessoryPreviousButton;
    public Button selectedPlayerAccessoryNextButton;
    public GameObject currentSelectedPlayerAccessory;
    // "Game Settings" Objects
    public Button decreasePlayersButton;
    public Button increasePlayersButton;
    public Button previousMapButton;
    public Button nextMapButton;
    public GameObject playerAmount;
    public GameObject mapName;

    // Variables
    ArrayList playerList = new ArrayList(); // A list of "Player" instances
    ArrayList playersPlaying = new ArrayList(); // A boolean list describing of which players are playing the game.
    ArrayList animals = new ArrayList(); // An array of animal images/files.
    ArrayList accessories = new ArrayList(); // An array of accessory images/files.
    int playersToPlayInGame = 2; // The number of players to play in the game. Default being 2.
    int currentPlayers = 0; // The number of players selected to play in the game.
    ArrayList maps = new ArrayList(); // An array of map names.
    int currentMap = -1; // The map being played on. Default being -1.
    int selectedPlayerNum = -1;

    // Constants
    int MINPLAYERS = 1;
    int MAXPLAYERS = 3;
    int MAXMAPS = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Lobby Buttons
        lobbyBackButton.onClick.AddListener(delegate { lobbyBack(); });
        createPlayerButton.onClick.AddListener(delegate { createPlayerScreenOpen(); });
        createPlayerScreenCloseButton.onClick.AddListener(delegate { createPlayerScreenClose(); });
        makePlayerButton.onClick.AddListener(delegate { createPlayer(); });
        startGameButton.onClick.AddListener(delegate { startGame(); });
        selectedPlayerAnimalPreviousButton.onClick.AddListener(delegate { previousAnimal(selectedPlayerNum); });
        selectedPlayerAnimalNextButton.onClick.AddListener(delegate { nextAnimal(selectedPlayerNum); });
        selectedPlayerAccessoryPreviousButton.onClick.AddListener(delegate { previousAccessory(selectedPlayerNum); });
        selectedPlayerAccessoryNextButton.onClick.AddListener(delegate { nextAccessory(selectedPlayerNum); });
        decreasePlayersButton.onClick.AddListener(delegate { decreasePlayers(); });
        increasePlayersButton.onClick.AddListener(delegate { increasePlayers(); });
        previousMapButton.onClick.AddListener(delegate { previousMap(); });
        nextMapButton.onClick.AddListener(delegate { nextMap(); });

        createPlayerScreenInput.characterLimit = 10;

        // Setting Lobby to "Not active" on startup
        mainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
        createPlayerScreen.SetActive(false);

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        mapName.GetComponent<TextMeshProUGUI>().text = "Select a Map";

        // Test sprites
        animals.Add(Resources.Load<Sprite>("Animal_test_0"));
        animals.Add(Resources.Load<Sprite>("Animal_test_1"));
        accessories.Add(Resources.Load<Sprite>("Accessory_test_0"));
        accessories.Add(Resources.Load<Sprite>("Accessory_test_1"));

        // Test maps
        maps.Add("The Zoo");

        // Test list of Players
        playerList.Add(new Player("Bob"));
        playerList.Add(new Player("Dave"));
        playerList.Add(new Player("Steve"));
        playerList.Add(new Player("Tyrone"));

        // Test player list
        for (int i = 0; i < this.playerList.Count; i++)
        {
            insertPlayerIntoPlayerScroll((Player) this.playerList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Shows the Lobby and puts every player into the player scroll view when the Lobby is opened. Also resets the game settings.
    public void lobbyOpen(ArrayList playerList)
    {
        lobbyCanvas.SetActive(true);

        this.playerList = playerList;

        for (int i = playersScrollContent.transform.childCount - 1; i < this.playerList.Count; i++)
        {
            insertPlayerIntoPlayerScroll((Player) this.playerList[i]);
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        mapName.GetComponent<TextMeshProUGUI>().text = "Select a Map";
    }

    public void startGame()
    {
        if (currentPlayers != playersToPlayInGame)
        {
            Debug.Log("Incorrect number of players");
            return;
        }

        if (currentMap < 0)
        {
            Debug.Log("Incorrect map");
            return;
        }

        lobbyCanvas.SetActive(false);

        // Change the debug.log to a pop-up message in-game.
        // Use an external function to save the player list.
        // Use an external function to give a list of players playing, as well as the number of players playing.
        // Use an external function to give the value of the map to be played on.
    }

    public void lobbyBack()
    {
        mainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
        createPlayerScreenClose();

        // Use an external function to save the player list
    }

    public void createPlayerScreenOpen()
    {
        createPlayerScreen.SetActive(true);
    }

    public void createPlayerScreenClose()
    {
        createPlayerScreen.SetActive(false);
        createPlayerScreenInput.text = "";
    }

    // When the "Make Player" button is clicked, a new instance of "Player" is created and added to playerList, then added to the player scroll.
    public void createPlayer()
    {
        string newPlayerName = createPlayerScreenInput.text;
        createPlayerScreenInput.text = "";
        Player newPlayer = new Player(newPlayerName);
        playerList.Add(newPlayer);
        insertPlayerIntoPlayerScroll(newPlayer);
    }

    // Inserts a player into the player scroll.
    public void insertPlayerIntoPlayerScroll(Player player)
    {
        GameObject newPlayerPanel = Instantiate(playersScrollPanelPrefab);
        int currentPlayerPanelNum = playersScrollContent.transform.childCount - 1;
        newPlayerPanel.SetActive(true);
        newPlayerPanel.transform.SetParent(playersScrollContent);
        newPlayerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.name;
        newPlayerPanel.transform.localScale = Vector2.one;
        Button newPlayerButton = newPlayerPanel.transform.Find("PlayersScrollPanelButton").gameObject.GetComponent<Button>();
        newPlayerButton.onClick.AddListener(delegate { showSelectedPlayer(currentPlayerPanelNum); });
        Button newPlayerToggle = newPlayerPanel.transform.Find("PlayersScrollPanelToggle").gameObject.GetComponent<Button>();
        newPlayerToggle.onClick.AddListener(delegate { toggleSelected(currentPlayerPanelNum); });
        playersPlaying.Add(false);
    }

    // Shows the selected player's name, animal, and equipment.
    public void showSelectedPlayer(int playerNumber)
    {
        selectedPlayerNum = playerNumber;
        currentSelectedPlayerText.GetComponent<TextMeshProUGUI>().text = ((Player) playerList[playerNumber]).name;
        currentSelectedPlayerAnimal.GetComponent<Image>().sprite = (Sprite) animals[((Player) playerList[playerNumber]).animal];
        currentSelectedPlayerAccessory.GetComponent<Image>().sprite = (Sprite) accessories[((Player) playerList[playerNumber]).accessory];
    }

    // Toggles the "X" button beside the player's name on the player list, which decides if the player is playing or not.
    public void toggleSelected(int playerNumber)
    {
        TextMeshProUGUI textBox = playersScrollContent.GetChild(playerNumber + 1).transform.Find("PlayersScrollPanelToggle").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textBox.text == "X")
        {
            textBox.text = "";
            playersPlaying[playerNumber] = false;
            currentPlayers--;
        } else
        {
            textBox.text = "X";
            playersPlaying[playerNumber] = true;
            currentPlayers++;
        }
    }

    // Changes the player's animal to the previous animal.
    public void previousAnimal(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        if (((Player) playerList[playerNumber]).animal == 0)
        {
            ((Player) playerList[playerNumber]).animal = animals.Count - 1;
        } else
        {
            ((Player) playerList[playerNumber]).animal -= 1;
        }

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's animal to the next animal.
    public void nextAnimal(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        if (((Player) playerList[playerNumber]).animal == (animals.Count - 1))
        {
            ((Player) playerList[playerNumber]).animal = 0;
        }
        else
        {
            ((Player) playerList[playerNumber]).animal += 1;
        }

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's accessory to the previous accessory.
    public void previousAccessory(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        if (((Player) playerList[playerNumber]).accessory == 0)
        {
            ((Player) playerList[playerNumber]).accessory = accessories.Count - 1;
        }
        else
        {
            ((Player) playerList[playerNumber]).accessory -= 1;
        }

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's accessory to the next accessory.
    public void nextAccessory(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        if (((Player) playerList[playerNumber]).accessory == (accessories.Count - 1))
        {
            ((Player) playerList[playerNumber]).accessory = 0;
        }
        else
        {
            ((Player) playerList[playerNumber]).accessory += 1;
        }

        showSelectedPlayer(playerNumber);
    }

    // Clears all the toggled buttons in the player scroll view;
    public void refreshPlayerListToggles()
    {
        for (int i = 0; i < playersScrollContent.transform.childCount - 1; i++)
        {
            TextMeshProUGUI textBox = playersScrollContent.GetChild(i + 1).transform.Find("PlayersScrollPanelToggle").gameObject.GetComponentInChildren<TextMeshProUGUI>();
            textBox.text = "";
            playersPlaying[i] = false;
        }

        currentPlayers = 0;
    }

    // Decreases the number of players in the game.
    public void decreasePlayers()
    {
        if (playersToPlayInGame > MINPLAYERS)
        {
            playersToPlayInGame--;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        refreshPlayerListToggles();
    }

    // Increases the number of players in the game.
    public void increasePlayers()
    {
        if (playersToPlayInGame < MAXPLAYERS)
        {
            playersToPlayInGame++;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        refreshPlayerListToggles();
    }

    // Changes the map to the previous map.
    public void previousMap()
    {
        if (currentMap > 0)
        {
            currentMap--;
        } else if (currentMap < 0) {
            currentMap = 0;
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
}
