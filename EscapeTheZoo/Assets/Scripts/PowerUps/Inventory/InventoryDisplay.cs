using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public Transform targetTransform;
    public PowerUpItemDisplay inventoryitem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setItems(List<PowerUpsItem> items)
    {
        foreach(PowerUpsItem i in items)
        {
            PowerUpItemDisplay display = (PowerUpItemDisplay)Instantiate(inventoryitem);
            display.transform.SetParent(targetTransform, false);
            display.setItem(i);
        }
    }
}
