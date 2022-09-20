using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManger : MonoBehaviour
{
    public int coinDisplay;
    public TMP_Text cointxt;
    public ShopItemScript[] shopItemScripts;
    public GameObject[] gameObjects;
    public ShopTemplete[] itemPanel;

    // Player GameObjects
    public Transform playersScrollContent;
    public GameObject playerScrollPanelPrefab; // Prefab reference to instantiate for every player in the loaded data
    public GameObject currentSelectedPlayerName; // Holds the name of the selected player
    public GameObject currentSelectedPlayerAnimal; // Holds the animal of the selected player
    public GameObject previewAcessoryImage;

    // Player profiles
    private List<Player> playerList = new List<Player>(); // A list of "Player" instances.
    private int selectedPlayerNum = -1; // The current selected player. Default being -1.
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        // Make the item prefabs visible
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }

        CreatePanelItem();
        CheckIfBuyable();

        // Disable preview feature
        previewAcessoryImage.SetActive(false);

        LoadPlayerProfilesData();
        LoadPlayerList();
    }

    private void LoadPlayerProfilesData()
    {
        // Load data
        dataManager = GetComponent<DataManager>();
        // Assign the data to the player list
        playerList = dataManager.LoadData("profiles.txt");
    }

    private void LoadPlayerList()
    {
        for (int i = 0; i < this.playerList.Count; i++)
        {
            InsertPlayerIntoPlayerScroll((Player)playerList[i]);
        }
        currentSelectedPlayerAnimal.GetComponent<Image>().sprite = Resources.Load<Sprite>("None");
    }

    // The ability to preview items is enabled when there is a player selection
    public void EnablePreview()
    {
        previewAcessoryImage.SetActive(true);
    }

    // Updates each item panel content with each of the shop item script content
    public void CreatePanelItem()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            itemPanel[i].titleTxt.text = shopItemScripts[i].title;
            itemPanel[i].descriptiontxt.text = shopItemScripts[i].description;
            itemPanel[i].cosmeticImg.GetComponent<Image>().sprite = shopItemScripts[i].cosmeticImage;
            itemPanel[i].costtxt.text = "Coins: " + shopItemScripts[i].cost.ToString();
        }
    }

    // Updates the coin displayed text with the selected player profile data
    public void UpdateCoinTotal(int playerNumber)
    {
        coinDisplay = playerList[playerNumber].balance;
        cointxt.text = "Coins: " + coinDisplay.ToString();

        CheckIfBuyable();
    }

    // Disables buy button for items that cost more than the player's coins or are already owned or if the item is already in the player's accessory list
    public void CheckIfBuyable()
    {
        if (selectedPlayerNum >= 0 && playerList[selectedPlayerNum].getOwnedCosmetics().Count > 0) // Given a player has been selected and has cosmetic items
        {
            BuyableHelper();
            foreach (string cosmetic in playerList[selectedPlayerNum].getOwnedCosmetics())
            {
                for (int i = 0; i < shopItemScripts.Length; i++)
                {
                    Button btn = gameObjects[i].GetComponentInChildren<Button>();

                    if (cosmetic.Equals(shopItemScripts[i].title.Replace(" ", string.Empty)))
                    {
                        ChangeButtonInteractionAndText(btn, false, "Owned!", 68);
                    }
                }
            }
        }
        else
        {
            BuyableHelper();
        }
    }

    private void BuyableHelper()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            // The button component in each item gameObject is disabled if the player coin is less than the cost of the corresponging item.
            Button btn = gameObjects[i].GetComponentInChildren<Button>();

            if (coinDisplay >= shopItemScripts[i].cost)
            {
                ChangeButtonInteractionAndText(btn, true, "Buy", 68);
            }
            else
            {
                ChangeButtonInteractionAndText(btn, false, "Not enough coins!", 45);
            }
        }
    }

    private void ChangeButtonInteractionAndText(Button btn, bool interactable, string text, int fontSize)
    {
        btn.interactable = interactable;
        btn.GetComponentInChildren<TextMeshProUGUI>().text = text;
        btn.GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;
    }

    // Adds the purchased item to the player profile's accessory list and deducts coins from player coins
    // (Assigned to the buttons on-click)
    public void PurchasingItem(int buttonNum)
    {
        if (coinDisplay >= shopItemScripts[buttonNum].cost)
        {
            // Deduct cost from the player
            playerList[selectedPlayerNum].deductFromBalance(shopItemScripts[buttonNum].cost);
            // Add accessory to the player
            string playerAccessory = shopItemScripts[buttonNum].title.Replace(" ", string.Empty);
            Debug.Log(playerAccessory);
            playerList[selectedPlayerNum].giveCosmetic(playerAccessory);

            SaveSelectedPlayerData();
            UpdateCoinTotal(selectedPlayerNum);
        }
    }

    //---------------Load and save player profile--------------------
    // These methods are similar to the LobbyManager player profile methods

    // Instantiates a new Player panel prefab in the scroll
    private void InsertPlayerIntoPlayerScroll(Player player)
    {
        // Instantiate prefab
        GameObject newPlayerPanel = Instantiate(playerScrollPanelPrefab);
        int currentPlayerPanelNum = playersScrollContent.transform.childCount - 1;
        newPlayerPanel.SetActive(true);
        newPlayerPanel.transform.SetParent(playersScrollContent);
        newPlayerPanel.transform.localScale = Vector2.one;
        // Assign listener to button click
        Button newPlayerButton = newPlayerPanel.transform.Find("PlayersScrollPanelButton").gameObject.GetComponent<Button>();
        newPlayerButton.onClick.AddListener(delegate { ShowSelectedPlayer(currentPlayerPanelNum); });
        // Update text to player's name
        newPlayerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.getName();
    }

    // Sets the PlayerNameText and AnimalImage objects to the currently selected player's data
    private void ShowSelectedPlayer(int playerNumber)
    {
        selectedPlayerNum = playerNumber;
        currentSelectedPlayerName.GetComponent<TextMeshProUGUI>().text = playerList[playerNumber].getName()+":";
        // The sprite is loaded from Assets/Resources/Character Face Sprites/
        currentSelectedPlayerAnimal.GetComponent<Image>().sprite = Resources.Load<Sprite>("Character Face Sprites/"+playerList[playerNumber].getAnimal());
        
        EnablePreview();
        UpdateCoinTotal(playerNumber);
    }

    private void SaveSelectedPlayerData()
    {
        dataManager.SaveData(playerList[selectedPlayerNum]);
    }
}
