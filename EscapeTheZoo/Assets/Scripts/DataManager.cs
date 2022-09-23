using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public string fName = "profiles.txt";
    public void SaveData(Player p)
    {
        p.setGameBalance(0);

        List<Player> tempPlayerList = new List<Player>();
        tempPlayerList = LoadData(fName);

        bool alreadyExists = false;

        for (int i = 0; i < tempPlayerList.Count; i++)
        {
            if (String.Equals(tempPlayerList[i].getName(), p.getName()))
            {
                tempPlayerList[i] = p;
                alreadyExists = true;
                break;
            }
        }
        if (!alreadyExists)
        {
            tempPlayerList.Add(p);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fName);
        //Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, tempPlayerList);
        file.Close();
    }

    public List<Player> LoadData(string fileName)
    {
        List<Player> playerList = new List<Player>();
        if(File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            playerList = (List<Player>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("Could not open file: " + fileName + " this may be because the file does not exist yet");
        }
        return playerList;
    }

    public void DeleteFile()
    {

        string filePath = Application.persistentDataPath + fName;

        if (File.Exists(filePath))
        {
            Debug.Log("Deleting file: " + fName + " at location: " + filePath);
            File.Delete(filePath);
        }
    }
}

[Serializable]

public class Player
{
    private string name;
    private int totalBalance; // The total currency a player has. Can be used in the shop.
    private int gameBalance; // The amount of currency a player has in a game of "Escape The Zoo!". Cannot be used in the shop.
    private string animal;
    private string accessory;
    private List<string> ownedCosmetics;

    public Player(string name, int totalBalance, string animal, string accessory, List<string> ownedCosmetics)
    {
        this.name = name;
        this.totalBalance = totalBalance;
        this.gameBalance = 0;
        this.animal = animal;
        this.accessory = accessory;
        this.ownedCosmetics = ownedCosmetics;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public int getTotalBalance()
    {
        return this.totalBalance;
    }

    public void setTotalBalance(int amount)
    {
        this.totalBalance = amount;
    }

    public int getGameBalance()
    {
        return this.gameBalance;
    }

    public void setGameBalance(int amount)
    {
        this.gameBalance = amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    public void changeGameBalanceByAmount(int amount)
    {
        this.gameBalance += amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    public void addGameBalanceToTotalBalance()
    {
        this.totalBalance += this.gameBalance;
        this.gameBalance = 0;
    }

    public string getAnimal()
    {
        return this.animal;
    }

    public void setAnimal(string animal)
    {
        this.animal = animal;
    }

    public string getAccessory()
    {
        return this.accessory;
    }

    public void setAccessory(string accessory)
    {
        this.accessory = accessory;
    }

    public List<string> getOwnedCosmetics()
    {
        return this.ownedCosmetics;
    }

    public void addCosmetic(string cosmeticName)
    {
        this.ownedCosmetics.Add(cosmeticName);
    }
}
