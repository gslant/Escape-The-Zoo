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
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
        cointxt.text = "Coins: " + coindisplay.ToString();
        CreatePanelItem();
        CheckIfBuyable();
    }


    public void Addcoin()
    {
        coindisplay++;
        cointxt.text = "Coins: " + coindisplay.ToString();
        CheckIfBuyable();
    }

    public void PurchasingItem(int buttonNum)
    {
        if (coindisplay >= shopItemScripts[buttonNum].cost)
        {
            coindisplay = coindisplay - shopItemScripts[buttonNum].cost;
            cointxt.text = "Coins: " + coindisplay.ToString();
            CheckIfBuyable();
        }
    }

    public void CheckIfBuyable()
    {
        // The button component in each item gameObject is disabled if the player coin
        // is less than the cost of the corresponging item.
        for (int i = 0; i < shopItemScripts.Length; i++)
        {
            Button btn = gameObjects[i].GetComponentInChildren<Button>();
            if (coindisplay >= shopItemScripts[i].cost)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }

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
