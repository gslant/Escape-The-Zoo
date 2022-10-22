using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItemDisplay : MonoBehaviour
{
    public Text textName;
    public Image sprite;

    public delegate void PowerUpItemDisplayDelegate(PowerUpsItem inventoryItem);
    public event PowerUpItemDisplayDelegate onClick;

    public PowerUpsItem item;

    // Start is called before the first frame update
    void Start()
    {
        if (item != null) setItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(PowerUpsItem item)
    {
        this.item = item;
        if(textName != null)
        {
            textName.text = item.displayName;
        }
        if(sprite != null)
        {
            sprite.sprite = item.sprite;
        }
    }

    public void Click()
    {
        if(onClick != null)
        {
            onClick.Invoke(item);
        }
    }
}
