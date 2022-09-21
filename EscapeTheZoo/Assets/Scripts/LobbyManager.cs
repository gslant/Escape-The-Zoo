using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DataManager;

public class LobbyManager : MonoBehaviour
{
    // Objects
    // Main Menu Objects
    public GameObject mainMenuCanvas;
    // Main Lobby Objects
    public GameObject lobbyCanvas;
    public Button lobbyBackButton;
    public Button startGameButton;
    // "Popup Message" Objects;
    public Button popupBox;
    public GameObject popupMessage;
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
    private int canvasEnabled = 0; // Keeps track of the state of lobby canvas.
    private List<Player> playerList = new List<Player>(); // A list of "Player" instances.
    private List<bool> playersPlaying = new List<bool>(); // A boolean list describing of which players are playing the game.
    private List<string> animals = new List<string>(); // An array of animal images/files.
    private List<string> accessories = new List<string>(); // An array of accessory images/files.
    private int playersToPlayInGame = 0; // The number of players to play in the game.
    private int currentPlayers = 0; // The number of players selected to play in the game.
    private List<string> maps = new List<string>(); // An array of map names.
    private int currentMap = -1; // The map being played on. Default being -1.
    private int selectedPlayerNum = -1; // The current selected player. Default being -1.
    private DataManager dataManager;
    public static List<Player> listOfPlayersPlaying; // This will be accessed from the GameBoard scene to load in the players

    // Constants
    private int MINPLAYERS = 2;
    private int MAXPLAYERS = 2;
    private int MAXMAPS = 1;
    private int PLAYERNAMECHARLIMIT = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Lobby Buttons
        lobbyBackButton.onClick.AddListener(delegate { lobbyBack(); });
        popupBox.onClick.AddListener(delegate { closePopup(); });
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

        // Player name character limit
        createPlayerScreenInput.characterLimit = PLAYERNAMECHARLIMIT;

        // Setting Lobby to "Not active" on startup
        mainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
        createPlayerScreen.SetActive(false);
        closePopup();

        // Sprites
        animals.Add("CatFace");
        animals.Add("ChickenFace");
        animals.Add("FrogFace");
        animals.Add("PandaFace");
        animals.Add("SnakeFace");
        accessories.Add("None");
        accessories.Add("HeartFacePaint");
        accessories.Add("TinFoilHat");
        accessories.Add("BirdNest");
        accessories.Add("StrawHat");
        accessories.Add("BlackTopHat");
        accessories.Add("BlueTopHat");
        accessories.Add("PinkCrown");
        accessories.Add("Crown");

        // Maps
        maps.Add("The Zoo");

        // Load in the player profiles
        dataManager = GetComponent<DataManager>();
 
        setPlayerList(dataManager.LoadData("profiles.txt"));
    }

    // Update is called once per frame
    void Update()
    {
        // Determines if the lobbyCanvas is active, and executes code depending on the circumstances
        if (lobbyCanvas.activeSelf == true && canvasEnabled == 0)
        { // Do something when the lobby opens
            // Loads in the player list
            for (int i = 0; i < this.playerList.Count; i++)
            {
                insertPlayerIntoPlayerScroll((Player)this.playerList[i]);
            }

            // Resets settings
            playersToPlayInGame = MINPLAYERS;
            playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";

            currentMap = -1;
            mapName.GetComponent<TextMeshProUGUI>().text = "Select a Map";

            selectedPlayerNum = -1;
            currentSelectedPlayerText.GetComponent<TextMeshProUGUI>().text = "Player Name";
            currentSelectedPlayerAnimal.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/None");
            currentSelectedPlayerAccessory.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/None"); ;

            canvasEnabled = 1;
        } else if (lobbyCanvas.activeSelf == false && canvasEnabled == 1)
        { // Do something when the lobby closes
            savePlayerList();

            // Destroy all the players in the player list, and resets the playersPlaying array
            foreach (Transform child in playersScrollContent)
            {
                if (child.gameObject.name == "PlayersScrollPanelPrefab(Clone)")
                {
                    Destroy(child.gameObject);
                }
            }

            playersPlaying = new List<bool>();

            canvasEnabled = 0;
        }
    }

    public void setPlayerList(List<Player> playerList)
    {
        this.playerList = playerList;
    }

    public void savePlayerList()
    {
        foreach (Player player in playerList)
        {
            dataManager.SaveData(player);
        }
    }

    public List<Player> getListOfPlayersPlaying()
    {
        return listOfPlayersPlaying;
    }

    private void setListOfPlayersPlaying()
    {
        listOfPlayersPlaying = new List<Player>();

        for (int i = 0; i < this.playerList.Count; i++)
        {
            if (playersPlaying[i])
            {
                listOfPlayersPlaying.Add(playerList[i]);
            }
        }
    }

    public string getMapToBePlayedOn()
    {
        return maps[currentMap];
    }

    private void startGame()
    {
        if (currentPlayers != playersToPlayInGame)
        {
            popup("Incorrect number of players selected");
            return;
        }

        if (currentMap < 0)
        {
            popup("Incorrect map");
            return;
        }

        savePlayerList();

        setListOfPlayersPlaying();

        lobbyCanvas.SetActive(false);
        SceneLoader.LoadScene("GameBoard");
    }

    private void lobbyBack()
    {
        mainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
        closePopup();
        createPlayerScreenClose();
    }

    private void popup(string message)
    {
        popupBox.gameObject.SetActive(true);
        popupMessage.GetComponent<TextMeshProUGUI>().text = message;
    }

    private void closePopup()
    {
        popupBox.gameObject.SetActive(false);
    }

    private void createPlayerScreenOpen()
    {
        createPlayerScreen.SetActive(true);
    }

    private void createPlayerScreenClose()
    {
        createPlayerScreen.SetActive(false);
        createPlayerScreenInput.text = "";
    }

    // When the "Make Player" button is clicked, a new instance of "Player" is created and added to playerList, then added to the player scroll.
    private void createPlayer()
    {
        string newPlayerName = createPlayerScreenInput.text;

        foreach (Player player in playerList)
        {
            if (newPlayerName == player.getName())
            {
                popup("Sorry. That name is already taken.");

                return;
            }
        }

        createPlayerScreenInput.text = "";
        List<string> newAccessoriesList = new List<string>();
        newAccessoriesList.Add("None");
        Player newPlayer = new Player(newPlayerName, 0, "CatFace", "None", newAccessoriesList);
        playerList.Add(newPlayer);
        insertPlayerIntoPlayerScroll(newPlayer);
    }

    // Inserts a player into the player scroll.
    private void insertPlayerIntoPlayerScroll(Player player)
    {
        GameObject newPlayerPanel = Instantiate(playersScrollPanelPrefab);
        int currentPlayerPanelNum = playersScrollContent.transform.childCount - 1;
        newPlayerPanel.SetActive(true);
        newPlayerPanel.transform.SetParent(playersScrollContent);
        newPlayerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.getName();
        newPlayerPanel.transform.localScale = Vector2.one;
        Button newPlayerButton = newPlayerPanel.transform.Find("PlayersScrollPanelButton").gameObject.GetComponent<Button>();
        newPlayerButton.onClick.AddListener(delegate { showSelectedPlayer(currentPlayerPanelNum); });
        Button newPlayerToggle = newPlayerPanel.transform.Find("PlayersScrollPanelToggle").gameObject.GetComponent<Button>();
        newPlayerToggle.onClick.AddListener(delegate { toggleSelected(currentPlayerPanelNum); });
        playersPlaying.Add(false);
    }

    // Shows the selected player's name, animal, and equipment.
    private void showSelectedPlayer(int playerNumber)
    {
        selectedPlayerNum = playerNumber;
        currentSelectedPlayerText.GetComponent<TextMeshProUGUI>().text = playerList[playerNumber].getName();
        currentSelectedPlayerAnimal.GetComponent<Image>().sprite = Resources.Load<Sprite>("Character Face Sprites/"+playerList[playerNumber].getAnimal());
        currentSelectedPlayerAccessory.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/" + playerList[playerNumber].getAccessory());
    }

    // Toggles the "X" button beside the player's name on the player list, which decides if the player is playing or not.
    private void toggleSelected(int playerNumber)
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
    private void previousAnimal(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        int animalArrayCurrentIndex = animals.IndexOf(playerList[playerNumber].getAnimal());

        if (animalArrayCurrentIndex == 0)
        {
            playerList[playerNumber].setAnimal(animals[animals.Count - 1]);
        } else
        {
            playerList[playerNumber].setAnimal(animals[animalArrayCurrentIndex - 1]);
        }

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's animal to the next animal.
    private void nextAnimal(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        int animalArrayCurrentIndex = animals.IndexOf(playerList[playerNumber].getAnimal());

        if (animalArrayCurrentIndex == animals.Count - 1)
        {
            playerList[playerNumber].setAnimal(animals[0]);
        }
        else
        {
            playerList[playerNumber].setAnimal(animals[animalArrayCurrentIndex + 1]);
        }

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's accessory to the previous accessory.
    private void previousAccessory(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        List<string> playerCosmeticsList = playerList[playerNumber].getOwnedCosmetics();
        int accessoryArrayCurrentIndex = playerCosmeticsList.IndexOf(playerList[playerNumber].getAccessory());

        if (!playerList[playerNumber].getAccessory().Equals("None"))
        {
            if (accessoryArrayCurrentIndex == 0)
            {
                playerList[playerNumber].setAccessory("None");
            }
            else
            {
                playerList[playerNumber].setAccessory(playerCosmeticsList[accessoryArrayCurrentIndex - 1]);
            }
        }
        

        showSelectedPlayer(playerNumber);
    }

    // Changes the player's accessory to the next accessory.
    private void nextAccessory(int playerNumber)
    {
        if (playerNumber == -1)
        {
            return;
        }

        List<string> playerCosmeticsList = playerList[playerNumber].getOwnedCosmetics();
        int accessoryArrayCurrentIndex = playerCosmeticsList.IndexOf(playerList[playerNumber].getAccessory());

        if (accessoryArrayCurrentIndex == playerCosmeticsList.Count - 1)
        {
            playerList[playerNumber].setAccessory("None");
        }
        else
        {
            playerList[playerNumber].setAccessory(playerCosmeticsList[accessoryArrayCurrentIndex + 1]);
        }

        showSelectedPlayer(playerNumber);
    }

    // Clears all the toggled buttons in the player scroll view;
    private void refreshPlayerListToggles()
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
    private void decreasePlayers()
    {
        if (playersToPlayInGame > MINPLAYERS)
        {
            playersToPlayInGame--;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        refreshPlayerListToggles();
    }

    // Increases the number of players in the game.
    private void increasePlayers()
    {
        if (playersToPlayInGame < MAXPLAYERS)
        {
            playersToPlayInGame++;
        }

        playerAmount.GetComponent<TextMeshProUGUI>().text = playersToPlayInGame + "";
        refreshPlayerListToggles();
    }

    // Changes the map to the previous map.
    private void previousMap()
    {
        if (currentMap > 0)
        {
            currentMap--;
        } else if (currentMap < 0) {
            currentMap = 0;
        }

        mapName.GetComponent<TextMeshProUGUI>().text = maps[currentMap];
    }

    // Changes the map to the next map.
    private void nextMap()
    {
        if (currentMap < MAXMAPS-1)
        {
            currentMap++;
        }

        mapName.GetComponent<TextMeshProUGUI>().text = maps[currentMap];
    }
}
