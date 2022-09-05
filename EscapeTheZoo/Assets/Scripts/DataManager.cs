using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public void SaveData()
    {

    }
}

[Serializable]

public class Player
{
    public string name;
    public int balance;
    public string animal;
    public List<string> ownedCosmetics;

    public void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return this.name;
    }

    public void deductFromBalance(int amount)
    {
        this.balance -= amount;
    }

    public void addToBalance(int amount)
    {
        this.balance += amount;
    }

    public List<string> getOwnedCosmetics()
    {
        return this.ownedCosmetics;
    }


    public void giveCosmetic(string cosmeticName)
    {
        this.ownedCosmetics.Add(cosmeticName);
    }

}

