using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<PowerUpsItem> itemslist = new List<PowerUpsItem>();
    public InventoryDisplay InventoryDisplay;


    // Start is called before the first frame update
    void Start()
    {
        InventoryDisplay inventory = (InventoryDisplay)Instantiate(InventoryDisplay);
        inventory.setItems(itemslist);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
