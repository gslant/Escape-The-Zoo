using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// LobbyManager TODO:
// Add the "Save Player List" external method to startGame() 
// Add the "Save Player List" external method to Update() when the Lobby closes
// Delete Temporary "Player" class and use the external "Player" class
// Replace Start() Test sprites with actual sprites
// Replace Start() Test maps with actual maps
// Remove Start() Test list of Players

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
    private ArrayList playerList = new ArrayList(); // A list of "Player" instances.
    private ArrayList playersPlaying = new ArrayList(); // A boolean list describing of which players are playing the game.
    private ArrayList animals = new ArrayList(); // An array of animal images/files.
    private ArrayList accessories = new ArrayList(); // An array of accessory images/files.
    private int playersToPlayInGame = 0; // The number of players to play in the game.
    private int currentPlayers = 0; // The number of players selected to play in the game.
    private ArrayList maps = new ArrayList(); // An array of map names.
    private int currentMap = -1; // The map being played on. Default being -1.
    private int selectedPlayerNum = -1; // The current selected player. Default being -1.

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

        // Test sprites
        animals.Add(Resources.Load<Sprite>("CatFace"));
        animals.Add(Resources.Load<Sprite>("ChickenFace"));
        animals.Add(Resources.Load<Sprite>("FrogFace"));
        animals.Add(Resources.Load<Sprite>("PandaFace"));
        animals.Add(Resources.Load<Sprite>("SnakeFace"));
        accessories.Add(Resources.Load<Sprite>("AlienTinFoilHat"));
        accessories.Add(Resources.Load<Sprite>("BirdNest"));
        accessories.Add(Resources.Load<Sprite>("BlackTopHat"));
        accessories.Add(Resources.Load<Sprite>("BlackTopHat_v2"));
        accessories.Add(Resources.Load<Sprite>("BlueTopHat"));
        accessories.Add(Resources.Load<Sprite>("Crown"));
        accessories.Add(Resources.Load<Sprite>("HeartPaint"));
        accessories.Add(Resources.Load<Sprite>("PinkCrown"));
        accessories.Add(Resources.Load<Sprite>("StrawHat"));

        // Test maps
        maps.Add("The Zoo");

        // Test list of Players
        playerList.Add(new Player("Bob"));
        playerList.Add(new Player("Dave"));
        playerList.Add(new Player("Steve"));
        playerList.Add(new Player("Tyrone"));
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
            currentSelectedPlayerAnimal.GetComponent<Image>().sprite = null;
            currentSelectedPlayerAccessory.GetComponent<Image>().sprite = null;

            canvasEnabled = 1;
        } else if (lobbyCanvas.activeSelf == false && canvasEnabled == 1)
        { // Do something when the lobby closes
            // Destroy all the players in the player list, and resets the playersPlaying array
            foreach (Transform child in playersScrollContent)
            {
                if (child.gameObject.name == "PlayersScrollPanelPrefab(Clone)")
                {
                    Destroy(child.gameObject);
                }
            }

            playersPlaying = new ArrayList();

            canvasEnabled = 0;
        }
    }

    public void setPlayerList(ArrayList playerList)
    {
        this.playerList = playerList;
    }

    public ArrayList getListOfPlayersPlaying()
    {
        ArrayList returnArray = new ArrayList();

        for (int i = 0; i < this.playerList.Count; i++)
        {
            if ((bool) playersPlaying[i])
            {
                returnArray.Add(playerList[i]);
            }
        }

        return returnArray;
    }

    public string getMapToBePlayedOn()
    {
        return (string) maps[currentMap];
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

        lobbyCanvas.SetActive(false);

        // TODO: Use an external function to save the player list.
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
        createPlayerScreenInput.text = "";
        Player newPlayer = new Player(newPlayerName);
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
        newPlayerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.name;
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
        currentSelectedPlayerText.GetComponent<TextMeshProUGUI>().text = ((Player) playerList[playerNumber]).name;
        currentSelectedPlayerAnimal.GetComponent<Image>().sprite = (Sprite) animals[((Player) playerList[playerNumber]).animal];
        currentSelectedPlayerAccessory.GetComponent<Image>().sprite = (Sprite) accessories[((Player) playerList[playerNumber]).accessory];
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
    private void nextAnimal(int playerNumber)
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
    private void previousAccessory(int playerNumber)
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
    private void nextAccessory(int playerNumber)
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

        mapName.GetComponent<TextMeshProUGUI>().text = (string) maps[currentMap];
    }

    // Changes the map to the next map.
    private void nextMap()
    {
        if (currentMap < MAXMAPS-1)
        {
            currentMap++;
        }

        mapName.GetComponent<TextMeshProUGUI>().text = (string) maps[currentMap];
    }
}