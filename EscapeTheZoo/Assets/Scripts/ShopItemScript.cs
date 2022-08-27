using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Object/New Shop Item", order = -1)]
public class ShopItemScript : ScriptableObject
{
    public string title;
    public string description;
    public int cost;
 
}
