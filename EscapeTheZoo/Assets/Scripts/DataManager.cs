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
    private string name; // The name of the player
    private int totalBalance; // The total currency a player has. Can be used in the shop.
    private int gameBalance; // The amount of currency a player has in a game of "Escape The Zoo!". Cannot be used in the shop.

    private string animal; // The current animal being used as the avatar
    private string accessory; // The current equipped cosmetic
    private List<string> ownedCosmetics; // All owned cosmetics

    public Player(string name, int totalBalance, string animal, string accessory, List<string> ownedCosmetics)
    {
        this.name = name;
        this.totalBalance = totalBalance;
        this.gameBalance = 0;
        this.animal = animal;
        this.accessory = accessory;
        this.ownedCosmetics = ownedCosmetics;
    }

    // Gets the player's name
    public string getName()
    {
        return this.name;
    }

    // Sets the player's name
    public void setName(string name)
    {
        this.name = name;
    }

    // Gets the player's current totalBalance
    public int getTotalBalance()
    {
        return this.totalBalance;
    }

    // Sets the player's totalBalance to a certain value
    public void setTotalBalance(int amount)
    {
        this.totalBalance = amount;
    }

    // Gets the player's current gameBalance
    public int getGameBalance()
    {
        return this.gameBalance;
    }

    // Sets the player's gameBalance to a certain value
    public void setGameBalance(int amount)
    {
        this.gameBalance = amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    // Changes the gameBalance by "amount"
    public void changeGameBalanceByAmount(int amount)
    {
        this.gameBalance += amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    // Adds the entire gameBalance of a player to their totalBalance, then sets their gameBalance to 0
    public void addGameBalanceToTotalBalance()
    {
        this.totalBalance += this.gameBalance;
        this.gameBalance = 0;
    }

    // Gets the current animal avatar being used
    public string getAnimal()
    {
        return this.animal;
    }

    // Sets the current animal avatar being used
    public void setAnimal(string animal)
    {
        this.animal = animal;
    }

    // Gets the current accessory being used
    public string getAccessory()
    {
        return this.accessory;
    }

    // Sets the current accessory being used
    public void setAccessory(string accessory)
    {
        this.accessory = accessory;
    }

    // Returns the list of owned accessories
    public List<string> getOwnedCosmetics()
    {
        return this.ownedCosmetics;
    }

    // Adds to the list of owned accessories
    public void addCosmetic(string cosmeticName)
    {
        this.ownedCosmetics.Add(cosmeticName);
    }
}
