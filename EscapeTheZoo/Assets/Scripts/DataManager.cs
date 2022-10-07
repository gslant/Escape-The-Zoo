using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public string fName = "profiles.txt";

    //Saves player data to persistent data path, which varies by device. it can commonly be found
    //at c:\users\name\Appdata\LocalLow\companyname
    public void SaveData(Player p)
    {
        p.setGameBalance(0);

        List<Player> tempPlayerList = new List<Player>();
        tempPlayerList = LoadData(fName);

        bool alreadyExists = false;

        //if the player already exists, save to the existing profile
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

    public void saveDataPrefs(Player p1, Player p2)
    {
        string play1 = "player1";
        string play2 = "player2";
        PlayerPrefs.SetString("name" + play1, p1.getName());
        PlayerPrefs.SetString("name" + play2, p2.getName());

        PlayerPrefs.SetInt("totalBalance" + play1, p1.getTotalBalance());
        PlayerPrefs.SetInt("totalBalance" + play2, p2.getTotalBalance());

        PlayerPrefs.SetInt("gameBalance" + play1, p1.getGameBalance());
        PlayerPrefs.SetInt("gameBalance" + play2, p2.getGameBalance());

        PlayerPrefs.SetString("animal" + play1, p1.getAnimal());
        PlayerPrefs.SetString("animal" + play2, p2.getAnimal());

        PlayerPrefs.SetString("accessory" + play1, p1.getAccessory());
        PlayerPrefs.SetString("accessory" + play2, p2.getAccessory());

        PlayerPrefs.SetInt("ownedCosmeticsCount" + play1, p1.getOwnedCosmetics().Count);
        PlayerPrefs.SetInt("ownedCosmeticsCount" + play2, p2.getOwnedCosmetics().Count);

        for(int i = 0; i < p1.getOwnedCosmetics().Count; i++)
        {
            PlayerPrefs.SetString("ownedCosmetics" + play1 + i, p1.getOwnedCosmetics()[i]);
        }

        for (int i = 0; i < p2.getOwnedCosmetics().Count; i++)
        {
            PlayerPrefs.SetString("ownedCosmetics" + play2 + i, p2.getOwnedCosmetics()[i]);
        }
    }

    public List<Player> LoadDataPrefs()
    {
        List<Player> playerList = new List<Player>();
        string play1 = "player1";
        string play2 = "player2";

        string pName = PlayerPrefs.GetString("name" + play1);
        int totalBalance = PlayerPrefs.GetInt("totalBalance" + play1);
        int gameBalance = PlayerPrefs.GetInt("gameBalance" + play1);
        string animal = PlayerPrefs.GetString("animal" + play1);
        string accessory = PlayerPrefs.GetString("accessory" + play1);
        int cosmeticsCount = PlayerPrefs.GetInt("ownedCosmeticsCount" + play1);

        List<string> ownedCosmetics = new List<string>();
        for (int i = 0; i < cosmeticsCount; i++)
        {
            ownedCosmetics.Add(PlayerPrefs.GetString("ownedCosmetics" + play1 + i));
        }

        Player player1 = new Player(pName, totalBalance, animal, accessory, ownedCosmetics);

        pName = PlayerPrefs.GetString("name" + play2);
        totalBalance = PlayerPrefs.GetInt("totalBalance" + play2);
        gameBalance = PlayerPrefs.GetInt("gameBalance" + play2);
        animal = PlayerPrefs.GetString("animal" + play2);
        accessory = PlayerPrefs.GetString("accessory" + play2);
        cosmeticsCount = PlayerPrefs.GetInt("ownedCosmeticsCount" + play2);

        ownedCosmetics = new List<string>();
        for (int i = 0; i < cosmeticsCount; i++)
        {
            ownedCosmetics.Add(PlayerPrefs.GetString("ownedCosmetics" + play2 + i));
        }

        Player player2 = new Player(pName, totalBalance, animal, accessory, ownedCosmetics);

        playerList.Add(player1);
        playerList.Add(player2);

        return playerList;
    }

    //Load list of players from file
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

    //This method can be used to delete the profile data. For development only, should not be user accessable
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

//Holds all information about a player
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
