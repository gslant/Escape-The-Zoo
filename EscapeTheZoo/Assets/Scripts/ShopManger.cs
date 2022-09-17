using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManger : MonoBehaviour
{
    public int coindisplay;
    public TMP_Text cointxt;
    public ShopItemScript[] shopItemScripts;
    public GameObject[] gameObjects;
    public ShopTemplete[] itemPanel;
    public Button[] buyingbuttons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
        cointxt.text = "Coins: " + coindisplay.ToString();
        createPanelItem();
        checkifbuyable();
    }


    public void addcoin()
    {
        coindisplay++;
        cointxt.text = "Coins: " + coindisplay.ToString();
        checkifbuyable();
    }

    public void purchasingItem(int buttonNum)
    {
        if (coindisplay >= shopItemScripts[buttonNum].cost)
        {
            coindisplay = coindisplay - shopItemScripts[buttonNum].cost;
            cointxt.text = "Coins: " + coindisplay.ToString();
            checkifbuyable();
        }
    }

    public void checkifbuyable()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            if (coindisplay >= shopItemScripts[i].cost)
                buyingbuttons[i].interactable = true;
            else
                buyingbuttons[i].interactable = false;
        }
    }

    public void createPanelItem()
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
