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
    public GameObject playerScrollPanelPrefab; // To be instantiated for every player in the loaded data

    // Player profiles
    private List<Player> playerList = new List<Player>(); // A list of "Player" instances.
    private List<Player> listOfPlayers;
    private int selectedPlayerNum = -1; // The current selected player. Default being -1.
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
        cointxt.text = "Coins: " + coinDisplay.ToString();
        CreatePanelItem();
        CheckIfBuyable();
    }

    // Updates the coin displayed text with the selected player profile data
    public void UpdateCoinTotal()
    {
        coinDisplay++;
        cointxt.text = "Coins: " + coinDisplay.ToString();
        CheckIfBuyable();
    }

    // Adds the purchased item to the player profile's accessory list
    public void PurchasingItem(int buttonNum)
    {
        if (coinDisplay >= shopItemScripts[buttonNum].cost)
        {
            coinDisplay = coinDisplay - shopItemScripts[buttonNum].cost;
            cointxt.text = "Coins: " + coinDisplay.ToString();
            CheckIfBuyable();
        }
    }

    public void CheckIfBuyable()
    {
        
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            // The button component in each item gameObject is disabled if:
            // the player coin is less than the cost of the corresponging item.
            Button btn = gameObjects[i].GetComponentInChildren<Button>();
            if (coinDisplay >= shopItemScripts[i].cost)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
            // or if the item is already in the player's accessory list
        }
    }

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
}
